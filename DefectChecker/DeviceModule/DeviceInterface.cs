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
        void SetDataDir(string modelDir, string dataDir);
        void GetDefectCell(string productName, string batchName, string boardName, string sideName, string shotName, string defectName, out DefectCell defectCell);
        int GetDefectListInShot(string productName, string batchName, string boardName, string sideName, string shotName, out List<string> defectList);
        int GetShotList(string productName, string batchName, string boardName, string sideName, out List<string> shotList);
        int GetSideList(string productName, string batchName, string boardName, out List<string> sideList);
        int GetBoardList(string productName, string batchName, out List<string> boardList);
        int GetBatchList(string productName, out List<string> batchList);
        int GetCodeList(out Dictionary<int, string> codeList);
        int GetProductList(out List<string> procductList);
        void GetTemplateWholeImgA(out Bitmap wholeImg);
        void GetTemplateWholeImgB(out Bitmap wholeImg);
        void GetGerberWholeImgA(out Bitmap gerberWholeImg);
        void GetGerberWholeImgB(out Bitmap gerberWholeImg);

        // LABEL: lack of GetDefectPositionListOfxxx() GetDefectPositionOfDefect()
    }
}
