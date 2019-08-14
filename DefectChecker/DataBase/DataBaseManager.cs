using Aqrose.Framework.Utility.Tools;
using DefectChecker.DefectDataStructure;
using DefectChecker.DeviceModule.MachVision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DefectChecker.DataBase
{
    public class DataBaseManager
    {
        public MarkDataBase DataBase { get; set; }

        public DataBaseManager()
        {
            DataBase = new MarkDataBase();
        }

        private const string _fileOfDataBaseManager = @"\config\DataBaseManager.xml";

        // image-1
        private List<DefectCell> _defectCellList = new List<DefectCell>();
        private Bitmap _wholeImageA = null;
        private Bitmap _wholeImageB = null;
        private Bitmap _gerberWholeImageA = null;
        private Bitmap _gerberWholeImageB = null;
        // skip-1
        private bool _hasKilledDefect = false;
        private bool _hasKilledShot = false;
        private bool _hasKilledSide = false;
        private bool _hasKilledBoard = false;
        private bool _hasKilledBatch = false;
        private bool _hasKilledProduct = false;

        // image-2
        public List<DefectCell> DefectCellList { get { return _defectCellList; } }
        public Bitmap WholeImageA { get { return _wholeImageA; } }
        public Bitmap WholeImageB { get { return _wholeImageB; } }
        public Bitmap GerberWholeImageA { get { return _gerberWholeImageA; } }
        public Bitmap GerberWholeImageB { get { return _gerberWholeImageB; } }
        // skip-3
        public bool HasKilledDefect { get { return _hasKilledDefect; } }
        public bool HasKilledShot { get { return _hasKilledShot; } }
        public bool HasKilledSide { get { return _hasKilledSide; } }
        public bool HasKilledBoard { get { return _hasKilledBoard; } }
        public bool HasKilledBatch { get { return _hasKilledBatch; } }
        public bool HasKilledProduct { get { return _hasKilledProduct; } }

        private DefectCell LoadDefectCell()
        {
            DataBase._device.GetDefectCell(DataBase.ProductName, DataBase.BatchName, DataBase.BoardName, DataBase.SideName, DataBase.ShotName, DataBase.DefectName, out var defectCell);

            return defectCell;
        }

        private void RecordMarkResult() { }
        private void RemoveMarkResult() { }

        //
        public bool TrySelectCell(int index, out DefectCell defectCell)
        {
            defectCell = new DefectCell();
            if (null == DataBase.DefectNameList || 0 == DataBase.DefectNameList.Count || index >= DataBase.DefectNameList.Count || index < 0)
            {
                DataBase.DefectName = "";

                return false;
            }
            DataBase.DefectName = DataBase.DefectNameList[index];
            defectCell = LoadDefectCell();

            return true;
        }
        // LABEL: refrence these codes temporary.
        /*
        public bool TryGetNextCell(out DefectCell defectCell, out bool isEnd)
        {
            defectCell = new DefectCell();
            isEnd = false;
            if (null == _defectNameList || 0 == _defectNameList.Count)
            {
                return false;
            }
            var index = _defectNameList.FindIndex(x => x.Contains(_defect));
            if (-1 == index)
            {
                return false;
            }
            if (++index >= _defectNameList.Count)
            {
                isEnd = true;

                return false;
            }
            if (!TrySelectCell(index, out defectCell))
            {
                return false;
            }

            return true;
        }
        public bool TryGetPreviousCell(out DefectCell defectCell, out bool isFirst)
        {
            defectCell = new DefectCell();
            isFirst = false;
            if (null == _defectNameList || 0 == _defectNameList.Count)
            {
                return false;
            }
            var index = _defectNameList.FindIndex(x => x.Contains(_defect));
            if (-1 == index)
            {
                return false;
            }
            if (--index < 0)
            {
                isFirst = true;

                return false;
            }
            if (!TrySelectCell(index, out defectCell))
            {
                return false;
            }

            return true;
        }
        */
        //
        public bool TrySelectDefectGroup(int index, int num, out List<DefectCell> defectCells)
        {
            DefectCell defectCell = new DefectCell();
            defectCells = new List<DefectCell>();
            var nullCount = 0;
            for (var iter = 0; iter < num; ++iter)
            {
                if (TrySelectCell(IndexOfDefectGroup * num + iter, out defectCell))
                {
                    defectCells.Add(defectCell);
                }
                else
                {
                    ++nullCount;
                    defectCells.Add(new DefectCell());
                }
            }
            _hasKilledDefect = nullCount == num;

            return nullCount != num;
        }
        public void SetIndexOfDefectGroup(int index)
        {
            IndexOfDefectGroup = index;

            return;
        }
        public bool TryGetNextDefectGroup(int num, out List<DefectCell> defectCells)
        {
            if (!TrySelectDefectGroup(++IndexOfDefectGroup, num, out defectCells))
            {
                --IndexOfDefectGroup;

                return false;
            }

            return true;
        }
        public bool TryGetPreviousDefectGroup(int num, out List<DefectCell> defectCells)
        {
            if (!TrySelectDefectGroup(--IndexOfDefectGroup, num, out defectCells))
            {
                ++IndexOfDefectGroup;

                return false;
            }

            return true;
        }
        //
        //public bool TrySelectShot(int index)
        //{
        //    _shot = "";
        //    _shotNameList = new List<string>();
        //    _hasKilledShot = false;

        //    IndexOfShot = index;
        //    if (null == ShotNameList || 0 == ShotNameList.Count || index >= ShotNameList.Count || index < 0)
        //    {
        //        _hasKilledShot = true;

        //        return false;
        //    }
        //    _shot = ShotNameList[index];
        //    LoadCellList();

        //    return true;
        //}
        public bool TrySelectShot(int index)
        {
            if (null == DataBase.ShotNameList || index >= DataBase.ShotNameList.Count)
            {
                return false;
            }
            return DataBase.UpdateShotName(DataBase.ShotNameList[index]);
        }
        public bool TryGetNextShot()
        {
            var index= DataBase.ShotNameList.IndexOf(DataBase.ShotName);
            if (!TrySelectShot(++index))
            {
                return false;
            }

            return true;
        }
        public bool TryGetPreviousShot()
        {
            var index= DataBase.ShotNameList.IndexOf(DataBase.ShotName);
            if (!TrySelectShot(--index))
            {
                return false;
            }

            return true;
        }
        //
        //public bool TrySelectSide(int index)
        //{
        //    _side = "";
        //    _sideNameList = new List<string>();
        //    _hasKilledSide = false;

        //    IndexOfSide = index;
        //    if (null == SideNameList || 0 == SideNameList.Count || index >= SideNameList.Count || index < 0)
        //    {
        //        _hasKilledSide = true;

        //        return false;
        //    }
        //    _side = SideNameList[index];
        //    LoadShotList();

        //    return true;
        //}
        public bool TrySelectSide(int index)
        {
            if (null == DataBase.SideNameList)
            {
                return false;
            }
            return DataBase.UpdateSideName(DataBase.SideNameList[index]);
        }
        public bool TryGetNextSide()
        {
            if (!TrySelectSide(++IndexOfSide))
            {
                --IndexOfSide;

                return false;
            }

            return true;
        }
        public bool TryGetPreviousSide()
        {
            if (!TrySelectSide(--IndexOfSide))
            {
                ++IndexOfSide;

                return false;
            }

            return true;
        }
        //
        //public bool TrySelectBoard(int index)
        //{
        //    _board = "";
        //    _boardNameList = new List<string>();
        //    _hasKilledBoard = false;

        //    IndexOfBoard = index;
        //    if (null == BoardNameList || 0 == BoardNameList.Count || index >= BoardNameList.Count || index < 0)
        //    {
        //        _hasKilledBoard = true;

        //        return false;
        //    }
        //    _board = BoardNameList[index];
        //    LoadSideList();

        //    return true;
        //}
        public bool TrySelectBoard(int index)
        {
            if (null == DataBase.BoardNameList)
            {
                return false;
            }
            return DataBase.UpdateBoardName(DataBase.BoardNameList[index]);
        }
        public bool TryGetNextBoard()
        {
            if (!TrySelectBoard(++IndexOfBoard))
            {
                --IndexOfBoard;

                return false;
            }

            return true;
        }
        public bool TryGetPreviousBoard()
        {
            if (!TrySelectBoard(--IndexOfBoard))
            {
                ++IndexOfBoard;

                return false;
            }

            return true;
        }
        //
        //public bool TrySelectBatch(int index)
        //{
        //    _batch = "";
        //    _boardNameList = new List<string>();
        //    _hasKilledBatch = false;

        //    IndexOfBatch = index;
        //    if (null == BatchNameList || 0 == BatchNameList.Count || index >= BatchNameList.Count || index < 0)
        //    {
        //        _hasKilledBatch = true;

        //        return false;
        //    }
        //    _batch = BatchNameList[index];
        //    LoadBoardList();

        //    return true;
        //}
        public bool TrySelectBatch(int index)
        {
            if (null == DataBase.BatchNameList)
            {
                return false;
            }
            return DataBase.UpdateBatchName(DataBase.BatchNameList[index]);
        }
        public bool TryGetNextBatch()
        {
            if (!TrySelectBatch(++IndexOfBatch))
            {
                --IndexOfBatch;

                return false;
            }

            return true;
        }
        public bool TryGetPreviousBatch()
        {
            if (!TrySelectBatch(--IndexOfBatch))
            {
                ++IndexOfBatch;

                return false;
            }

            return true;
        }
        //
        public void GetDefectInfo() { }

        public void Mark(int type)
        {

        }
        //

    }
}
