using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectChecker.Common
{
    public class EncodingHelper
    {
        private static EncodingHelper _instance = new EncodingHelper();

        //

        private EncodingHelper()
        {
        }

        //

        private static Encoding GetType(FileStream fs)
        {
            byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM 

            BinaryReader r = new BinaryReader(fs, Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);
            if (IsUTF8Bytes(ss) || (ss[0] == UTF8[0] && ss[1] == UTF8[1] && ss[2] == UTF8[2]))
            {
                r.Close();

                return Encoding.UTF8;
            }
            if (ss[0] == UnicodeBIG[0] && ss[1] == UnicodeBIG[1] && ss[2] == UnicodeBIG[2])
            {
                r.Close();

                return Encoding.BigEndianUnicode;
            }
            if (ss[0] == Unicode[0] && ss[1] == Unicode[1] && ss[2] == Unicode[2])
            {
                r.Close();

                return Encoding.Unicode;
            }
            r.Close();

            return Encoding.Default;
        }

        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1; //计算当前正分析的字符应还有的字节数 
            byte curByte; //当前分析的字节. 
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前 
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X 
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1 
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式");
            }
            return true;
        }

        //

        public static EncodingHelper GetInstance()
        {
            if (null == _instance)
            {
                _instance = new EncodingHelper();
            }

            return _instance;
        }

        public Encoding GetEncodingType(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            Encoding encodingType = GetType(fs);
            fs.Close();

            return encodingType;
        }

        public string UTF8ToUnicode(string utf8String)
        {
            Encoding unicode = Encoding.Unicode;
            Encoding utf8 = Encoding.UTF8;

            byte[] utf8Bytes = utf8.GetBytes(utf8String);
            byte[] unicodeBytes = Encoding.Convert(utf8, unicode, utf8Bytes);

            char[] unicodeChars = new char[unicode.GetCharCount(unicodeBytes, 0, unicodeBytes.Length)];
            unicode.GetChars(unicodeBytes, 0, unicodeBytes.Length, unicodeChars, 0);
            string unicodeString = new string(unicodeChars);

            return unicodeString;
        }
    }
}
