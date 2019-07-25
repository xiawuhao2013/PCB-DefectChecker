using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefectChecker.DefectDataStructure;

namespace DefectChecker.DeviceModule
{
    public interface DeviceInterface
    {
        void GetBatchList(out List<string> batchList);
        void GetBoardList(string batchName, out List<string> boardList);
        void GetDefetInBoard(string batchName, string boardName, out List<string> defectImgList);
        void GetCodeList(out Dictionary<int, string> codeList);
        void GetDefectCell(string batchName, string boardName, string defectImgName, out DefectCell defectCell);
        void GetTemplateWholeImg(out Bitmap templateWholeImg);
        void GetGerberWholeImg(out Bitmap gerberWholeImg);
    }
}
