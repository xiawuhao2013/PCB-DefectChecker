﻿using Aqrose.Framework.Utility.MessageManager;
using Aqrose.Framework.Utility.Tools;
using DefectChecker.DataBase.SqliteDataBase;
using DefectChecker.DefectDataStructure;
using DefectChecker.DeviceModule.MachVision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DefectChecker.DataBase
{
    public class DataBaseManager
    {
        // config files
        private const string _fileDataBaseManager = @"\config\DataBaseManager.xml";

        private SqliteDB _sqliteDb;
        private DeviceMachVision _device = new DeviceMachVision();

        private string _dataDir;
        private string _modelDir;
        private string _dataBaseDir;
        private string _dataBaseName;
        private int _dilationPixel;
        private int _displayWindowNum;
        private bool _isJumpMarkedData;
        
        // product dictionary - 1
        private List<string> _productNameList = new List<string>();
        private List<string> _batchNameList = new List<string>();
        private List<string> _boardNameList = new List<string>();
        private List<string> _sideNameList = new List<string>();
        private List<string> _shotNameList = new List<string>();
        private List<string> _defectNameList = new List<string>();

        // product dictionary - 2
        public List<string> ProductNameList { get { return _productNameList; } }
        public List<string> BatchNameList { get { return _batchNameList; } }
        public List<string> BoardNameList { get { return _boardNameList; } }
        public List<string> SideNameList { get { return _sideNameList; } }
        public List<string> ShotNameList { get { return _shotNameList; } }
        public List<string> DefectNameList { get { return _defectNameList; } }

        // product name info
        public string ProductName { get; set; }
        public string BatchName { get; set; }
        public string BoardName { get; set; }
        public string SideName { get; set; }
        public string ShotName { get; set; }
        public string DefectName { get; set; }
        public List<DefectCell> DefectCells { get; set; }
        public int DefectRegionIndex { get; set; }
        public int DisplayWindowIndex { get; private set; }

        public DataBaseManager(string dataDir, string modelDir, string dataBaseDir, 
            string dataBaseName, int dilationPixel, int displayWindowNum, bool isJumpMarkedData)
        {
            Init(dataDir, modelDir, dataBaseDir, dataBaseName, dilationPixel, displayWindowNum, isJumpMarkedData);
        }
        
        private void Init(string dataDir, string modelDir, string dataBaseDir,
            string dataBaseName, int dilationPixel, int displayWindowNum, bool isJumpMarkedData)
        {
            _dataDir = dataDir;
            _modelDir = modelDir;
            _dataBaseDir = dataBaseDir;
            _dataBaseName = dataBaseName;
            _dilationPixel = dilationPixel;
            _displayWindowNum = displayWindowNum;
            _isJumpMarkedData = isJumpMarkedData;

            _sqliteDb = new SqliteDB(_dataBaseDir, _dataBaseName, "MarkTable");
            ResetProduct();
            LoadDataBaseInfo();
            UpdateDataCells();
            SaveDataBaseInfo();
        }

        private void LoadDataBaseInfo()
        {
            _device.SetDataDir(_modelDir, _dataDir);

            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.ReadParameter(Application.StartupPath + _fileDataBaseManager);

            ProductName = xmlParameter.GetParamData("ProductName");
            BatchName = xmlParameter.GetParamData("BatchName");
            BoardName = xmlParameter.GetParamData("BoardName");
            SideName = xmlParameter.GetParamData("SideName");
            ShotName = xmlParameter.GetParamData("ShotName");
            DefectName = xmlParameter.GetParamData("DefectName");

            RefreshProductNameList();
            if (!ProductNameList.Contains(ProductName))
            {
                TrySelectProduct(0);
            }
            RefreshBatchNameList();
            if (!BatchNameList.Contains(BatchName))
            {
                TrySelectBatch(0);
            }
            RefreshBoardNameList();
            if(!BoardNameList.Contains(BoardName))
            {
                TrySelectBoard(0);
            }
            RefreshSideNameList();
            if (!SideNameList.Contains(SideName))
            {
                TrySelectSide(0);
            }
            RefreshShotNameList();
            if (!ShotNameList.Contains(ShotName))
            {
                TrySelectShot(0);
            }
            RefreshDefectNameList();
            if (!DefectNameList.Contains(DefectName))
            {
                TrySelectDefect(0);
            }
            UpdateDataCells();

            return;
        }

        public void SaveDataBaseInfo()
        {
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.Add("ProductName", ProductName);
            xmlParameter.Add("BatchName", BatchName);
            xmlParameter.Add("BoardName", BoardName);
            xmlParameter.Add("SideName", SideName);
            xmlParameter.Add("ShotName", ShotName);
            xmlParameter.Add("DefectName", DefectName);
            xmlParameter.WriteParameter(Application.StartupPath + _fileDataBaseManager);
        }

        private void UpdateDataCells()
        {
            DefectCells = new List<DefectCell>();
            if (_displayWindowNum <= 0)
            {
                return;
            }

            int head = -1;
            int end = -1;
            if (!DefectNameList.Contains(DefectName))
            {
                int iter = _displayWindowNum;
                do
                {
                    DefectCells.Add(new DefectCell());
                } while (--iter > 0);
                return;
            }

            int indexOfGroup = DefectNameList.IndexOf(DefectName) / _displayWindowNum;
            head = indexOfGroup * _displayWindowNum;
            end = head + _displayWindowNum - 1;

            for (var iter = head; iter <= end; ++iter)
            {
                var defectCell = new DefectCell();
                if (null == DefectNameList || 0 == DefectNameList.Count || iter >= DefectNameList.Count || iter < 0)
                {
                    DefectCells.Add(new DefectCell());
                }
                else
                {
                    _device.GetDefectCell(ProductName, BatchName, BoardName, SideName, ShotName, DefectNameList[iter], out defectCell);
                    defectCell.GenRegionFromRect(_dilationPixel);
                    DefectCells.Add(defectCell);
                }
            }

            DisplayWindowIndex = DefectNameList.IndexOf(DefectName) - head;
            DefectRegionIndex = 0;

            return;
        }

        //
        private void ResetProduct()
        {
            ProductName = "";
            _productNameList = new List<string>();
            BatchName = "";
            _batchNameList = new List<string>();
            BoardName = "";
            _boardNameList = new List<string>();
            SideName = "";
            _sideNameList = new List<string>();
            ShotName = "";
            _shotNameList = new List<string>();
            DefectName = "";
            _defectNameList = new List<string>();
            DefectCells = new List<DefectCell>();
            DefectRegionIndex = 0;
            DisplayWindowIndex = 0;

            return;
        }

        private void ResetBatch()
        {
            BatchName = "";
            _batchNameList = new List<string>();
            BoardName = "";
            _boardNameList = new List<string>();
            SideName = "";
            _sideNameList = new List<string>();
            ShotName = "";
            _shotNameList = new List<string>();
            DefectName = "";
            _defectNameList = new List<string>();
            DefectCells = new List<DefectCell>();
            DefectRegionIndex = 0;
            DisplayWindowIndex = 0;

            return;
        }

        private void ResetBoard()
        {
            BoardName = "";
            _boardNameList = new List<string>();
            SideName = "";
            _sideNameList = new List<string>();
            ShotName = "";
            _shotNameList = new List<string>();
            DefectName = "";
            _defectNameList = new List<string>();
            DefectCells = new List<DefectCell>();
            DefectRegionIndex = 0;
            DisplayWindowIndex = 0;

            return;
        }

        private void ResetSide()
        {
            SideName = "";
            _sideNameList = new List<string>();
            ShotName = "";
            _shotNameList = new List<string>();
            DefectName = "";
            _defectNameList = new List<string>();
            DefectCells = new List<DefectCell>();
            DefectRegionIndex = 0;
            DisplayWindowIndex = 0;

            return;
        }

        private void ResetShot()
        {
            ShotName = "";
            _shotNameList = new List<string>();
            DefectName = "";
            _defectNameList = new List<string>();
            DefectCells = new List<DefectCell>();
            DefectRegionIndex = 0;
            DisplayWindowIndex = 0;

            return;
        }

        private void ResetDefect()
        {
            DefectName = "";
            _defectNameList = new List<string>();
            DefectCells = new List<DefectCell>();
            DefectRegionIndex = 0;
            DisplayWindowIndex = 0;

            return;
        }

        //
        private bool RefreshProductNameList()
        {
            return _device.GetProductList(out _productNameList) > 0;
        }

        private bool RefreshBatchNameList()
        {
            return _device.GetBatchList(ProductName, out _batchNameList) > 0;
        }

        private bool RefreshBoardNameList()
        {
            return _device.GetBoardList(ProductName, BatchName, out _boardNameList) > 0;
        }

        private bool RefreshSideNameList()
        {
            return _device.GetSideList(ProductName, BatchName, BoardName, out _sideNameList) > 0;
        }

        private bool RefreshShotNameList()
        {
            return _device.GetShotList(ProductName, BatchName, BoardName, SideName, out _shotNameList) > 0;
        }

        private bool RefreshDefectNameList()
        {
            return _device.GetDefectListInShot(ProductName, BatchName, BoardName, SideName, ShotName, out _defectNameList) > 0;
        }

        //
        private bool TrySelectProduct(int index)
        {
            ResetProduct();
            if (!RefreshProductNameList())
            {
                return false;
            }
            if (index >= ProductNameList.Count || index < 0)
            {
                return false;
            }
            ProductName = ProductNameList[index];

            return true;
        }

        private bool TrySelectBatch(int index)
        {
            ResetBatch();
            if (!RefreshBatchNameList())
            {
                return false;
            }
            if (index >= BatchNameList.Count || index < 0)
            {
                return false;
            }
            BatchName = BatchNameList[index];

            return true;
        }

        private bool TrySelectBoard(int index)
        {
            ResetBoard();
            if (!RefreshBoardNameList())
            {
                return false;
            }
            if (index >= BoardNameList.Count || index < 0)
            {
                return false;
            }
            BoardName = BoardNameList[index];

            return true;
        }

        private bool TrySelectSide(int index)
        {
            ResetSide();
            if (!RefreshSideNameList())
            {
                return false;
            }
            if (index >= SideNameList.Count || index < 0)
            {
                return false;
            }
            SideName = SideNameList[index];

            return true;
        }

        private bool TrySelectShot(int index)
        {
            ResetShot();
            if (!RefreshShotNameList())
            {
                return false;
            }
            if (index >= ShotNameList.Count || index < 0)
            {
                return false;
            }
            ShotName = ShotNameList[index];

            return true;
        }

        private bool TrySelectDefect(int index)
        {
            ResetDefect();
            if (!RefreshDefectNameList())
            {
                return false;
            }
            if (index >= DefectNameList.Count || index < 0)
            {
                return false;
            }
            DefectName = DefectNameList[index];
            UpdateDataCells();

            return true;
        }

        //
        private bool TryGetNextProductNotEmpty(bool isHead = false)
        {
            int index = -1;
            RefreshProductNameList();
            do
            {
                if (!isHead)
                {
                    if (ProductNameList.Contains(ProductName))
                    {
                        index = ProductNameList.IndexOf(ProductName);
                    }
                    else
                    {
                        return false;
                    }
                }

                if (index >= ProductNameList.Count - 1)
                {
                    return false;
                }
                isHead = false;
            } while ((!TrySelectProduct(++index)) || (!TryGetNextBatchNotEmpty(true)));

            return true;
        }

        public bool DecreaseDefectRegionIndex()
        {
            int index = DefectRegionIndex;
            index--;
            if (index < 0)
            {
                return false;
            }
            DefectRegionIndex = index;

            return true;
        }

        public void SetDefectRegionIndexLast()
        {
            DefectRegionIndex = DefectCells[DisplayWindowIndex].DefectRegions.Count - 1;
        }

        public bool IncreaseDefectRegionIndex()
        {
            int index = DefectRegionIndex;
            index++;
            if (index >= DefectCells[DisplayWindowIndex].DefectRegions.Count)
            {
                return false;
            }

            DefectRegionIndex++;

            return true;
        }

        private bool TryGetNextBatchNotEmpty(bool isHead = false)
        {
            int index = -1;
            RefreshBatchNameList();
            do
            {
                if (!isHead)
                {
                    if (BatchNameList.Contains(BatchName))
                    {
                        index = BatchNameList.IndexOf(BatchName);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (index >= BatchNameList.Count - 1)
                {
                    return false;
                }
                isHead = false;
            } while ((!TrySelectBatch(++index)) || (!TryGetNextBoardNotEmpty(true)));

            return true;
        }

        private bool TryGetNextBoardNotEmpty(bool isHead = false)
        {
            int index = -1;
            RefreshBoardNameList();
            do
            {
                if (!isHead)
                {
                    if (BoardNameList.Contains(BoardName))
                    {
                        index = BoardNameList.IndexOf(BoardName);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (index >= BoardNameList.Count - 1)
                {
                    return false;
                }
                isHead = false;
            } while ((!TrySelectBoard(++index)) || (!TryGetNextSideNotEmpty(true)));

            return true;
        }

        private bool TryGetNextSideNotEmpty(bool isHead = false)
        {
            int index = -1;
            RefreshSideNameList();
            do
            {
                if (!isHead)
                {
                    if (SideNameList.Contains(SideName))
                    {
                        index = SideNameList.IndexOf(SideName);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (index >= SideNameList.Count - 1)
                {
                    return false;
                }
                isHead = false;
            } while ((!TrySelectSide(++index)) || (!TryGetNextShotNotEmpty(true)));

            return true;
        }

        private bool TryGetNextShotNotEmpty(bool isHead = false)
        {
            int index = -1;
            RefreshShotNameList();
            do
            {
                if (!isHead)
                {
                    if (ShotNameList.Contains(ShotName))
                    {
                        index = ShotNameList.IndexOf(ShotName);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (index >= ShotNameList.Count - 1)
                {
                    return false;
                }
                isHead = false;
            } while ((!TrySelectShot(++index)) || (!TryGetNextDefectNotEmpty(true)));

            return true;
        }

        private bool TryGetNextDefectNotEmpty(bool isHead = false)
        {
            int index = -1;
            RefreshDefectNameList();
            do
            {
                if (!isHead)
                {
                    if (DefectNameList.Contains(DefectName))
                    {
                        index = DefectNameList.IndexOf(DefectName);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (index >= DefectNameList.Count - 1)
                {
                    return false;
                }
                isHead = false;
            } while (!TrySelectDefect(++index));
            
            return true;
        }

        //
        private bool TryGetPreviousProductNotEmpty(bool isEnd = false)
        {
            RefreshProductNameList();
            int index = ProductNameList.Count;
            do
            {
                if (!isEnd)
                {
                    if (ProductNameList.Contains(ProductName))
                    {
                        index = ProductNameList.IndexOf(ProductName);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (index <= 0)
                {
                    return false;
                }
                isEnd = false;
            } while ((!TrySelectProduct(--index)) || (!TryGetPreviousBatchNotEmpty(true)));

            return true;
        }

        private bool TryGetPreviousBatchNotEmpty(bool isEnd = false)
        {
            RefreshBatchNameList();
            int index = BatchNameList.Count;
            do
            {
                if (!isEnd)
                {
                    if (BatchNameList.Contains(BatchName))
                    {
                        index = BatchNameList.IndexOf(BatchName);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (index <= 0)
                {
                    return false;
                }
                isEnd = false;
            } while ((!TrySelectBatch(--index)) || (!TryGetPreviousBoardNotEmpty(true)));

            return true;
        }

        private bool TryGetPreviousBoardNotEmpty(bool isEnd = false)
        {
            RefreshBoardNameList();
            int index = BoardNameList.Count;
            do
            {
                if (!isEnd)
                {
                    if (BoardNameList.Contains(BoardName))
                    {
                        index = BoardNameList.IndexOf(BoardName);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (index <= 0)
                {
                    return false;
                }
                isEnd = false;
            } while ((!TrySelectBoard(--index)) || (!TryGetPreviousSideNotEmpty(true)));

            return true;
        }

        private bool TryGetPreviousSideNotEmpty(bool isEnd = false)
        {
            RefreshSideNameList();
            int index = SideNameList.Count;
            do
            {
                if (!isEnd)
                {
                    if (SideNameList.Contains(SideName))
                    {
                        index = SideNameList.IndexOf(SideName);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (index <= 0)
                {
                    return false;
                }
                isEnd = false;
            } while ((!TrySelectSide(--index)) || (!TryGetPreviousShotNotEmpty(true)));

            return true;
        }

        private bool TryGetPreviousShotNotEmpty(bool isEnd = false)
        {
            RefreshShotNameList();
            int index = ShotNameList.Count;
            do
            {
                if (!isEnd)
                {
                    if (ShotNameList.Contains(ShotName))
                    {
                        index = ShotNameList.IndexOf(ShotName);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (index <= 0)
                {
                    return false;
                }
                isEnd = false;
            } while ((!TrySelectShot(--index)) || (!TryGetPreviousDefectNotEmpty(true)));

            return true;
        }

        private bool TryGetPreviousDefectNotEmpty(bool isEnd = false)
        {
            RefreshDefectNameList();
            int index = DefectNameList.Count;
            do
            {
                if (!isEnd)
                {
                    if (DefectNameList.Contains(DefectName))
                    {
                        index = DefectNameList.IndexOf(DefectName);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (index <= 0)
                {
                    return false;
                }
                isEnd = false;
            } while (!TrySelectDefect(--index));

            return true;
        }

        public void GetGerberImage(string side, out Bitmap gerberBitmap)
        {
            if (side == "SideA")
            {
                _device.GetGerberWholeImgA(out gerberBitmap);
            }
            else
            {
                _device.GetGerberWholeImgB(out gerberBitmap);
            }
        }
        
        //
        public void SwitchProduct(string productName)
        {
            var index = ProductNameList.IndexOf(productName);
            if (TrySelectProduct(index) && TrySelectBatch(0) && TrySelectBoard(0) && TrySelectSide(0) && TrySelectShot(0) && TrySelectDefect(0))
            {
            }

            return;
        }

        public void SwitchBatch(string batchName)
        {
            var index = BatchNameList.IndexOf(batchName);
            if (TrySelectBatch(index) && TrySelectBoard(0) && TrySelectSide(0) && TrySelectShot(0) && TrySelectDefect(0))
            {
            }

            return;
        }

        public void SwitchBoard(string boardName)
        {
            var index = BoardNameList.IndexOf(boardName);
            if (TrySelectBoard(index) && TrySelectSide(0) && TrySelectShot(0) && TrySelectDefect(0))
            {
            }

            return;
        }

        public void SwitchSide(string sideName)
        {
            var index = SideNameList.IndexOf(sideName);
            if (TrySelectSide(index) && TrySelectShot(0) && TrySelectDefect(0))
            {
            }

            return;
        }

        public void SwitchShot(string shotName)
        {
            var index = ShotNameList.IndexOf(shotName);
            if (TrySelectShot(index) && TrySelectDefect(0))
            {
            }

            return;
        }

        public void SwitchDefect(string defectName)
        {
            var index = DefectNameList.IndexOf(defectName);
            if (TrySelectDefect(index))
            {
            }

            return;
        }

        public bool TrySwitchNextRegion()
        {
            if (TryGetNextDefectNotEmpty() || TryGetNextShotNotEmpty() || TryGetNextSideNotEmpty() || TryGetNextBoardNotEmpty() || TryGetNextBatchNotEmpty() || TryGetNextProductNotEmpty())
            {
                
                return true;
            }

            return false;
        }

        public bool TrySwitchForward()
        {
            if (TryGetPreviousDefectNotEmpty() || TryGetPreviousShotNotEmpty() || TryGetPreviousSideNotEmpty() || TryGetPreviousBoardNotEmpty() || TryGetPreviousBatchNotEmpty() || TryGetPreviousProductNotEmpty())
            {
                return true;
            }

            return false;
        }

        public void SaveMarkInfo(EMarkDataType markType)
        {
            MarkDataInfo markDataInfo = new MarkDataInfo(ProductName, BatchName, BoardName, SideName, ShotName, DefectName);
            _sqliteDb.ReadMarkDataType(ref markDataInfo);
            MarkRegionInfo markRegionInfo = new MarkRegionInfo();
            markRegionInfo.SetByDefectCell(DefectCells[DisplayWindowIndex], DefectRegionIndex, markType);
            markDataInfo.AddMarks(DefectRegionIndex, markRegionInfo);
            _sqliteDb.WriteMarkDataInfo(markDataInfo);
            return;
        }

    }
}
