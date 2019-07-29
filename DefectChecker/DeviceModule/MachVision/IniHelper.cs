using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DefectChecker.Common
{
    public class IniHelper
    {
        private string _path = default(string);

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

        public void ReadValue(string section, string key, string path, out string value)
        {
            try
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(section, key, "", temp, 1024, path);
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

        public void SetPath(string path)
        {
            _path = path;
        }


    }
}
