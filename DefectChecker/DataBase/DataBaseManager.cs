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
        // config files
        private const string _fileProjectSetting = @"\config\ProjectSetting.xml";
        private const string _fileDataBaseManager = @"\config\DataBaseManager.xml";

        private MarkDataBase _markDataBase = new MarkDataBase();
        private DeviceMachVision _device = new DeviceMachVision();

        private string _dataDir;
        private string _modelDir;

        // product dictionary - 1
        private List<string> _productNameList = new List<string>();
        private List<string> _batchNameList = new List<string>();
        private List<string> _boardNameList = new List<string>();
        private List<string> _sideNameList = new List<string>();
        private List<string> _shotNameList = new List<string>();
        private List<string> _defectNameList = new List<string>();

        //// skip
        //private int _indexOfProductNameList = 0;
        //private int _indexOfBatchNameList = 0;
        //private int _indexOfBoardNameList = 0;
        //private int _indexOfSideNameList = 0;
        //private int _indexOfShotNameList = 0;
        //private int _indexOfDefectNameList = 0;

        // product dictionary - 2
        public List<string> ProductNameList { get { return _productNameList; } }
        public List<string> BatchNameList { get { return _batchNameList; } }
        public List<string> BoardNameList { get { return _boardNameList; } }
        public List<string> SideNameList { get { return _sideNameList; } }
        public List<string> ShotNameList { get { return _shotNameList; } }
        public List<string> DefectNameList { get { return _defectNameList; } }

        // product name info
        public string ProductName
        {
            get;
            set;
        }
        public string BatchName
        {
            get;
            set;
        }
        public string BoardName { get; set; }
        public string SideName { get; set; }
        public string ShotName { get; set; }
        public string DefectName { get; set; }

        public DataBaseManager(MarkDataBase markDataBase)
        {
            _markDataBase = markDataBase;
            Init();
        }

        private void ClearDataBaseInfo()
        {
            ProductName = "";
            BatchName = "";
            BoardName = "";
            SideName = "";
            ShotName = "";
            DefectName = "";

            _productNameList = new List<string>();
            _batchNameList = new List<string>();
            _boardNameList = new List<string>();
            _sideNameList = new List<string>();
            _shotNameList = new List<string>();
            _defectNameList = new List<string>();
        }

        private void Init()
        {
            LoadProjectSetting();
            LoadDataBaseInfo();
            SaveDataBaseInfo();
        }

        private void LoadProjectSetting()
        {
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.ReadParameter(Application.StartupPath + _fileProjectSetting);
            _dataDir = xmlParameter.GetParamData("DataDir");
            _modelDir = xmlParameter.GetParamData("ModelDir");
        }

        private void LoadDataBaseInfo()
        {
            ClearDataBaseInfo();
            _device.SetDataDir(_modelDir, _dataDir);

            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.ReadParameter(Application.StartupPath + _fileDataBaseManager);

            ProductName = xmlParameter.GetParamData("ProductName");
            BatchName = xmlParameter.GetParamData("BatchName");
            BoardName = xmlParameter.GetParamData("BoardName");
            SideName = xmlParameter.GetParamData("SideName");
            ShotName = xmlParameter.GetParamData("ShotName");
            DefectName = xmlParameter.GetParamData("DefectName");

            if (string.IsNullOrWhiteSpace(ProductName))
            {
                SelectProduct("", true);
            }
            else
            {
               SelectProduct(ProductName, false);
            }

            return;
        }

        private void SaveDataBaseInfo()
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

            return;
        }

        private void ResetShot()
        {
            ShotName = "";
            _shotNameList = new List<string>();
            DefectName = "";
            _defectNameList = new List<string>();

            return;
        }

        private void ResetDefect()
        {
            DefectName = "";
            _defectNameList = new List<string>();

            return;
        }

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


        public bool SelectProduct(string productName, bool isChooseFirst)
        {
            ResetProduct();
            if (!UpdateProductName(productName, isChooseFirst))
            {
                return false;
            }

            return SelectBatch("", true);
        }

        public bool SelectBatch(string batchName, bool isChooseFirst)
        {
            ResetBatch();
            if (!UpdateBatchName(batchName, isChooseFirst))
            {
                return false;
            }

            return SelectBoard("", true);
        }

        public bool SelectBoard(string boardName, bool isChooseFirst)
        {
            ResetBoard();
            if (!UpdateBoardName(boardName, isChooseFirst))
            {
                return false;
            }

            return SelectSide("", true);
        }

        public bool SelectSide(string sideName, bool isChooseFirst)
        {
            ResetSide();
            if (!UpdateSideName(sideName, isChooseFirst))
            {
                return false;
            }

            return SelectShot("", true);
        }

        public bool SelectShot(string shotName, bool isChooseFirst)
        {
            ResetShot();
            if (!UpdateShotName(shotName, isChooseFirst))
            {
                return false;
            }

            return SelectDefect("", true);
        }

        public bool SelectDefect(string defectName, bool isChooseFirst)
        {
            ResetDefect();
            if (!UpdateDefectName(defectName, isChooseFirst))
            {
                return false;
            }

            return true;
        }

        
        

        private bool UpdateProductName(string productName, bool isChooseFirst = false)
        {
            if (!RefreshProductNameList())
            {
                return false;
            }
            if (isChooseFirst)
            {
                ProductName = ProductNameList[0];
                return true;
            }
            if (ProductNameList.Contains(productName))
            {
                ProductName = productName;
                return true;
            }

            return false;
        }

        private bool UpdateBatchName(string batchName, bool isChooseFirst = false)
        {
            if (!RefreshBatchNameList())
            {
                return false;
            }

            if (isChooseFirst)
            {
                BatchName = BatchNameList[0];
                return true;
            }
            if (BatchNameList.Contains(batchName))
            {
                BatchName = batchName;
                return true;
            }

            return false;
        }

        private bool UpdateBoardName(string boardName, bool isChooseFirst = false)
        {
            if (!RefreshBoardNameList())
            {
                return false;
            }

            if (isChooseFirst)
            {
                BoardName = BoardNameList[0];
                return true;
            }
            if (BoardNameList.Contains(boardName))
            {
                BoardName = boardName;
                return true;
            }

            return false;
        }

        private bool UpdateSideName(string sideName, bool isChooseFirst)
        {
            if (!RefreshSideNameList())
            {
                return false;
            }

            if (isChooseFirst)
            {
                SideName = SideNameList[0];
                return true;
            }
            if (SideNameList.Contains(sideName))
            {
                SideName = sideName;
                return true;
            }

            return false;
        }

        private bool UpdateShotName(string shotName, bool isChooseFirst)
        {
            if (!RefreshShotNameList())
            {
                return false;
            }

            if (isChooseFirst)
            {
                ShotName = ShotNameList[0];
                return true;
            }
            if (ShotNameList.Contains(shotName))
            {
                ShotName = shotName;
                return true;
            }

            return false;
        }

        private bool UpdateDefectName(string defectName, bool isChooseFirst)
        {
            if (!RefreshDefectNameList())
            {
                return false;
            }

            if (isChooseFirst)
            {
                DefectName = DefectNameList[0];
                return true;
            }
            if (DefectNameList.Contains(defectName))
            {
                DefectName = defectName;
                return true;
            }

            return false;
        }

    }
}
