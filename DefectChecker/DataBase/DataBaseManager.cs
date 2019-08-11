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
        public MarkDataBase DataBase { get; set; }

        public DataBaseManager()
        {
            DataBase = new MarkDataBase();
        }

        //private const string _paramFileOfDataBaseManager = @"\ParamFile-DataBaseManager.xml";
        //private const string _paramFileOfSetting= @"\ParamFile-Setting.xml";
        //private DeviceMachVision _machVision = new DeviceMachVision();
        //// image-1
        //private List<DefectCell> _defectCellList = new List<DefectCell>();
        //private Bitmap _wholeImageA = null;
        //private Bitmap _wholeImageB = null;
        //private Bitmap _gerberWholeImageA = null;
        //private Bitmap _gerberWholeImageB = null;
        //// name-1

        //private string _dataDir = "";
        //private string _modelDir = "";
        //private string _defect = "";
        //private string _shot = "";
        //private string _side = "";
        //private string _board = "";
        //private string _batch = "";
        //private string _product = "";
        //// skip-1
        //private bool _hasKilledDefect = false;
        //private bool _hasKilledShot = false;
        //private bool _hasKilledSide = false;
        //private bool _hasKilledBoard = false;
        //private bool _hasKilledBatch = false;
        //private bool _hasKilledProduct = false;

        //// skip-2
        //public int IndexOfDefect { get; set; } = 0;
        //public int IndexOfDefectGroup { get; set; } = 0;
        //public int IndexOfShot { get; set; } = 0;
        //public int IndexOfSide { get; set; } = 0;
        //public int IndexOfBoard { get; set; } = 0;
        //public int IndexOfBatch { get; set; } = 0;
        //public int IndexOfProduct { get; set; } = 0;
        //// image-2
        //public List<DefectCell> DefectCellList { get { return _defectCellList; } }
        //public Bitmap WholeImageA { get { return _wholeImageA; } }
        //public Bitmap WholeImageB { get { return _wholeImageB; } }
        //public Bitmap GerberWholeImageA { get { return _gerberWholeImageA; } }
        //public Bitmap GerberWholeImageB { get { return _gerberWholeImageB; } }
        //// name-2
        //public List<string> DefectNameList { get { return _defectNameList; } }
        //public List<string> ShotNameList { get { return _shotNameList; } }
        //public List<string> SideNameList { get { return _sideNameList; } }
        //public List<string> BoardNameList { get { return _boardNameList; } }
        //public List<string> BatchNameList { get { return _batchNameList; } }
        //public List<string> ProductNameList { get { return _productNameList; } }
        //public string Defect { get { return _defect; } }
        //public string Shot { get { return _shot; } }
        //public string Side { get { return _side; } }
        //public string Board { get { return _board; } }
        //public string Batch { get { return _batch; } }
        //public string Product { get { return _product; } }
        //// skip-3
        //public bool HasKilledDefect { get { return _hasKilledDefect; } }
        //public bool HasKilledShot { get { return _hasKilledShot; } }
        //public bool HasKilledSide { get { return _hasKilledSide; } }
        //public bool HasKilledBoard { get { return _hasKilledBoard; } }
        //public bool HasKilledBatch { get { return _hasKilledBatch; } }
        //public bool HasKilledProduct { get { return _hasKilledProduct; } }

        ////

        //public DataBaseManager()
        //{

        //    //LoadConfig();
        //    ////
        //    //LoadProductList();
        //    //if (String.IsNullOrWhiteSpace(Product) && null != ProductNameList && 0 != ProductNameList.Count)
        //    //{
        //    //    _product = ProductNameList[0];
        //    //}
        //    //LoadBatchList();
        //    //if (String.IsNullOrWhiteSpace(Batch) && null != BatchNameList && 0 != BatchNameList.Count)
        //    //{
        //    //    _batch = BatchNameList[0];
        //    //}
        //    //LoadBoardList();
        //    //if (String.IsNullOrWhiteSpace(Board) && null != BoardNameList && 0 != BoardNameList.Count)
        //    //{
        //    //    _board = BoardNameList[0];
        //    //}
        //    //LoadSideList();
        //    //if (String.IsNullOrWhiteSpace(_side) && null != SideNameList && 0 != SideNameList.Count)
        //    //{
        //    //    _side = SideNameList[0];
        //    //}
        //    //LoadShotList();
        //    //if (String.IsNullOrWhiteSpace(_shot) && null!= ShotNameList && 0 != ShotNameList.Count)
        //    //{
        //    //    _shot = ShotNameList[0];
        //    //}
        //    //LoadCellList();
        //    //if (String.IsNullOrWhiteSpace(_defect) && null != DefectNameList && 0 != DefectNameList.Count)
        //    //{
        //    //    _defect = DefectNameList[0];
        //    //}

        //}

        ////

        //private void LoadConfig()
        //{
        //    // LABEL: bug exists. need ensurance of the name match with index.
        //    XmlParameter xmlParameter = new XmlParameter();
        //    xmlParameter.ReadParameter(Application.StartupPath + _paramFileOfSetting);
        //    _dataDir = xmlParameter.GetParamData("DataDir");
        //    _modelDir = xmlParameter.GetParamData("ModelDir");

        //    xmlParameter.ReadParameter(Application.StartupPath + _paramFileOfDataBaseManager);
        //    _product = xmlParameter.GetParamData("Product");
        //    _batch = xmlParameter.GetParamData("Batch");
        //    _board = xmlParameter.GetParamData("Board");
        //    _side = xmlParameter.GetParamData("Side");
        //    _shot = xmlParameter.GetParamData("Shot");
        //    _defect = xmlParameter.GetParamData("Defect");
        //    IndexOfDefect = String.IsNullOrWhiteSpace(xmlParameter.GetParamData("IndexOfDefect")) ? 0 : Convert.ToInt32(xmlParameter.GetParamData("IndexOfDefect"));
        //    IndexOfDefectGroup = String.IsNullOrWhiteSpace(xmlParameter.GetParamData("IndexOfDefectGroup")) ? 0 : Convert.ToInt32(xmlParameter.GetParamData("IndexOfDefectGroup"));
        //    IndexOfShot = String.IsNullOrWhiteSpace(xmlParameter.GetParamData("IndexOfShot")) ? 0 : Convert.ToInt32(xmlParameter.GetParamData("IndexOfShot"));
        //    IndexOfSide = String.IsNullOrWhiteSpace(xmlParameter.GetParamData("IndexOfSide")) ? 0 : Convert.ToInt32(xmlParameter.GetParamData("IndexOfSide"));
        //    IndexOfBoard = String.IsNullOrWhiteSpace(xmlParameter.GetParamData("IndexOfBoard")) ? 0 : Convert.ToInt32(xmlParameter.GetParamData("IndexOfBoard"));
        //    IndexOfBatch = String.IsNullOrWhiteSpace(xmlParameter.GetParamData("IndexOfBatch")) ? 0 : Convert.ToInt32(xmlParameter.GetParamData("IndexOfBatch"));

        //    return;
        //}

        //private void LoadProductList()
        //{
        //    _machVision.GetProductList(out _productNameList);

        //    return;
        //}

        //private void LoadBatchList()
        //{
        //    _machVision.GetBatchList(Product, out _batchNameList);

        //    return;
        //}

        //private void LoadBoardList()
        //{
        //    _machVision.GetBoardList(Product, Batch, out _boardNameList);

        //    return;
        //}

        //private void LoadSideList()
        //{
        //    _machVision.GetSideList(Product, Batch, Board, out _sideNameList);

        //    return;
        //}

        //private void LoadShotList()
        //{
        //    _machVision.GetShotList(Product, Batch, Board, _side, out _shotNameList);

        //    return;
        //}

        //private void LoadCellList()
        //{
        //    _machVision.GetDefectListInShot(Product, Batch, Board, _side, _shot, out _defectNameList);

        //    return;
        //}

        //private DefectCell LoadDefectCell()
        //{
        //    _machVision.GetDefectCell(Product, Batch, Board, _side, _shot, _defect, out var defectCell);

        //    return defectCell;
        //}

        //private void RecordMarkResult() { }
        //private void RemoveMarkResult() { }

        ////
        //public bool TrySelectCell(int index, out DefectCell defectCell)
        //{
        //    defectCell = new DefectCell();
        //    if (null == DefectNameList || 0 == DefectNameList.Count || index >= DefectNameList.Count || index < 0)
        //    {
        //        _defect = "";

        //        return false;
        //    }
        //    _defect = DefectNameList[index];
        //    defectCell = LoadDefectCell();

        //    return true;
        //}
        //// LABEL: refrence these codes temporary.
        ///*
        //public bool TryGetNextCell(out DefectCell defectCell, out bool isEnd)
        //{
        //    defectCell = new DefectCell();
        //    isEnd = false;
        //    if (null == _defectNameList || 0 == _defectNameList.Count)
        //    {
        //        return false;
        //    }
        //    var index = _defectNameList.FindIndex(x => x.Contains(_defect));
        //    if (-1 == index)
        //    {
        //        return false;
        //    }
        //    if (++index >= _defectNameList.Count)
        //    {
        //        isEnd = true;

        //        return false;
        //    }
        //    if (!TrySelectCell(index, out defectCell))
        //    {
        //        return false;
        //    }

        //    return true;
        //}
        //public bool TryGetPreviousCell(out DefectCell defectCell, out bool isFirst)
        //{
        //    defectCell = new DefectCell();
        //    isFirst = false;
        //    if (null == _defectNameList || 0 == _defectNameList.Count)
        //    {
        //        return false;
        //    }
        //    var index = _defectNameList.FindIndex(x => x.Contains(_defect));
        //    if (-1 == index)
        //    {
        //        return false;
        //    }
        //    if (--index < 0)
        //    {
        //        isFirst = true;

        //        return false;
        //    }
        //    if (!TrySelectCell(index, out defectCell))
        //    {
        //        return false;
        //    }

        //    return true;
        //}
        //*/
        ////
        //public bool TrySelectDefectGroup(int index, int num, out List<DefectCell> defectCells)
        //{
        //    DefectCell defectCell = new DefectCell();
        //    defectCells = new List<DefectCell>();
        //    var nullCount = 0;
        //    for (var iter = 0; iter < num; ++iter)
        //    {
        //        if (TrySelectCell(IndexOfDefectGroup * num + iter, out defectCell))
        //        {
        //            defectCells.Add(defectCell);
        //        }
        //        else
        //        {
        //            ++nullCount;
        //            defectCells.Add(new DefectCell());
        //        }
        //    }
        //    _hasKilledDefect = nullCount == num;

        //    return nullCount != num;
        //}
        //public void SetIndexOfDefectGroup(int index)
        //{
        //    IndexOfDefectGroup = index;

        //    return;
        //}
        //public bool TryGetNextDefectGroup(int num, out List<DefectCell> defectCells)
        //{
        //    if (!TrySelectDefectGroup(++IndexOfDefectGroup, num, out defectCells))
        //    {
        //        --IndexOfDefectGroup;

        //        return false;
        //    }

        //    return true;
        //}
        //public bool TryGetPreviousDefectGroup(int num, out List<DefectCell> defectCells)
        //{
        //    if (!TrySelectDefectGroup(--IndexOfDefectGroup, num, out defectCells))
        //    {
        //        ++IndexOfDefectGroup;

        //        return false;
        //    }

        //    return true;
        //}
        ////
        //public bool TrySelectShot(int index)
        //{
        //    _shot = "";
        //    _shotNameList = new List<string>();
        //    _hasKilledShot = false;

        //    IndexOfShot = index;
        //    if (null == ShotNameList || 0 == ShotNameList.Count || index >= ShotNameList.Count || index < 0)
        //    {
        //        _hasKilledShot = true;

        //        return false;
        //    }
        //    _shot = ShotNameList[index];
        //    LoadCellList();

        //    return true;
        //}
        //public bool TryGetNextShot()
        //{
        //    if (!TrySelectShot(++IndexOfShot))
        //    {
        //        --IndexOfShot;

        //        return false;
        //    }

        //    return true;
        //}
        //public bool TryGetPreviousShot()
        //{
        //    if (!TrySelectShot(--IndexOfShot))
        //    {
        //        ++IndexOfShot;

        //        return false;
        //    }

        //    return true;
        //}
        ////
        //public bool TrySelectSide(int index)
        //{
        //    _side = "";
        //    _sideNameList = new List<string>();
        //    _hasKilledSide = false;

        //    IndexOfSide = index;
        //    if (null == SideNameList || 0 == SideNameList.Count || index >= SideNameList.Count || index < 0)
        //    {
        //        _hasKilledSide = true;

        //        return false;
        //    }
        //    _side = SideNameList[index];
        //    LoadShotList();

        //    return true;
        //}
        //public bool TryGetNextSide()
        //{
        //    if (!TrySelectSide(++IndexOfSide))
        //    {
        //        --IndexOfSide;

        //        return false;
        //    }

        //    return true;
        //}
        //public bool TryGetPreviousSide()
        //{
        //    if (!TrySelectSide(--IndexOfSide))
        //    {
        //        ++IndexOfSide;

        //        return false;
        //    }

        //    return true;
        //}
        ////
        //public bool TrySelectBoard(int index)
        //{
        //    _board = "";
        //    _boardNameList = new List<string>();
        //    _hasKilledBoard = false;

        //    IndexOfBoard = index;
        //    if (null == BoardNameList || 0 == BoardNameList.Count || index >= BoardNameList.Count || index < 0)
        //    {
        //        _hasKilledBoard = true;

        //        return false;
        //    }
        //    _board = BoardNameList[index];
        //    LoadSideList();

        //    return true;
        //}
        //public bool TryGetNextBoard()
        //{
        //    if (!TrySelectBoard(++IndexOfBoard))
        //    {
        //        --IndexOfBoard;

        //        return false;
        //    }

        //    return true;
        //}
        //public bool TryGetPreviousBoard()
        //{
        //    if (!TrySelectBoard(--IndexOfBoard))
        //    {
        //        ++IndexOfBoard;

        //        return false;
        //    }

        //    return true;
        //}
        ////
        //public bool TrySelectBatch(int index)
        //{
        //    _batch = "";
        //    _boardNameList = new List<string>();
        //    _hasKilledBatch = false;

        //    IndexOfBatch = index;
        //    if (null == BatchNameList || 0 == BatchNameList.Count || index >= BatchNameList.Count || index < 0)
        //    {
        //        _hasKilledBatch = true;

        //        return false;
        //    }
        //    _batch = BatchNameList[index];
        //    LoadBoardList();

        //    return true;
        //}
        //public bool TryGetNextBatch()
        //{
        //    if (!TrySelectBatch(++IndexOfBatch))
        //    {
        //        --IndexOfBatch;

        //        return false;
        //    }

        //    return true;
        //}
        //public bool TryGetPreviousBatch()
        //{
        //    if (!TrySelectBatch(--IndexOfBatch))
        //    {
        //        ++IndexOfBatch;

        //        return false;
        //    }

        //    return true;
        //}
        ////
        //public void GetDefectInfo() { }

        //public void Mark(int type)
        //{

        //}
        ////
        //public void SaveConfig()
        //{
        //    XmlParameter xmlParameter = new XmlParameter();
        //    xmlParameter.Add("Product", Product);
        //    xmlParameter.Add("Batch", Batch);
        //    xmlParameter.Add("Board", Board);
        //    xmlParameter.Add("Side", _side);
        //    xmlParameter.Add("Shot", _shot);
        //    xmlParameter.Add("Defect", _defect);
        //    xmlParameter.Add("IndexOfDefect", IndexOfDefect);
        //    xmlParameter.Add("IndexOfDefectGroup", IndexOfDefectGroup);
        //    xmlParameter.Add("IndexOfShot", IndexOfShot);
        //    xmlParameter.Add("IndexOfSide", IndexOfSide);
        //    xmlParameter.Add("IndexOfBoard", IndexOfBoard);
        //    xmlParameter.Add("IndexOfBatch", IndexOfBatch);

        //    xmlParameter.WriteParameter(Application.StartupPath + _paramFileOfDataBaseManager);

        //    return;
        //}

    }
}
