using DefectChecker.DefectDataStructure;
using System.Collections.Generic;
using System.Drawing;

namespace DefectChecker.DataBase
{
    public class DataBaseManager
    {
        public List<string> ProductNameList { get; set; }
        public List<string> BatchNameList { get; set; }
        public List<string> BoardNameList { get; set; }

        public List<string> ImageNameList { get; set; }
        public string Product { get; set; }
        public string Batch { get; set; }
        public string Board { get; set; }
        public List<DefectCell> DefectCellList {get;set;}
        public Bitmap WholeImageA { get; set; }
        public Bitmap WholeImageB { get; set; }
        public Bitmap GerberWholeImageA { get; set; }
        public Bitmap GerberWholeImageB { get; set; }

        
        public DataBaseManager() { }

        private void LoadCellList() { }
        private void RecordMarkResult() { }
        private void RemoveMarkResult() { }

        public List<DefectCell> GetCurDefectGroup(int num) { return null; }
        public void NextDefectGroup(int num) { }
        public void NextCell() { }
        public void PreviousCell() { }
        public void SelectCell(int index) { }
        public void NextBoard() { }
        public void PreviousBoard() { }
        public void SelectBoard(int index) { }
        public void NextBatch() { }
        public void PreviousBatch() { }
        public void SelectBatch(int index) { }

        public void GetDefectInfo() { }
        public void Mark(int type) { }
    }
}
