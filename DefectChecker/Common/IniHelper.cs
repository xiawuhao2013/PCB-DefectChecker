using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefectChecker.Common
{
    public class IniHelper
    {
        public IniHelper()
        {
        }

        //

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        
        public bool TryWriteValue(string section, string key, string value, string path)
        {
            try
            {
                WritePrivateProfileString(section, key, value, path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public int ReadValue(string section, string key, string path, out string value)
        {
            int strLen;
            try
            {
                StringBuilder temp = new StringBuilder(1024);
                strLen = GetPrivateProfileString(section, key, null, temp, 1024, path);
                value = temp.ToString();
            }
            catch (Exception ex)
            {
                value = default(string);
                MessageBox.Show(ex.Message);

                return 0;
            }

            return strLen;
        }
    }
}
