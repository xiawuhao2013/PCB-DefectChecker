using System;
using System.IO;
using System.Windows.Forms;

using PathMap = System.Collections.Generic.Dictionary<string, string>;

namespace DefectChecker.Common
{
    public class FolderHelper
    {
        private static FolderHelper _instance = new FolderHelper();
        private string _fileExtension = @"*.*";

        //

        private FolderHelper()
        {
        }

        //

        public static FolderHelper GetInstance()
        {
            if (null == _instance)
            {
                _instance = new FolderHelper();
            }

            return _instance;
        }

        public void ResetFileExtension()
        {
            _fileExtension = @"*.*";

            return;
        }

        public void SetFileExtension(string extension)
        {
            _fileExtension = extension;

            return;
        }

        public bool TryGetChildrenDirMap(string dirPath, out PathMap childrenDirMap)
        {
            childrenDirMap = new PathMap();
            try
            {
                var dir = new DirectoryInfo(dirPath);
                foreach (var dirInfo in dir.GetDirectories())
                {
                    childrenDirMap.Add(dirInfo.Name, dirInfo.FullName);
                }
            }
            catch (Exception ex)
            {
                childrenDirMap.Clear();
                MessageBox.Show(ex.Message);

                return false;
            }

            return true;
        }

        public bool TryGetChildrenFileMap(string dirPath, out PathMap childrenFileMap)
        {
            childrenFileMap = new PathMap();
            try
            {
                var dir = new DirectoryInfo(dirPath);
                foreach (var fileInfo in dir.GetFiles(_fileExtension))
                {
                    childrenFileMap.Add(fileInfo.Name, fileInfo.FullName);
                }
            }
            catch (Exception ex)
            {
                childrenFileMap.Clear();
                MessageBox.Show(ex.Message);

                return false;
            }

            return true;
        }
    }
}
