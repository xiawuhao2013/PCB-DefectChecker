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

        private PathMap GetDefectInfoOfBoard(string productName, string batchName, string boardName)
        {
            PathMap defectCellOfBoard = new PathMap();
            foreach (var side in GetSideInfo(productName, batchName, boardName))
            {
                foreach (var shot in GetShotInfo(side.Value))
                {
                    foreach (var defectCellOfShot in GetDefectInfoOfShot(shot.Value))
                    {
                        defectCellOfBoard.Add(defectCellOfShot.Key, defectCellOfShot.Value);
                    }
                }
            }

            return defectCellOfBoard;
        }

        private PathMap GetSideInfo(string productName, string batchName, string boardName)
        {
            PathMap side = new PathMap();
            if (!GetBoardPathInfo(productName, batchName).TryGetValue(boardName, out var boardPath))
            {
                side.Clear();
            }
            if (!folder.TryGetChildrenDirMap(boardName, out side))
            {
                side.Clear();
            }

            return side;
        }

        private PathMap GetShotInfo(string sidePath)
        {
            PathMap shot = new PathMap();
            if (!folder.TryGetChildrenDirMap(sidePath, out shot))
            {
                shot.Clear();
            }

            return shot;
        }

        private PathMap GetDefectInfoOfShot(string shotPath)
        {
            PathMap defectCell = new PathMap();
            folder.FileExtension = _defectFileExtent;
            if (!folder.TryGetChildrenFileMap(shotPath, out defectCell))
            {
                defectCell.Clear();
            }

            return defectCell;
        }

        public void GetDefetInBoard(string productName, string batchName, string boardName, out List<string> defectImgList)
        {
            defectImgList = new List<string>();
            try
            {

            }
            catch (Exception ex)
            {

            }

            throw new NotImplementedException();
        }

        public void GetDefectCell(string batchName, string boardName, string defectImgName, out DefectCell defectCell)
        {
            throw new NotImplementedException();
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
