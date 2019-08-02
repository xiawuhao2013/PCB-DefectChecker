using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectChecker.DataBase
{
    public enum EMarkDataType
    {
        OK = 0,
        NG,
        Undefined
    };

    public class MarkDataInfo
    {
        public string ProductName { get; set; }
        public string BatchName { get; set; }
        public string BoardName { get; set; }
        public string SideName { get; set; }
        public string ShotName { get; set; }
        public string DefectName { get; set; }
        public EMarkDataType MarkType { get; set; }

        public MarkDataInfo(string productName, string batchName, string boardName, string sideName,
            string shotName, string defectName, EMarkDataType markType)
        {
            ProductName = productName;
            BatchName = batchName;
            BoardName = boardName;
            SideName = sideName;
            ShotName = shotName;
            DefectName = defectName;
            MarkType = markType;
        }
    }
}
