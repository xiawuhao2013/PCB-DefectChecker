using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefectChecker.DefectDataStructure;

namespace DefectChecker.DeviceModule.MachVision
{
    class DeviceMachVision : DeviceInterface
    {
        public void GetBatchList(out List<string> batchList)
        {
            throw new NotImplementedException();
        }

        public void GetBoardList(string batchName, out List<string> boardList)
        {
            throw new NotImplementedException();
        }

        public void GetCodeList(out Dictionary<int, string> codeList)
        {
            throw new NotImplementedException();
        }

        public void GetDefectCell(string batchName, string boardName, string defectImgName, out DefectCell defectCell)
        {
            throw new NotImplementedException();
        }

        public void GetDefetInBoard(string batchName, string boardName, out List<string> defectImgList)
        {
            throw new NotImplementedException();
        }

        public void GetGerberWholeImg(out Bitmap gerberWholeImg)
        {
            throw new NotImplementedException();
        }

        public void GetTemplateWholeImg(out Bitmap templateWholeImg)
        {
            throw new NotImplementedException();
        }
    }
}
