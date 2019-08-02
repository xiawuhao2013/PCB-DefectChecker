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
        #region TestPassed
        /*****************************************************************************************/

        private bool TryGetFirstDefectGroup()
        {
            _dataBaseManager.SetIndexOfDefectGroup(-1);

            return TryGetNextDefectGroup();
        }

        private bool TryGetLastDefectGroup()
        {
            _dataBaseManager.SetIndexOfDefectGroup(_dataBaseManager.DefectNameList.Count / NumberOfCell + 1);

            return TryGetPreviousDefectGroup();
        }

        private bool TryGetNextDefectGroup()
        {
            var isStop = false;
            do
            {
                MessageBox.Show("下一个Group！");
                isStop = _dataBaseManager.TryGetNextDefectGroup(NumberOfCell, out _curDefectGroup);
                isStop |= _dataBaseManager.HasKilledDefect;
            } while (!isStop);

            return !_dataBaseManager.HasKilledDefect;
        }

        private bool TryGetPreviousDefectGroup()
        {
            var isStop = false;
            do
            {
                MessageBox.Show("上一个Group！");
                isStop = _dataBaseManager.TryGetPreviousDefectGroup(NumberOfCell, out _curDefectGroup);
                isStop |= _dataBaseManager.HasKilledDefect;
            } while (!isStop);

            return !_dataBaseManager.HasKilledDefect;
        }
        //
        private bool TryGetFirstShot()
        {
            _dataBaseManager.SetIndexOfShot(-1);

            return TryGetNextShot();
        }

        private bool TryGetLastShot()
        {
            _dataBaseManager.SetIndexOfShot(_dataBaseManager.ShotNameList.Count);

            return TryGetPreviousShot();
        }

        private bool TryGetNextShot()
        {
            var isStop = false;
            do
            {
                MessageBox.Show("下一个Shot！");
                isStop = _dataBaseManager.TryGetNextShot();
                isStop = isStop ? TryGetFirstDefectGroup() : false;
                isStop |= _dataBaseManager.HasKilledShot;
            } while (!isStop);

            return !_dataBaseManager.HasKilledShot;
        }

        private bool TryGetPreviousShot()
        {
            var isStop = false;
            do
            {
                MessageBox.Show("上一个Shot！");
                isStop = _dataBaseManager.TryGetPreviousShot();
                isStop = isStop ? TryGetLastDefectGroup() : false;
                isStop |= _dataBaseManager.HasKilledShot;
            } while (!isStop);

            return !_dataBaseManager.HasKilledShot;
        }
        //
        private bool TryGetFirstSide()
        {
            _dataBaseManager.SetIndexOfSide(-1);

            return TryGetNextSide();
        }

        private bool TryGetLastSide()
        {
            _dataBaseManager.SetIndexOfSide(_dataBaseManager.SideNameList.Count);

            return TryGetPreviousSide();
        }

        private bool TryGetNextSide()
        {
            var isStop = false;
            do
            {
                MessageBox.Show("下一个Side！");
                isStop = _dataBaseManager.TryGetNextSide();
                isStop = isStop ? TryGetFirstShot() : false;
                isStop |= _dataBaseManager.HasKilledSide;
            } while (!isStop);

            return !_dataBaseManager.HasKilledSide;
        }

        private bool TryGetPreviousSide()
        {
            var isStop = false;
            do
            {
                MessageBox.Show("上一个Side！");
                isStop = _dataBaseManager.TryGetPreviousSide();
                isStop = isStop ? TryGetLastShot() : false;
                isStop |= _dataBaseManager.HasKilledSide;
            } while (!isStop);

            return !_dataBaseManager.HasKilledSide;
        }
        //
        private bool TryGetFirstBoard()
        {
            _dataBaseManager.SetIndexOfBoard(-1);

            return TryGetNextBoard();
        }

        private bool TryGetLastBoard()
        {
            _dataBaseManager.SetIndexOfBoard(_dataBaseManager.BoardNameList.Count);

            return TryGetPreviousBoard();
        }

        private bool TryGetNextBoard()
        {
            var isStop = false;
            do
            {
                MessageBox.Show("下一个Board！");
                isStop = _dataBaseManager.TryGetNextBoard();
                isStop = isStop ? TryGetFirstSide() : false;
                isStop |= _dataBaseManager.HasKilledBoard;
            } while (!isStop);

            return !_dataBaseManager.HasKilledBoard;
        }

        private bool TryGetPreviousBoard()
        {
            var isStop = false;
            do
            {
                MessageBox.Show("上一个Board！");
                isStop = _dataBaseManager.TryGetPreviousBoard();
                isStop = isStop ? TryGetLastSide() : false;
                isStop |= _dataBaseManager.HasKilledBoard;
            } while (!isStop);

            return !_dataBaseManager.HasKilledBoard;
        }
        //
        private bool TryGetFirstBatch()
        {
            _dataBaseManager.SetIndexOfBatch(-1);

            return TryGetNextBatch();
        }

        private bool TryGetLastBatch()
        {
            _dataBaseManager.SetIndexOfBatch(_dataBaseManager.BatchNameList.Count);

            return TryGetPreviousBatch();
        }

        private bool TryGetNextBatch()
        {
            var isStop = false;
            do
            {
                MessageBox.Show("下一个Batch！");
                isStop = _dataBaseManager.TryGetNextBatch();
                isStop = isStop ? TryGetFirstBoard() : false;
                isStop |= _dataBaseManager.HasKilledBatch;
            } while (!isStop);

            return !_dataBaseManager.HasKilledBatch;
        }

        private bool TryGetPreviousBatch()
        {
            var isStop = false;
            do
            {
                MessageBox.Show("上一个Batch！");
                isStop = _dataBaseManager.TryGetPreviousBatch();
                isStop = isStop ? TryGetLastBoard() : false;
                isStop |= _dataBaseManager.HasKilledBatch;
            } while (!isStop);

            return !_dataBaseManager.HasKilledBatch;
        }
        /*****************************************************************************************/
        #endregion

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
            
            if(TryGetFirstDefectGroup())
            {
                RefreshCellViews(_curDefectGroup);
            }
            //_dataBaseManager.SaveConfig();

            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (TryGetNextDefectGroup() || TryGetNextShot() || TryGetNextSide() || TryGetNextBoard() || TryGetNextBatch())
            {
            }
            else
            {
                MessageBox.Show("完了！");

                return;
            }
            RefreshCellViews(_curDefectGroup);
            //_dataBaseManager.SaveConfig();

            return;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (TryGetPreviousDefectGroup() || TryGetPreviousShot() || TryGetPreviousSide() || TryGetPreviousBoard() || TryGetPreviousBatch())
            {
            }
            else
            {
                MessageBox.Show("完了！");

                return;
            }
            RefreshCellViews(_curDefectGroup);
            //_dataBaseManager.SaveConfig();

            return;
        }
    }
}
