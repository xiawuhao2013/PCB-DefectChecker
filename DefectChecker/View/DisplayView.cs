using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DefectChecker.DataBase;
using DefectChecker.DefectDataStructure;

namespace DefectChecker.View
{
    public partial class DisplayView : UserControl
    {
        private DataBaseManager _dataBaseManager = new DataBaseManager();
        private DispalyViewOfCells _displayViewOfCells = new DispalyViewOfCells();
        // LABEL: temporary variables.
        private int _typeOfMark = 0;
        private int _indexOfSelectedBatch = 0;
        private int _indexOfSelectedBoard = 0;
        private int _indexOfSelectedCell = 0;
        private int _indexOfCursor = 0;

        public int NumberOfCell { get { return _displayViewOfCells.NumberOfCell; } }

        //

        public DisplayView()
        {
            InitializeComponent();
            this.panelOfInfo.Visible = false;
            RefreshDiplayViewCells();
        }

        //

        private void ToolStripMenuItemOfchangeDisplayNum_Click(object sender, EventArgs e)
        {
            _displayViewOfCells.UpdateNumberOfCellDisplay();
            RefreshDiplayViewCells();

            return;
        }

        private void ToolStripMenuItemOfAAA_Click(object sender, EventArgs e)
        {
            const string textOfShow = @"显示侧边栏";
            const string textOfHide = @"隐藏侧边栏";
            if (textOfHide == this.ToolStripMenuItemOfAAA.Text)
            {
                this.panelOfInfo.Visible = false;
                this.ToolStripMenuItemOfAAA.Text = textOfShow;

                return;
            }
            if (textOfShow == this.ToolStripMenuItemOfAAA.Text)
            {
                this.panelOfInfo.Visible = true;
                this.ToolStripMenuItemOfAAA.Text = textOfHide;

                return;
            }

            return;
        }

        private void RefreshDiplayViewCells()
        {
            this.splitContainer1.Panel2.Controls.Clear();
            _displayViewOfCells.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Add(_displayViewOfCells);

            return;
        }

        private List<DefectCell> GetCurDefectGroup()
        {
            return _dataBaseManager.GetCurDefectGroup(NumberOfCell);
        }

        private void NextDefectGroup()
        {
            _dataBaseManager.NextDefectGroup(NumberOfCell);

            return;
        }

        private void NextCell()
        {
            _dataBaseManager.NextCell();

            return;
        }

        private void PreviousCell()
        {
            _dataBaseManager.PreviousCell();

            return;
        }

        private void SelectCell()
        {
            _dataBaseManager.SelectCell(_indexOfSelectedCell);

            return;
        }

        private void NextBoard()
        {
            _dataBaseManager.NextBoard();

            return;
        }

        private void PreviousBoard()
        {
            _dataBaseManager.PreviousBoard();

            return;
        }

        private void SelectBoard()
        {
            _dataBaseManager.SelectBoard(_indexOfSelectedBoard);

            return;
        }

        private void NextBatch()
        {
            _dataBaseManager.NextBatch();

            return;
        }

        private void PreviousBatch()
        {
            _dataBaseManager.PreviousBatch();

            return;
        }

        private void SelectBatch()
        {
            _dataBaseManager.SelectBatch(_indexOfSelectedBatch);

            return;
        }

        private void GetDefectInfo()
        {
            _dataBaseManager.GetDefectInfo();

            return;
        }

        private void Mark()
        {
            _dataBaseManager.Mark(_typeOfMark);

            return;
        }

        private void RefreshCellViews(List<DefectCell> defectCellList)
        {
            int indexOfCellView = 0;
            foreach (var cell in defectCellList)
            {
                _displayViewOfCells.CellViewList.TryGetValue(indexOfCellView, out var aqDisplay);
                if (null == cell.DefectImage)
                {
                    aqDisplay.Image = cell.DefectImage;
                }
                aqDisplay.Refresh();
                ++indexOfCellView;
                if (indexOfCellView > NumberOfCell)
                {
                    MessageBox.Show(@"异常的显示数量！");

                    return;
                }
            }

            return;
        }
    }
}
