using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Aqrose.Framework.Utility.Tools;
using DefectChecker.Common;
using DefectChecker.DefectDataStructure;

using PathMap = System.Collections.Generic.Dictionary<string, string>;

namespace DefectChecker.DeviceModule.MachVision
{
    class DeviceMachVision : DeviceInterface
    {
        private const string _defectFileExtent = @"*.bmp";
        private const string _codeFileExtent = @"*.par";
        private const string _shotResultFileExtent = @"*.ini";
        private const string _shot0ResultFileName = @"ResultShot_0.ini";
        private const string _shot1ResultFileName = @"ResultShot_1.ini";
        private string _dataDir = "";
        private string _modelDir = "";
        
        public DeviceMachVision()
        {

        }

        private PathMap GetDefectPathInfo(string productName, string batchName, string boardName, string sideName, string shotName)
        {
            PathMap defect = new PathMap();
            if (!GetShotPathInfo(productName, batchName, boardName, sideName).TryGetValue(shotName, out var shotPath))
            {
                defect.Clear();
            }
            FolderHelper.GetInstance().SetFileExtension(_defectFileExtent);
            if (!FolderHelper.GetInstance().TryGetChildrenFileMap(shotPath, out defect))
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
            if (!FolderHelper.GetInstance().TryGetChildrenDirMap(sidePath, out shot))
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
            if (!FolderHelper.GetInstance().TryGetChildrenDirMap(boardPath, out side))
            {
                side.Clear();
            }

            return side;
        }

        private PathMap GetBoardPathInfo(string productName, string batchName)
        {
            PathMap board = new PathMap();
            if (!GetBatchPathInfo(productName).TryGetValue(batchName, out var batchPath))
            {
                board.Clear();
            }
            if (!FolderHelper.GetInstance().TryGetChildrenDirMap(batchPath, out board))
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
            if (!FolderHelper.GetInstance().TryGetChildrenDirMap(productPath, out batch))
            {
                batch.Clear();
            }

            return batch;
        }

        private PathMap GetProductPathInfo()
        {
            PathMap product = new PathMap();
            if (!FolderHelper.GetInstance().TryGetChildrenDirMap(_dataDir, out product))
            {
                product.Clear();
            }

            return product;
        }
        
        private bool TryGetGerberImg(string side, out Bitmap wholeImg)
        {
            wholeImg = null;
            try
            {
                string filePath = _modelDir + "\\"+ side + "\\Panel.jpg";
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

        private bool TryGetTemplateImg(string sideName, string shotName, out Bitmap templateBitmap)
        {
            templateBitmap = null;
            try
            {
                string filePath = _modelDir + "\\" + sideName + "\\"+shotName+".jpg";
                if (!File.Exists(filePath))
                {
                    return false;
                }
                templateBitmap = new Bitmap(filePath);
            }
            catch (Exception ex)
            {
                templateBitmap = null;
                MessageBox.Show(ex.Message);

                return false;
            }

            return true;
        }
        
        private Dictionary<string, string> GetDefectPositionInfoOfShot(string productName, string batchName, string boardName, string sideName, string shotName, string shotResultFileName)
        {
            Dictionary<string, string> defectPositionOfShot = new Dictionary<string, string>();
            IniHelper iniHelper = new IniHelper();
            if (!GetShotPathInfo(productName, batchName, boardName, sideName).TryGetValue(shotName, out var shotPath))
            {
                return defectPositionOfShot;
            }
            FolderHelper.GetInstance().SetFileExtension(_shotResultFileExtent);
            if (!FolderHelper.GetInstance().TryGetChildrenFileMap(shotPath, out var shotResultFile))
            {
                return defectPositionOfShot;
            }
            if (1 != shotResultFile.Count)
            {
                return defectPositionOfShot;
            }
            if (!shotResultFile.TryGetValue(shotResultFileName, out var shotResultFilePath))
            {
                return defectPositionOfShot;
            }
            var encodingType = EncodingHelper.GetInstance().GetEncodingType(shotResultFilePath);
            foreach (var defect in GetDefectPathInfo(productName, batchName, boardName, sideName, shotName))
            {
                iniHelper.ReadValue(defect.Key, @"SD_0000", shotResultFilePath, out var position);
                defectPositionOfShot.Add(defect.Key, position);
            }

            return defectPositionOfShot;
        }

        #region Device Interface
        
        public void SetDataDir(string modelDir, string dataDir)
        {
            _modelDir = modelDir;
            _dataDir = dataDir;
        }

        public int GetCodeList(out Dictionary<int, string> codeList)
        {
            codeList = new Dictionary<int, string>();
            try
            {
                string fileName = _modelDir + "\\DefectCodeList.ini";
                IniHelper iniHelper = new IniHelper();
                int i = 1;
                while (true)
                {
                    iniHelper.ReadValue("Info", "Name" + i.ToString(), fileName, out string valueOfName);
                    iniHelper.ReadValue("Info", "CodeID" + i.ToString(), fileName, out string valueOfID);
                    i++;
                    if (default(string) == valueOfName || default(string) == valueOfID || "" == valueOfName || "" == valueOfID)
                    {
                        break;
                    }
                    var id = Convert.ToInt32(valueOfID);
                    codeList.Add(id, valueOfName);
                }
            }
            catch (Exception ex)
            {
                codeList.Clear();
                MessageBox.Show(ex.Message);

                return 0;
            }

            return codeList.Count;
        }

        public int GetProductList(out List<string> procductList)
        {
            Dictionary<int, string> codeList;
            GetCodeList(out codeList);
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

                return 0;
            }

            return procductList.Count;
        }

        public int GetBatchList(string productName, out List<string> batchList)
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

                return 0;
            }

