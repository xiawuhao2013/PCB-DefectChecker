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
        private static IniHelper _instance = new IniHelper();

        //

        private IniHelper()
        {
        }

        //

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        //

        public static IniHelper GetInstance()
        {
            if (null ==_instance)
            {
                _instance = new IniHelper();
            }

            return _instance;
        }

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

        public void ReadValue(string section, string key, string path, out string value)
        {
            try
            {
                Encoding utf8 = Encoding.UTF8;
                Encoding unicode = Encoding.Unicode;
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(section, key, null, temp, 1024, path);
                value = temp.ToString();
            }
            catch (Exception ex)
            {
                value = default(string);
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }
    }
}
