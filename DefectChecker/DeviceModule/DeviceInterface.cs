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
        int GetCodeList(out Dictionary<int, string> codeList);
        int GetProductList(out List<string> procductList);
        int GetBatchList(string productName, out List<string> batchList);
        int GetBoardList(string productName, string batchName, out List<string> boardList);
        int GetSideList(string productName, string batchName, string boardName, out List<string> sideList);
        int GetShotList(string productName, string batchName, string boardName, string sideName, out List<string> shotList);
        int GetDefectListInShot(string productName, string batchName, string boardName, string sideName, string shotName, out List<string> defectList);
        void GetDefectCell(string productName, string batchName, string boardName, string sideName, string shotName, string defectName, out DefectCell defectCell);
        bool GetTemplateWholeImgA(out Bitmap wholeImg);
        bool GetTemplateWholeImgB(out Bitmap wholeImg);
        bool GetGerberWholeImgA(out Bitmap gerberWholeImg);
        bool GetGerberWholeImgB(out Bitmap gerberWholeImg);

        // LABEL: lack of GetDefectPositionListOfxxx() GetDefectPositionOfDefect()
    }
}
