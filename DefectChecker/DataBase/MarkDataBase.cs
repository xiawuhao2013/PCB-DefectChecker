using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectChecker.DataBase
{
    class MarkDataBase
    {
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
