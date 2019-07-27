using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Aqrose.Framework.Utility.Tools;
using DefectChecker.DefectDataStructure;

using PathMap = System.Collections.Generic.Dictionary<string, string>;

namespace DefectChecker.DeviceModule.MachVision
{
    class DeviceMachVision : DeviceInterface
    {
        private const string _paramFileName = @"\ParamFile.xml";
        private string _dataDir = default(string);
        private string _modelDir = default(string);
        private PathMap _productMap = new PathMap();
        private PathMap _batchMap = new PathMap();
        private PathMap _boardMap = new PathMap();

        DeviceMachVision()
        {
            LoadConfig();
        }

        private bool GetChildrenDirMap(string dirPath, out PathMap childrenDirMap)
        {
            childrenDirMap = new PathMap();
            //childrenDirMap.Clear();
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

        private void LoadConfig()
        {
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.ReadParameter(Application.StartupPath + _paramFileName);
            _dataDir = xmlParameter.GetParamData("DataDir");
            _modelDir = xmlParameter.GetParamData("ModelDir");

            return;
        }

        public void GetProductList(out List<string> procductList)
        {
            procductList = new List<string>();
            //procductList.Clear();
            try
            {
                _productMap.Clear();
                if (!GetChildrenDirMap(_dataDir, out _productMap))
                {
                    return;
                }
                foreach (var product in _productMap)
                {
                    procductList.Add(product.Key);
                }
            }
            catch (Exception ex)
            {
                procductList.Clear();
                _productMap.Clear();
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }

        public void GetBatchList(string productName, out List<string> batchList)
        {
            batchList = new List<string>();
            //batchList.Clear();
            try
            {
                _batchMap.Clear();
                GetProductList(out var procductList);
                if (!_productMap.TryGetValue(productName, out var productPath))
                {
                    return;
                }
                if (!GetChildrenDirMap(productPath, out _batchMap))
                {
                    return;
                }
                foreach (var batch in _batchMap)
                {
                    batchList.Add(batch.Key);
                }
            }
            catch (Exception ex)
            {
                batchList.Clear();
                _batchMap.Clear();
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
                _boardMap.Clear();
                GetBatchList(productName, out var batchList);
                if (!_batchMap.TryGetValue(batchName, out var batchPath))
                {
                    return;
                }
                if (!GetChildrenDirMap(batchPath, out _boardMap))
                {
                    return;
                }
                foreach (var board in _boardMap)
                {
                    boardList.Add(board.Key);
                }
            }
            catch (Exception ex)
            {
                boardList.Clear();
                _batchMap.Clear();
                MessageBox.Show(ex.Message);

                return;
            }

            return;
        }

        public void GetCodeList(out Dictionary<int, string> codeList)
        {
            throw new NotImplementedException();
        }

        public void GetDefectCell(string batchName, string boardName, string defectImgName, out DefectCell defectCell)
        {
            throw new NotImplementedException();
        }

        public void GetDefetInBoard(string productName, string batchName, string boardName, out List<string> defectImgList)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void GetTemplateWholeImgB(out Bitmap wholeImg)
        {
            throw new NotImplementedException();
        }
    }
}
