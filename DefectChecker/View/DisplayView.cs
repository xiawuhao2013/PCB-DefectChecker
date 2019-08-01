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
        private List<DefectCell> _curDefectGroup = new List<DefectCell>();
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
            InitializeViewCells();
        }

        //

        private void ToolStripMenuItemOfchangeDisplayNum_Click(object sender, EventArgs e)
        {
            _displayViewOfCells.UpdateNumberOfCellDisplay();
            InitializeViewCells();

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

        private void InitializeViewCells()
        {
            this.splitContainer1.Panel2.Controls.Clear();
            _displayViewOfCells.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Add(_displayViewOfCells);

            return;
        }

        private void GetDefectGroupOfLastExit()
        {
            _dataBaseManager.GetDefectGroupOfLastExit(NumberOfCell, out _curDefectGroup);
            RefreshCellViews(_curDefectGroup);

            return;
        }

        private void GetFirstDefectGroup()
        {
            _dataBaseManager.GetFirstDefectGroup(NumberOfCell, out _curDefectGroup, out var isEmpty);
            if (!isEmpty)
            {
                RefreshCellViews(_curDefectGroup);

                return;
            }
            MessageBox.Show("这组是空的，继续向下！");
            if (!TryGetNextShot())
            {
                return;
            }

            return;
        }

        private void GetLastDefectGroup()
        {
            _dataBaseManager.GetLastDefectGroup(NumberOfCell, out _curDefectGroup, out var isEmpty);
            if (!isEmpty)
            {
                RefreshCellViews(_curDefectGroup);

                return;
            }
            MessageBox.Show("这组是空的，继续向上！");
            if (!TryGetPreviousShot())
            {
                return;
            }

            return;
        }

        private void GetNextDefectGroup()
        {
            _dataBaseManager.GetNextDefectGroup(NumberOfCell, out _curDefectGroup, out var isEmpty);
            if (!isEmpty)
            {
                RefreshCellViews(_curDefectGroup);

                return;
            }
            MessageBox.Show("已经是最后一组了！");
            if (!TryGetNextShot())
            {
                return;
            }
            GetFirstDefectGroup();

            return;
        }

        private void GetPreviousDefectGroup()
        {
            _dataBaseManager.GetPreviousDefectGroup(NumberOfCell, out _curDefectGroup, out var isEmpty);
            if (!isEmpty)
            {
                RefreshCellViews(_curDefectGroup);

                return;
            }
            MessageBox.Show("已经是第一组了！");
            if (!TryGetPreviousShot())
            {
                return;
            }
            GetFirstDefectGroup();

            return;
        }

        private bool TryGetFirstShot()
        {
            return _dataBaseManager.TrySelectShot(0);
        }

        private bool TryGetNextShot()
        {
            return _dataBaseManager.TryGetNextShot(out var isEnd);
        }

        private bool TryGetPreviousShot()
        {
            return _dataBaseManager.TryGetPreviousShot(out var isFirst);
        }

        private bool TryGetFirstBoard()
        {
            return _dataBaseManager.TrySelectBoard(0);
        }

        private bool TryGetNextBoard()
        {
            return _dataBaseManager.TryGetNextBoard(out var isEnd);
        }

        private bool TryGetPreviousBoard()
        {
            return _dataBaseManager.TryGetPreviousBoard(out var isFirst);
        }

        private bool TrySelectBoard(int index)
        {
            return _dataBaseManager.TrySelectBoard(index);
        }

        private bool TryGetFirstBatch()
        {
            return _dataBaseManager.TrySelectBatch(0);
        }

        private bool TryGetNextBatch()
        {
            return _dataBaseManager.TryGetNextBatch(out var isEnd);
        }

        private bool TryGetPreviousBatch()
        {
            return _dataBaseManager.TryGetPreviousBatch(out var isFirst);
        }

        private bool TrySelectBatch(int index)
        {
            return _dataBaseManager.TrySelectBatch(index);
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
                aqDisplay.InteractiveGraphics.Clear();
                if (null != cell.DefectImage)
                {
                    aqDisplay.Visible = true;
                    aqDisplay.Image = cell.DefectImage.Clone() as Bitmap;
                    aqDisplay.FitToScreen();
                    aqDisplay.Update();
                }
                else
                {
                    aqDisplay.Visible = false;
                }
                ++indexOfCellView;
                if (indexOfCellView > NumberOfCell)
                {
                    MessageBox.Show(@"异常的显示数量！");

                    return;
                }
            }

            return;
        }

        /*************************************************************************************/

        private void button1_Click(object sender, EventArgs e)
        {
            GetFirstDefectGroup();
            _dataBaseManager.SaveConfig();

            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetNextDefectGroup();
            _dataBaseManager.SaveConfig();

            return;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetPreviousDefectGroup();
            _dataBaseManager.SaveConfig();

            return;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GetDefectGroupOfLastExit();

            return;
        }
    }
}
