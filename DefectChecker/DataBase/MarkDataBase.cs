using Aqrose.Framework.Utility.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefectChecker.DataBase
{
    class MarkDataBase
    {
        private const string _fileProjectSetting = @"\config\ProjectSetting.xml";
        private const string _fileDataBaseInfo = @"\config\DataBaseInfo.xml";
        private string _dataDir;
        private string _modelDir;

        public List<string> ProductNameList { get; set; }
        public List<string> BatchNameList { get; set; }
        public List<string> BoardNameList { get; set; }
        public List<string> SideNameList { get; set; }
        public List<string> ShotNameList { get; set; }
        public List<string> DefectNameList { get; set; } 

        public MarkDataBase()
        {

        }

        public void Init()
        {
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.ReadParameter(Application.StartupPath + _fileProjectSetting);
            _dataDir = xmlParameter.GetParamData("DataDir");
            _modelDir = xmlParameter.GetParamData("ModelDir");
            

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
