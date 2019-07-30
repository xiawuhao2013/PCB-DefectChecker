using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Aqrose.Framework.Utility.Tools;
using DefectChecker.Common;
using DefectChecker.DefectDataStructure;

using PathMap = System.Collections.Generic.Dictionary<string, string>;

namespace DefectChecker.DeviceModule.MachVision
{
    class DeviceMachVision : DeviceInterface
    {
        private const string _paramFileName = @"\ParamFile.xml";
        private const string _sideA = @"SideA";
        private const string _sideB = @"SideB";
        private const string _fileName = @"Panel.jpg";
        private const string _defectFileExtent = @"*.bmp";
        private string _dataDir = default(string);
        private string _modelDir = default(string);
        private FolderHelper folder = new FolderHelper();

        public DeviceMachVision()
        {
            folder.FileExtension = @"*.jpg";
            LoadConfig();
        }

        private PathMap GetDefectPathInfo(string productName, string batchName, string boardName, string sideName, string shotName)
        {
            PathMap defect = new PathMap();
            if (!GetShotPathInfo(productName, batchName, boardName, sideName).TryGetValue(shotName, out var shotPath))
            {
                defect.Clear();
            }
            folder.FileExtension = _defectFileExtent;
            if (!folder.TryGetChildrenFileMap(shotPath, out defect))
            {
                defect.Clear();
            }

            return defect;
        }

        private PathMap GetShotPathInfo(string productName, string batchName, string boardName, string sideName)
        {
            PathMap shot = new PathMap();
            if (!GetSidePathInfo(productName, batchName, boardName).TryGetValue(sideName, out var sidePath))
            {
                shot.Clear();
            }
            if (!folder.TryGetChildrenDirMap(sidePath, out shot))
            {
                shot.Clear();
            }

            return shot;
        }

        private PathMap GetSidePathInfo(string productName, string batchName, string boardName)
        {
            PathMap side = new PathMap();
            if (!GetBoardPathInfo(productName, batchName).TryGetValue(boardName, out var boardPath))
            {
                side.Clear();
            }
            if (!folder.TryGetChildrenDirMap(boardPath, out side))
            {
                side.Clear();
            }

            return side;
        }

        private bool TryGetTemplateImg(string side, string fileName, out Bitmap wholeImg)
        {
            wholeImg = null;
            try
            {
                if (!folder.TryGetChildrenDirMap(_modelDir, out var pathMap))
                {
                    return false;
                }
                if (!pathMap.TryGetValue(side, out var path))
                {
                    return false;
                }
                if (!folder.TryGetChildrenFileMap(path, out var fileMap))
                {
                    return false;
                }
                if (!fileMap.TryGetValue(fileName, out var filePath))
                {
                    return false;
                }
                if (!File.Exists(filePath))
                {
                    return false;
                }
                wholeImg = new Bitmap(filePath);
            }
            catch (Exception ex)
            {
                wholeImg = null;
                MessageBox.Show(ex.Message);

                return false;
            }

            return true;
        }

        private PathMap GetBoardPathInfo(string productName, string batchName)
        {
            PathMap board = new PathMap();
            if (!GetBatchPathInfo(productName).TryGetValue(batchName, out var batchPath))
            {
                board.Clear();
            }
            if (!folder.TryGetChildrenDirMap(batchPath, out board))
            {
                board.Clear();
            }

            return board;
        }

        private PathMap GetBatchPathInfo(string productName)
        {
            PathMap batch = new PathMap();
            if (!GetProductPathInfo().TryGetValue(productName, out var productPath))
            {
                batch.Clear();
            }
            if (!folder.TryGetChildrenDirMap(productPath, out batch))
            {
                batch.Clear();
            }

            return batch;
        }

        private PathMap GetProductPathInfo()
        {
            PathMap product = new PathMap();
            if (!folder.TryGetChildrenDirMap(_dataDir, out product))
            {
                product.Clear();
            }

            return product;
        }

