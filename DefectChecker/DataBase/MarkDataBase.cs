using Aqrose.Framework.Utility.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DefectChecker.DeviceModule;
using DefectChecker.DeviceModule.MachVision;

namespace DefectChecker.DataBase
{
    public class MarkDataBase
    {
        private const string _fileProjectSetting = @"\config\ProjectSetting.xml";
        private const string _fileDataBaseInfo = @"\config\DataBaseInfo.xml";
        private string _dataDir;
        private string _modelDir;

        private DeviceInterface _device;

        private List<string> _productNameList;
        private List<string> _batchNameList;
        private List<string> _boardNameList;
        private List<string> _sideNameList;
        private List<string> _shotNameList;
        private List<string> _defectNameList;

        public string ProductName { get; set; }
        public string BatchName { get; set; }
        public string BoardName { get; set; }
        public string SideName { get; set; }
        public string ShotName { get; set; }
        public string DefectName { get; set; }

        public List<string> ProductNameList
        {
            get { return _productNameList; }
            set { _productNameList = value; }
        }

        public List<string> BatchNameList
        {
            get { return _batchNameList; }
            set { _batchNameList = value; }
        }

        public List<string> BoardNameList
        {
            get { return _boardNameList; }
            set { _boardNameList = value; }
        }

        public List<string> SideNameList
        {
            get { return _sideNameList; }
            set { _sideNameList = value; }
        }

        public List<string> ShotNameList
        {
            get { return _shotNameList; }
            set { _shotNameList = value; }
        }

        public List<string> DefectNameList
        {
            get { return _defectNameList; }
            set { _defectNameList = value; }
        }

        public MarkDataBase()
        {
            _device = new DeviceMachVision();
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
            xmlParameter.ReadParameter(Application.StartupPath + _fileDataBaseInfo);

            string productName = xmlParameter.GetParamData("ProductName");
            string batchName = xmlParameter.GetParamData("BatchName");
            string boardName = xmlParameter.GetParamData("BoardName");
            string sideName = xmlParameter.GetParamData("SideName");
            string shotName = xmlParameter.GetParamData("ShotName");
            string defectName = xmlParameter.GetParamData("DefectName");

            bool isChooseFirst = false;
            if (_device.GetProductList(out _productNameList)<=0)
            {
                return;
            }else if (!_productNameList.Contains(productName))
            {
                isChooseFirst = true;
                ProductName = _productNameList[0];
            }
            else
            {
                ProductName = productName;
            }

            if (_device.GetBatchList(ProductName, out _batchNameList) <= 0)
            {
                return;
            }
            else if ((!isChooseFirst) && _batchNameList.Contains(batchName))
            {
                BatchName = batchName;
            }
            else
            {
                isChooseFirst = true;
                BatchName = _batchNameList[0];
            }

            if (_device.GetBoardList(ProductName, BatchName, out _boardNameList) <= 0)
            {
                return;
            }
            else if ((!isChooseFirst) && _boardNameList.Contains(boardName))
            {
                BoardName = boardName;
            }
            else
            {
                isChooseFirst = true;
                BoardName = _boardNameList[0];
            }

            if (_device.GetSideList(ProductName, BatchName, BoardName, out _sideNameList) <= 0)
            {
                return;
            }
            else if ((!isChooseFirst) && _sideNameList.Contains(sideName))
            {
                SideName = sideName;
            }
            else
            {
                isChooseFirst = true;
                SideName = _sideNameList[0];
            }

            if (_device.GetShotList(ProductName, BatchName, BoardName, SideName, out _shotNameList) <= 0)
            {
                return;
            }
            else if ((!isChooseFirst) && _shotNameList.Contains(shotName))
            {
                ShotName = shotName;
            }
            else
            {
                isChooseFirst = true;
                ShotName = _shotNameList[0];
            }

            if (_device.GetDefectListInShot(ProductName, BatchName, BoardName, SideName, ShotName, out _defectNameList) <= 0)
            {
                return;
            }
            else if ((!isChooseFirst) && _defectNameList.Contains(defectName))
            {
                DefectName = defectName;
            }
            else
            {
                isChooseFirst = true;
                DefectName = _defectNameList[0];
            }
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
            xmlParameter.WriteParameter(Application.StartupPath + _fileDataBaseInfo);
        }

        public bool SaveMarkDataInfo(string productName, string batchName, string boardName, string sideName,
            string shotName, string defectName, EMarkDataType markType)
        {
            return true;
        }

        public bool GetMarkDataInfo(string productName, string batchName, string boardName, string sideName,
            string shotName, string defectName, out EMarkDataType markType)
        {
            markType = EMarkDataType.OK;
            return true;
        }
    }
}
