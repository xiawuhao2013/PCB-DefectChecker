﻿using Aqrose.Framework.Utility.Tools;
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
        private const string _paramFileName = @"\ParamFile.xml";
        private List<string> _sideList = new List<string>();
        private List<string> _shotList = new List<string>();
        private List<string> _defectList = new List<string>();
        private string _dataDir = "";
        private string _modelDir = "";
        private string _side = "";
        private string _shot = "";
        private string _defect = "";

        private DeviceMachVision _machVision = new DeviceMachVision();

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

        //

        public DataBaseManager()
        {
            LoadConfig();
            //
            LoadProductList();
            if ("" == Product && 0 != ProductNameList.Count)
            {
                Product = ProductNameList[0];
            }
            LoadBatchList();
            if ("" == Batch && 0 != BatchNameList.Count)
            {
                Batch = BatchNameList[0];
            }
            LoadBoardList();
            if ("" == Board && 0 != BoardNameList.Count)
            {
                Board = BoardNameList[0];
            }
            LoadSideList();
            if ("" == _side && 0 != _sideList.Count)
            {
                _side = _sideList[0];
            }
            LoadShotList();
            if ("" == _shot && 0 != _shotList.Count)
            {
                _shot = _shotList[0];
            }
            LoadCellList();
            if ("" == _defect && 0 != _defectList.Count)
            {
                _defect = _defectList[0];
            }

        }

        //

        private void LoadConfig()
        {
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.ReadParameter(Application.StartupPath + _paramFileName);
            _dataDir = xmlParameter.GetParamData("DataDir");
            _modelDir = xmlParameter.GetParamData("ModelDir");
            Product = xmlParameter.GetParamData("Product");
            Batch = xmlParameter.GetParamData("Batch");
            Board = xmlParameter.GetParamData("Board");
            _side = xmlParameter.GetParamData("side");
            _shot = xmlParameter.GetParamData("shot");
            _defect = xmlParameter.GetParamData("defect");

            return;
        }

        private void SaveConfig()
        {
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.Add("Product", Product);
            xmlParameter.Add("Batch", Batch);
            xmlParameter.Add("Board", Board);
            xmlParameter.Add("side", _side);
            xmlParameter.Add("shot", _shot);
            xmlParameter.Add("defect", _defect);
            xmlParameter.WriteParameter(Application.StartupPath + _paramFileName);

            return;
        }

        private void LoadProductList()
        {
            _machVision.GetProductList(out var productNameList);
            ProductNameList = productNameList;

            return;
        }

        private void LoadBatchList()
        {
            _machVision.GetBatchList(Product, out var batchNameList);
            BatchNameList = batchNameList;

            return;
        }

        private void LoadBoardList()
        {
            _machVision.GetBoardList(Product, Batch, out var boardNameList);
            BoardNameList = boardNameList;

            return;
        }

        private void LoadSideList()
        {
            _machVision.GetSideList(Product, Batch, Board, out _sideList);

            return;
        }

        private void LoadShotList()
        {
            _machVision.GetShotList(Product, Batch, Board, _side, out _shotList);

            return;
        }

        private void LoadCellList()
        {
            _machVision.GetDefectListInShot(Product, Batch, Board, _side, _shot, out _defectList);

            return;
        }

        private DefectCell LoadDefectCell()
        {
            _machVision.GetDefectCell(Product, Batch, Board, _side, _shot, _defect, out var defectCell);

            return defectCell;
        }

        private void RecordMarkResult() { }
        private void RemoveMarkResult() { }

        //
        // LABLE: select specific image function is not clear now.
        // should i add SelectDefectGroup()?
        public void GetFirstDefectGroup(int num, out List<DefectCell> defectCells, out bool isEnd)
        {
            DefectCell defectCell = new DefectCell();
            defectCells = new List<DefectCell>();
            isEnd = true;   // i think it should be true.
            if (TrySelectCell(0, out defectCell))
            {
                defectCells.Add(defectCell);
            }
            else
            {
                defectCells.Add(new DefectCell());
            }
            for (var index = 1; index < num; ++index)
            {
                if (TryGetNextCell(out defectCell, out isEnd))
                {
                    defectCells.Add(defectCell);
                }
                else
                {
                    defectCells.Add(new DefectCell());
                }
            }

            return;
        }
        public void GetNextDefectGroup(int num, out List<DefectCell> defectCells, out bool isEnd)
        {
            DefectCell defectCell = new DefectCell();
            defectCells = new List<DefectCell>();
            isEnd = true;
            for (var index = 0; index < num; ++index)
            {
                if (TryGetNextCell(out defectCell, out isEnd))
                {
                    defectCells.Add(defectCell);
                }
                else
                {
                    defectCells.Add(new DefectCell());
                }
            }

            return;
        }
        public void GetPreviousDefectGroup(int num, out List<DefectCell> defectCells, out bool isFirst)
        {
            DefectCell defectCell = new DefectCell();
            defectCells = new List<DefectCell>();
            isFirst = true;
            for (var index = 0; index < num; ++index)
            {
                if (TryGetPreviousCell(out defectCell, out isFirst))
                {
                    defectCells.Add(defectCell);
                }
                else
                {
                    defectCells.Add(new DefectCell());
                }
            }

            return;
        }
        //
        public bool TrySelectCell(int index, out DefectCell defectCell)
        {
            defectCell = new DefectCell();
            if (null == _defectList || 0 == _defectList.Count)
            {
                return false;
            }
            if (index >= _defectList.Count || index < 0)
            {
                return false;
            }
            _defect = _defectList[index];
            defectCell = LoadDefectCell();

            return true;
        }
        public bool TryGetNextCell(out DefectCell defectCell, out bool isEnd)
        {
            defectCell = new DefectCell();
            isEnd = false;
            if (null == _defectList || 0 == _defectList.Count)
            {
                return false;
            }
            var index = _defectList.FindIndex(x => x.Contains(_defect));
            if (-1 == index)
            {
                return false;
            }
            if (++index >= _defectList.Count)
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
            if (null == _defectList || 0 == _defectList.Count)
            {
                return false;
            }
            var index = _defectList.FindIndex(x => x.Contains(_defect));
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
        //
        public bool TrySelectShot(int index)
        {
            if (null == _shotList || 0 == _shotList.Count)
            {
                return false;
            }
            if (index >= _shotList.Count || index < 0)
            {
                return false;
            }
            _shot = _shotList[index];
            LoadCellList();
            if (0 != _defectList.Count)
            {
                _defect = _defectList[0];
            }

            return true;
        }
        public bool TryGetNextShot(out bool isEnd)
        {
            isEnd = false;
            if (null == _shotList || 0 == _shotList.Count)
            {
                return false;
            }
            var index = _shotList.FindIndex(x => x.Contains(_shot));
            if (-1 == index)
            {
                return false;
            }
            if (++index >= _shotList.Count)
            {
                isEnd = true;

                return false;
            }
            if (!TrySelectShot(index))
            {
                return false;
            }

            return true;
        }
        public bool TryGetPreviousShot(out bool isFirst)
        {
            isFirst = false;
            if (null == _shotList || 0 == _shotList.Count)
            {
                return false;
            }
            var index = _shotList.FindIndex(x => x.Contains(_shot));
            if (-1 == index)
            {
                return false;
            }
            if (--index < 0)
            {
                isFirst = true;

                return false;
            }
            if (!TrySelectShot(index))
            {
                return false;
            }

            return true;
        }
        //
        public bool TrySelectSide(int index)
        {
            if (null == _sideList || 0 == _sideList.Count)
            {
                return false;
            }
            if (index >= _sideList.Count || index < 0)
            {
                return false;
            }
            _side = _sideList[index];
            LoadShotList();
            if (0 != _shotList.Count)
            {
                _shot = _shotList[0];
            }
            LoadCellList();
            if (0 != _defectList.Count)
            {
                _defect = _defectList[0];
            }

            return true;
        }
        public bool TryGetNextSide(out bool isEnd)
        {
            isEnd = false;
            if (null == _sideList || 0 == _sideList.Count)
            {
                return false;
            }
            var index = _sideList.FindIndex(x => x.Contains(_side));
            if (-1 == index)
            {
                return false;
            }
            if (++index >= _sideList.Count)
            {
                isEnd = true;

                return false;
            }
            if (!TrySelectSide(index))
            {
                return false;
            }

            return true;
        }
        public bool TryGetPreviousSide(out bool isFirst)
        {
            isFirst = false;
            if (null == _sideList || 0 == _sideList.Count)
            {
                return false;
            }
            var index = _sideList.FindIndex(x => x.Contains(_side));
            if (-1 == index)
            {
                return false;
            }
            if (--index < 0)
            {
                isFirst = true;

                return false;
            }
            if (!TrySelectSide(index))
            {
                return false;
            }

            return true;
        }
        //
        public bool TrySelectBoard(int index)
        {
            if (null == BoardNameList || 0 == BoardNameList.Count)
            {
                return false;
            }
            if (index >= BoardNameList.Count || index < 0)
            {
                return false;
            }
            Board = BoardNameList[index];
            LoadSideList();
            if (0 != _sideList.Count)
            {
                _side = _sideList[0];
            }
            LoadShotList();
            if (0 != _shotList.Count)
            {
                _shot = _shotList[0];
            }
            LoadCellList();
            if (0 != _defectList.Count)
            {
                _defect = _defectList[0];
            }

            return true;
        }
        public bool TryGetNextBoard(out bool isEnd)
        {
            isEnd = false;
            if (null == BoardNameList || 0 == BoardNameList.Count)
            {
                return false;
            }
            var index = BoardNameList.FindIndex(x => x.Contains(Board));
            if (-1 == index)
            {
                return false;
            }
            if (++index >= BoardNameList.Count)
            {
                isEnd = true;

                return false;
            }
            if (!TrySelectBoard(index))
            {
                return false;
            }

            return true;
        }
        public bool TryGetPreviousBoard(out bool isFirst)
        {
            isFirst = false;
            if (null == BoardNameList || 0 == BoardNameList.Count)
            {
                return false;
            }
            var index = BoardNameList.FindIndex(x => x.Contains(Board));
            if (-1 == index)
            {
                return false;
            }
            if (--index < 0)
            {
                isFirst = true;

                return false;
            }
            if (!TrySelectBoard(index))
            {
                return false;
            }

            return true;
        }
        //
        public bool TrySelectBatch(int index)
        {
            if (null == BatchNameList || 0 == BatchNameList.Count)
            {
                return false;
            }
            if (index >= BatchNameList.Count || index < 0)
            {
                return false;
            }
            Batch = BatchNameList[index];
            LoadBoardList();
            if (0 != BoardNameList.Count)
            {
                Board = BoardNameList[0];
            }
            LoadSideList();
            if (0 != _sideList.Count)
            {
                _side = _sideList[0];
            }
            LoadShotList();
            if (0 != _shotList.Count)
            {
                _shot = _shotList[0];
            }
            LoadCellList();
            if (0 != _defectList.Count)
            {
                _defect = _defectList[0];
            }

            return true;
        }
        public bool TryGetNextBatch(out bool isEnd)
        {
            isEnd = false;
            if (null == BatchNameList || 0 == BatchNameList.Count)
            {
                return false;
            }
            var index = BatchNameList.FindIndex(x => x.Contains(Batch));
            if (-1 == index)
            {
                return false;
            }
            if (++index >= BatchNameList.Count)
            {
                return false;
            }
            if (!TrySelectBatch(index))
            {
                return false;
            }

            return true;
        }
        public bool TryGetPreviousBatch(out bool isFirst)
        {
            isFirst = false;
            if (null == BatchNameList || 0 == BatchNameList.Count)
            {
                return false;
            }
            var index = BatchNameList.FindIndex(x => x.Contains(Batch));
            if (-1 == index)
            {
                return false;
            }
            if (--index < 0)
            {
                isFirst = true;

                return false;
            }
            if (!TrySelectBatch(index))
            {
                return false;
            }

            return true;
        }
        //
        public void GetDefectInfo() { }
        public void Mark(int type) { }
    }
}
