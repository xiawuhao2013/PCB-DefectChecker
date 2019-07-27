using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PathMap = System.Collections.Generic.Dictionary<string, string>;

namespace DefectChecker.Common
{
    public class FolderHelper
    {
        public string FileExtension { get; set; }

        public FolderHelper()
        {
            FileExtension= @"*.*";
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
                foreach (var fileInfo in dir.GetFiles(FileExtension))
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