            return batchList.Count;
        }

        public int GetBoardList(string productName, string batchName, out List<string> boardList)
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

                return 0;
            }

            return boardList.Count;
        }

        public int GetSideList(string productName, string batchName, string boardName, out List<string> sideList)
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
                sideList.Clear();
                MessageBox.Show(ex.Message);

                return 0;
            }

            return sideList.Count;
        }

        public int GetShotList(string productName, string batchName, string boardName, string sideName, out List<string> shotList)
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

                return 0;
            }

            return shotList.Count;
        }

        public int GetDefectListInShot(string productName, string batchName, string boardName, string sideName, string shotName, out List<string> defectList)
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

                return 0;
            }

            return defectList.Count;
        }

        public void GetDefectCell(string productName, string batchName, string boardName, string sideName, string shotName, string defectName, out DefectCell defectCell)
        {
            defectCell = new DefectCell();
            string defectPath = _dataDir + "\\" + productName + "\\" + batchName + "\\" +
                                boardName + "\\" + sideName + "\\" + shotName;
            MachVisionFile machVisionFile = new MachVisionFile(defectPath, shotName);
            defectName = Regex.Replace(defectName, @"(.bmp)$", "");
            machVisionFile.ReadDefectInfo(defectName, out var defectInfo);
            defectCell.Info = defectInfo;
            defectCell.DefectImage = new Bitmap(defectPath + "\\" + defectName + ".bmp");


            Bitmap templateBitmap;
            TryGetTemplateImg(sideName, shotName, out templateBitmap);

            Bitmap templateImage;
            if (ImageOperateTools.BitmapCropImage(templateBitmap, defectInfo.RoiInTemplate, out templateImage))
            {
                defectCell.TemplateImage = templateImage;
            }
            else
            {
                defectCell.TemplateImage = null;
            }

        }

        public bool GetTemplateWholeImgA(out Bitmap wholeImg)
        {
            wholeImg = new Bitmap(1000, 1000);

            return true;
        }

        public bool GetTemplateWholeImgB(out Bitmap wholeImg)
        {
            wholeImg = new Bitmap(1000, 1000);

            return true;
        }

        public bool GetGerberWholeImgA(out Bitmap gerberWholeImg)
        {
            gerberWholeImg = null;
            try
            {
                if (!TryGetGerberImg("SideA", out var image))
                {
                    return false;
                }
                gerberWholeImg = image.Clone() as Bitmap;
            }
            catch (Exception ex)
            {
                gerberWholeImg = null;
                MessageBox.Show(ex.Message);

                return false;
            }

            return true;
        }

        public bool GetGerberWholeImgB(out Bitmap gerberWholeImg)
        {
            gerberWholeImg = null;
            try
            {
                if (!TryGetGerberImg("SideB", out var image))
                {
                    return false;
                }
                gerberWholeImg = image.Clone() as Bitmap;
            }
            catch (Exception ex)
            {
                gerberWholeImg = null;
                MessageBox.Show(ex.Message);

                return false;
            }

            return true;
        }
        
        #endregion
    }
}
