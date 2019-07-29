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
        void GetDefectCell(string productName, string batchName, string boardName, string sideName, string shotName, string defectName, out DefectCell defectCell);
        void GetDefectListInShot(string productName, string batchName, string boardName, string sideName, string shotName, out List<string> defectList);
        void GetShotList(string productName, string batchName, string boardName, string sideName, out List<string> shotList);
        void GetSideList(string productName, string batchName, string boardName, out List<string> sideList);
        void GetBoardList(string productName, string batchName, out List<string> boardList);
        void GetBatchList(string productName, out List<string> batchList);
        void GetCodeList(out Dictionary<int, string> codeList);
        void GetProductList(out List<string> procductList);
        void GetTemplateWholeImgA(out Bitmap wholeImg);
        void GetTemplateWholeImgB(out Bitmap wholeImg);
        void GetGerberWholeImgA(out Bitmap gerberWholeImg);
        void GetGerberWholeImgB(out Bitmap gerberWholeImg);

        // LABEL: lack of GetDefectPositionListOfxxx() GetDefectPositionOfDefect()
    }
}
