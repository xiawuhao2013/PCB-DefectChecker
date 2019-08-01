using Aqrose.Framework.Utility.Tools;
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
        private const string _paramFileName = @"\ParamFile.xml";
        private List<string> _defectNameList = new List<string>();
        private List<string> _shotNameList = new List<string>();
        private List<string> _sideNameList = new List<string>();
        private List<string> _boardNameList = new List<string>();
        private List<string> _batchNameList = new List<string>();
        private List<string> _productNameList = new List<string>();
        private string _dataDir = "";
        private string _modelDir = "";
        private string _side = "";
        private string _shot = "";
        private string _defect = "";
        private int _indexOfDefectGroup = 0;
        private int _indexOfShot = 0;
        private int _indexOfSide = 0;
        private int _indexOfBoard = 0;
        private int _indexOfBatch = 0;

        private bool _hasKilledDefect = false;
        private bool _hasKilledShot = false;
        private bool _hasKilledSide = false;
        private bool _hasKilledBoard = false;
        private bool _hasKilledBatch = false;
        private bool _hasKilledProduct = false;

        private DeviceMachVision _machVision = new DeviceMachVision();

        public List<string> DefectNameList { get { return _defectNameList; } }
        public List<string> ShotNameList { get { return _shotNameList; } }
        public List<string> SideNameList { get { return _sideNameList; } }
        public List<string> BoardNameList { get { return _boardNameList; } }
        public List<string> BatchNameList { get { return _batchNameList; } }
        public List<string> ProductNameList { get { return _productNameList; } }

        public List<string> ImageNameList { get; set; }
        public string Product { get; set; }
        public string Batch { get; set; }
        public string Board { get; set; }
        public List<DefectCell> DefectCellList {get;set;}
        public Bitmap WholeImageA { get; set; }
        public Bitmap WholeImageB { get; set; }
        public Bitmap GerberWholeImageA { get; set; }
        public Bitmap GerberWholeImageB { get; set; }

        public bool HasKilledDefect { get { return _hasKilledDefect; } }
        public bool HasKilledShot { get { return _hasKilledShot; } }
        public bool HasKilledSide { get { return _hasKilledSide; } }
        public bool HasKilledBoard { get { return _hasKilledBoard; } }
        public bool HasKilledBatch { get { return _hasKilledBatch; } }
        public bool HasKilledProduct { get { return _hasKilledProduct; } }

        //

        public DataBaseManager()
        {
            LoadConfig();
            //
            LoadProductList();
            if (String.IsNullOrWhiteSpace(Product) && null != ProductNameList && 0 != ProductNameList.Count)
            {
                Product = ProductNameList[0];
            }
            LoadBatchList();
            if (String.IsNullOrWhiteSpace(Batch) && null != BatchNameList && 0 != BatchNameList.Count)
            {
                Batch = BatchNameList[0];
            }
            LoadBoardList();
            if (String.IsNullOrWhiteSpace(Board) && null != BoardNameList && 0 != BoardNameList.Count)
            {
                Board = BoardNameList[0];
            }
            LoadSideList();
            if (String.IsNullOrWhiteSpace(_side) && null != _sideNameList && 0 != _sideNameList.Count)
            {
                _side = _sideNameList[0];
            }
            LoadShotList();
            if (String.IsNullOrWhiteSpace(_shot) && null!= _shotNameList && 0 != _shotNameList.Count)
            {
                _shot = _shotNameList[0];
            }
            LoadCellList();
            if (String.IsNullOrWhiteSpace(_defect) && null != _defectNameList && 0 != _defectNameList.Count)
            {
                _defect = _defectNameList[0];
            }

        }

        //

        private void LoadConfig()
        {
            // LABEL: bug exists. need ensurance of the name match with index.
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.ReadParameter(Application.StartupPath + _paramFileName);
            _dataDir = xmlParameter.GetParamData("DataDir");
            _modelDir = xmlParameter.GetParamData("ModelDir");
            Product = xmlParameter.GetParamData("Product");
            Batch = xmlParameter.GetParamData("Batch");
            Board = xmlParameter.GetParamData("Board");
            _side = xmlParameter.GetParamData("Side");
            _shot = xmlParameter.GetParamData("Shot");
            _defect = xmlParameter.GetParamData("Defect");
            _indexOfDefectGroup = String.IsNullOrWhiteSpace(xmlParameter.GetParamData("IndexOfDefectGroup")) ? 0 : Convert.ToInt32(xmlParameter.GetParamData("IndexOfDefectGroup"));
            _indexOfShot = String.IsNullOrWhiteSpace(xmlParameter.GetParamData("IndexOfShot")) ? 0 : Convert.ToInt32(xmlParameter.GetParamData("IndexOfShot"));
            _indexOfSide = String.IsNullOrWhiteSpace(xmlParameter.GetParamData("IndexOfSide")) ? 0 : Convert.ToInt32(xmlParameter.GetParamData("IndexOfSide"));
            _indexOfBoard = String.IsNullOrWhiteSpace(xmlParameter.GetParamData("IndexOfBoard")) ? 0 : Convert.ToInt32(xmlParameter.GetParamData("IndexOfBoard"));
            _indexOfBatch = String.IsNullOrWhiteSpace(xmlParameter.GetParamData("IndexOfBatch")) ? 0 : Convert.ToInt32(xmlParameter.GetParamData("IndexOfBatch"));

            return;
        }

        private void LoadProductList()
        {
            _machVision.GetProductList(out _productNameList);

            return;
        }

        private void LoadBatchList()
        {
            _machVision.GetBatchList(Product, out _batchNameList);

            return;
        }

        private void LoadBoardList()
        {
            _machVision.GetBoardList(Product, Batch, out _boardNameList);

            return;
        }

        private void LoadSideList()
        {
            _machVision.GetSideList(Product, Batch, Board, out _sideNameList);

            return;
        }

        private void LoadShotList()
        {
            _machVision.GetShotList(Product, Batch, Board, _side, out _shotNameList);

            return;
        }

        private void LoadCellList()
        {
            _machVision.GetDefectListInShot(Product, Batch, Board, _side, _shot, out _defectNameList);

            return;
        }

        private DefectCell LoadDefectCell()
        {
            _machVision.GetDefectCell(Product, Batch, Board, _side, _shot, _defect, out var defectCell);

            return defectCell;
        }

        private void RecordMarkResult() { }
        private void RemoveMarkResult() { }

        //
        public bool TrySelectCell(int index, out DefectCell defectCell)
        {
            defectCell = new DefectCell();
            if (null == DefectNameList || 0 == DefectNameList.Count)
            {
                return false;
            }
            if (index >= DefectNameList.Count || index < 0)
            {
                return false;
            }
            _defect = DefectNameList[index];
            defectCell = LoadDefectCell();

            return true;
        }
        // LABEL: refrence these codes temporary.
        /*
        public bool TryGetNextCell(out DefectCell defectCell, out bool isEnd)
        {
            defectCell = new DefectCell();
            isEnd = false;
            if (null == _defectNameList || 0 == _defectNameList.Count)
            {
                return false;
            }
            var index = _defectNameList.FindIndex(x => x.Contains(_defect));
            if (-1 == index)
            {
                return false;
            }
            if (++index >= _defectNameList.Count)
            {
                isEnd = true;

                return false;
            }
            if (!TrySelectCell(index, out defectCell))
            {
                return false;
            }

            return true;
        }
        public bool TryGetPreviousCell(out DefectCell defectCell, out bool isFirst)
        {
            defectCell = new DefectCell();
            isFirst = false;
            if (null == _defectNameList || 0 == _defectNameList.Count)
            {
                return false;
            }
            var index = _defectNameList.FindIndex(x => x.Contains(_defect));
            if (-1 == index)
            {
                return false;
            }
            if (--index < 0)
            {
                isFirst = true;

                return false;
            }
            if (!TrySelectCell(index, out defectCell))
            {
                return false;
            }

            return true;
        }
        */
        //
        public bool TrySelectDefectGroup(int index, int num, out List<DefectCell> defectCells)
        {
            DefectCell defectCell = new DefectCell();
            defectCells = new List<DefectCell>();
            var nullCount = 0;
            for (var iter = 0; iter < num; ++iter)
            {
                if (TrySelectCell(_indexOfDefectGroup * num + iter, out defectCell))
                {
                    defectCells.Add(defectCell);
                }
                else
                {
                    ++nullCount;
                    defectCells.Add(new DefectCell());
                }
            }
            _hasKilledDefect = nullCount != num;

            return _hasKilledDefect;
        }
        public bool TryGetDefectGroupOfLastExit(int num, out List<DefectCell> defectCells)
        {
            return TrySelectDefectGroup(_indexOfDefectGroup, num, out defectCells);
        }
        public bool TryGetFirstDefectGroup(int num, out List<DefectCell> defectCells)
        {
            _indexOfDefectGroup = 0;

            return TrySelectDefectGroup(_indexOfDefectGroup, num, out defectCells);
        }
        public bool TryGetLastDefectGroup(int num, out List<DefectCell> defectCells)
        {
            _indexOfDefectGroup = DefectNameList.Count / num;

            return TrySelectDefectGroup(_indexOfDefectGroup, num, out defectCells);
        }
        public bool TryGetNextDefectGroup(int num, out List<DefectCell> defectCells)
        {
            if (!TrySelectDefectGroup(++_indexOfDefectGroup, num, out defectCells))
            {
                --_indexOfDefectGroup;

                return false;
            }

            return true;
        }
        public bool TryGetPreviousDefectGroup(int num, out List<DefectCell> defectCells)
        {
            if (!TrySelectDefectGroup(--_indexOfDefectGroup, num, out defectCells))
            {
                ++_indexOfDefectGroup;

                return false;
            }

            return true;
        }
        //
        public bool TrySelectShot(int index)
        {
            _hasKilledShot = false;
            if (null == ShotNameList || 0 == ShotNameList.Count)
            {
                return false;
            }
            if (index >= ShotNameList.Count || index < 0)
            {
                _hasKilledShot = true;

                return false;
            }
            _shot = ShotNameList[index];
            LoadCellList();

            return true;
        }
        public bool TryGetShotOfLastExit()
        {
            return TrySelectShot(_indexOfShot);
        }
        public bool TryGetFirstShot()
        {
            _indexOfShot = 0;

            return TrySelectShot(_indexOfShot);
        }
        public bool TryGetLastShot()
        {
            _indexOfShot = Math.Max(ShotNameList.Count - 1, 0);

            return TrySelectShot(_indexOfShot);
        }
        public bool TryGetNextShot()
        {
            if (!TrySelectShot(++_indexOfShot))
            {
                --_indexOfShot;

                return false;
            }

            return true;
        }
        public bool TryGetPreviousShot()
        {
            if (!TrySelectShot(--_indexOfShot))
            {
                ++_indexOfShot;

                return false;
            }

            return true;
        }
        //
        public bool TrySelectSide(int index)
        {
            _hasKilledSide = false;
            if (null == SideNameList || 0 == SideNameList.Count)
            {
                return false;
            }
            if (index >= SideNameList.Count || index < 0)
            {
                _hasKilledSide = true;

                return false;
            }
            _side = SideNameList[index];
            LoadShotList();

            return true;
        }
        public bool TryGetSideOfLastExit()
        {
            return TrySelectSide(_indexOfSide);
        }
        public bool TryGetFirstSide()
        {
            _indexOfSide = 0;

            return TrySelectSide(_indexOfSide);
        }
        public bool TryGetLastSide()
        {
            _indexOfSide = Math.Max(SideNameList.Count - 1, 0);

            return TrySelectSide(_indexOfSide);
        }
        public bool TryGetNextSide()
        {
            if (!TrySelectSide(++_indexOfSide))
            {
                --_indexOfSide;

                return false;
            }

            return true;
        }
        public bool TryGetPreviousSide()
        {
            if (!TrySelectSide(--_indexOfSide))
            {
                ++_indexOfSide;

                return false;
            }

            return true;
        }
        //
        public bool TrySelectBoard(int index)
        {
            _hasKilledBoard = false;
            if (null == BoardNameList || 0 == BoardNameList.Count)
            {
                return false;
            }
            if (index >= BoardNameList.Count || index < 0)
            {
                _hasKilledBoard = true;

                return false;
            }
            Board = BoardNameList[index];
            LoadSideList();

            return true;
        }
        public bool TryGetBoardOfLastExit()
        {
            return TrySelectBoard(_indexOfBoard);
        }
        public bool TryGetFirstBoard()
        {
            _indexOfBoard = 0;

            return TrySelectBoard(_indexOfBoard);
        }
        public bool TryGetLastBoard()
        {
            _indexOfBoard = Math.Max(BoardNameList.Count - 1, 0);

            return TrySelectBoard(_indexOfBoard);
        }
        public bool TryGetNextBoard()
        {
            if (!TrySelectBoard(++_indexOfBoard))
            {
                --_indexOfBoard;

                return false;
            }

            return true;
        }
        public bool TryGetPreviousBoard()
        {
            if (!TrySelectBoard(--_indexOfBoard))
            {
                ++_indexOfBoard;

                return false;
            }

            return true;
        }
        //
        public bool TrySelectBatch(int index)
        {
            _hasKilledBatch = false;
            if (null == BatchNameList || 0 == BatchNameList.Count)
            {
                return false;
            }
            if (index >= BatchNameList.Count || index < 0)
            {
                _hasKilledBatch = true;

                return false;
            }
            Batch = BatchNameList[index];
            LoadBoardList();

            return true;
        }
        public bool TryGetBatchOfLastExit()
        {
            return TrySelectBatch(_indexOfBatch);
        }
        public bool TryGetFirstBatch()
        {
            _indexOfBatch = 0;

            return TrySelectBatch(_indexOfBatch);
        }
        public bool TryGetLastBatch()
        {
            _indexOfBatch = Math.Max(BatchNameList.Count - 1, 0);

            return TrySelectBatch(_indexOfBatch);
        }
        public bool TryGetNextBatch()
        {
            if (!TrySelectBatch(++_indexOfBatch))
            {
                --_indexOfBatch;

                return false;
            }

            return true;
        }
        public bool TryGetPreviousBatch()
        {
            if (!TrySelectBatch(--_indexOfBatch))
            {
                ++_indexOfBatch;

                return false;
            }

            return true;
        }
        //
        public void GetDefectInfo() { }
        public void Mark(int type) { }
        //
        public void SaveConfig()
        {
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.Add("DataDir", _dataDir);
            xmlParameter.Add("ModelDir", _modelDir);
            xmlParameter.Add("Product", Product);
            xmlParameter.Add("Batch", Batch);
            xmlParameter.Add("Board", Board);
            xmlParameter.Add("Side", _side);
            xmlParameter.Add("Shot", _shot);
            xmlParameter.Add("Defect", _defect);
            xmlParameter.Add("IndexOfDefectGroup", _indexOfDefectGroup);
            xmlParameter.Add("IndexOfShot", _indexOfShot);
            xmlParameter.Add("IndexOfSide", _indexOfSide);
            xmlParameter.Add("IndexOfBoard", _indexOfBoard);
            xmlParameter.Add("IndexOfBatch", _indexOfBatch);

            xmlParameter.WriteParameter(Application.StartupPath + _paramFileName);

            return;
        }

    }
}
