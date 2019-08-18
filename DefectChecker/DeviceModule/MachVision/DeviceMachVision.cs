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
        private const string _fileName = @"Panel.jpg";
        private const string _defectFileExtent = @"*.bmp";
        private const string _codeFileExtent = @"*.par";
        private const string _shotResultFileExtent = @"*.ini";
        private const string _shot0ResultFileName = @"ResultShot_0.ini";
        private const string _shot1ResultFileName = @"ResultShot_1.ini";
        private const string _section = "Info";
        private const string _keyOfName = "Name";
        private const string _keyOfID = "ParamID";
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

        private bool TryGetTemplateImg(string side, string fileName, out Bitmap wholeImg)
        {
            wholeImg = null;
            try
            {
                if (!FolderHelper.GetInstance().TryGetChildrenDirMap(_modelDir, out var pathMap))
                {
                    return false;
                }
                if (!pathMap.TryGetValue(side, out var path))
                {
                    return false;
                }
                FolderHelper.GetInstance().ResetFileExtension();
                if (!FolderHelper.GetInstance().TryGetChildrenFileMap(path, out var fileMap))
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

        // LABEL: need product name?
        private PathMap GetCodeFileList()
        {
            PathMap codeFile = new PathMap();
            try
            {
                FolderHelper.GetInstance().SetFileExtension(_codeFileExtent);
                if (!FolderHelper.GetInstance().TryGetChildrenFileMap(_modelDir, out codeFile))
                {
                    codeFile.Clear();
                }
            }
            catch (Exception ex)
            {
                codeFile.Clear();
                MessageBox.Show(ex.Message);
            }

            return codeFile;
        }
        
        public void GetDefectCell(string productName, string batchName, string boardName, string sideName, string shotName, string defectName, out DefectCell defectCell)
        {
            defectCell = new DefectCell();
            string defectPath = _dataDir +"\\" + productName + "\\" + batchName + "\\" + 
                                boardName + "\\" + sideName + "\\" + shotName;
            MachVisionFile machVisionFile = new MachVisionFile(defectPath, shotName);
            defectName = Regex.Replace(defectName, @"(.bmp)$", "");
            machVisionFile.ReadDefectInfo(defectName, out var defectInfo);
            defectCell.Info = defectInfo;
            defectCell.DefectImage = new Bitmap(defectPath+"\\"+defectName+".bmp");
            Bitmap templateBitmap;
            GetTemplateWholeImgA(out templateBitmap);
            
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

        // LABEL: some bugs to fix.
        public int GetCodeList(out Dictionary<int, string> codeList)
        {
            codeList = new Dictionary<int, string>();
            try
            {
                IniHelper iniHelper = new IniHelper();
                foreach (var codeFile in GetCodeFileList())
                {
                    if (File.Exists(codeFile.Value))
                    {
                        var encodingType = EncodingHelper.GetInstance().GetEncodingType(codeFile.Value);
                        iniHelper.ReadValue(_section, _keyOfName, codeFile.Value, out string valueOfName);
                        iniHelper.ReadValue(_section, _keyOfID, codeFile.Value, out string valueOfID);
                        if (Encoding.UTF8 == encodingType)
                        {
                            valueOfName = EncodingHelper.GetInstance().UTF8ToUnicode(valueOfName);
                            valueOfID= EncodingHelper.GetInstance().UTF8ToUnicode(valueOfID);
                        }
                        if (default(string) == valueOfName || default(string) == valueOfID || "" == valueOfName || "" == valueOfID)
                        {
                            continue;
                        }
                        var id = Convert.ToInt32(valueOfID);
                        codeList.Add(id, valueOfName);
                    }
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

        public void GetGerberWholeImgA(out Bitmap gerberWholeImg)
        {
            gerberWholeImg = null;
            try
            {
                if (!TryGetTemplateImg("SideA", _fileName, out var image))
                {
                    return;
                }
                gerberWholeImg = image.Clone() as Bitmap;
            }
            catch (Exception ex)
            {
                gerberWholeImg = null;
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }

        public void GetGerberWholeImgB(out Bitmap gerberWholeImg)
        {
            gerberWholeImg = null;
            try
            {
                if (!TryGetTemplateImg("SideB", _fileName, out var image))
                {
                    return;
                }
                gerberWholeImg = image.Clone() as Bitmap;
            }
            catch (Exception ex)
            {
                gerberWholeImg = null;
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }

        public void GetTemplateWholeImgA(out Bitmap wholeImg)
        {
            wholeImg = new Bitmap(1000, 1000);
        }

        public void GetTemplateWholeImgB(out Bitmap wholeImg)
        {
            wholeImg = new Bitmap(1000, 1000);
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

        public void SetDataDir(string modelDir, string dataDir)
        {
            _modelDir = modelDir;
            _dataDir = dataDir;
        }
    }
}
