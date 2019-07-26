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
        void GetProductList(out List<string> procductList);
        void GetBatchList(string productName, out List<string> batchList);
        void GetBoardList(string productName, string batchName, out List<string> boardList);
        void GetDefetInBoard(string productName, string batchName, string boardName, out List<string> defectImgList);
        void GetCodeList(out Dictionary<int, string> codeList);
        void GetDefectCell(string batchName, string boardName, string defectImgName, out DefectCell defectCell);
        void GetTemplateWholeImgA(out Bitmap wholeImg);
        void GetTemplateWholeImgB(out Bitmap wholeImg);
        void GetGerberWholeImgA(out Bitmap gerberWholeImg);
        void GetGerberWholeImgB(out Bitmap gerberWholeImg);
    }
}