        private void LoadConfig()
        {
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.ReadParameter(Application.StartupPath + _paramFileName);
            _dataDir = xmlParameter.GetParamData("DataDir");
            _modelDir = xmlParameter.GetParamData("ModelDir");

            return;
        }

        //

        public void GetDefectCell(string productName, string batchName, string boardName, string sideName, string shotName, string defectName, out DefectCell defectCell)
        {
            try
            {
                defectCell = new DefectCell();
                if (!GetDefectPathInfo(productName, batchName, boardName, sideName, shotName).TryGetValue(defectName, out var defectPath))
                {
                    return;
                }
                if (!File.Exists(defectPath))
                {
                    return;
                }
                defectCell.DefectImage = new Bitmap(defectPath);
            }
            catch (Exception ex)
            {
                defectCell = new DefectCell();
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }

        public void GetDefectListInShot(string productName, string batchName, string boardName, string sideName, string shotName, out List<string> defectList)
        {
            defectList = new List<string>();
            try
            {
                foreach (var defect in GetDefectPathInfo(productName, batchName, boardName, sideName, shotName))
                {
                    defectList.Add(defect.Key);
                }
            }
            catch (Exception ex)
            {
                defectList.Clear();
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }

        public void GetShotList(string productName, string batchName, string boardName, string sideName, out List<string> shotList)
        {
            shotList = new List<string>();
            try
            {
                foreach (var shot in GetShotPathInfo(productName, batchName, boardName, sideName))
                {
                    shotList.Add(shot.Key);
                }
            }
            catch (Exception ex)
            {
                shotList.Clear();
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }

        public void GetSideList(string productName, string batchName, string boardName, out List<string> sideList)
        {
            sideList = new List<string>();
            try
            {
                foreach (var side in GetSidePathInfo(productName, batchName, boardName))
                {
                    sideList.Add(side.Key);
                }
            }
            catch (Exception ex)
            {
                sideList = new List<string>();
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }

        public void GetBoardList(string productName, string batchName, out List<string> boardList)
        {
            boardList = new List<string>();
            try
            {
                foreach (var board in GetBoardPathInfo(productName, batchName))
                {
                    boardList.Add(board.Key);
                }
            }
            catch (Exception ex)
            {
                boardList.Clear();
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }

        public void GetBatchList(string productName, out List<string> batchList)
        {
            batchList = new List<string>();
            try
            {
                foreach (var batch in GetBatchPathInfo(productName))
                {
                    batchList.Add(batch.Key);
                }
            }
            catch (Exception ex)
            {
                batchList.Clear();
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }

        public void GetCodeList(out Dictionary<int, string> codeList)
        {
            throw new NotImplementedException();
        }

        public void GetProductList(out List<string> procductList)
        {
            procductList = new List<string>();
            try
            {
                foreach (var product in GetProductPathInfo())
                {
                    procductList.Add(product.Key);
                }
            }
            catch (Exception ex)
            {
                procductList.Clear();
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }

        public void GetGerberWholeImgA(out Bitmap gerberWholeImg)
        {
            throw new NotImplementedException();
        }

        public void GetGerberWholeImgB(out Bitmap gerberWholeImg)
        {
            throw new NotImplementedException();
        }

        public void GetTemplateWholeImgA(out Bitmap wholeImg)
        {
            wholeImg = null;
            try
            {
                if (!TryGetTemplateImg(_sideA, _fileName, out var image))
                {
                    return;
                }
                wholeImg = image.Clone() as Bitmap;
            }
            catch(Exception ex)
            {
                wholeImg = null;
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }

        public void GetTemplateWholeImgB(out Bitmap wholeImg)
        {
            wholeImg = null;
            try
            {
                if (!TryGetTemplateImg(_sideB, _fileName, out var image))
                {
                    return;
                }
                wholeImg = image.Clone() as Bitmap;
            }
            catch (Exception ex)
            {
                wholeImg = null;
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }


        /******************************************************/

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

        
    }
}
