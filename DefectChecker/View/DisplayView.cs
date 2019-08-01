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

        private bool TryGetDefectGroupOfLastExit()
        {
            return _dataBaseManager.TryGetDefectGroupOfLastExit(NumberOfCell, out _curDefectGroup);
        }

        private bool TryGetFirstDefectGroup()
        {
            return _dataBaseManager.TryGetFirstDefectGroup(NumberOfCell, out _curDefectGroup);
        }

        private bool TryGetLastDefectGroup()
        {

            return _dataBaseManager.TryGetLastDefectGroup(NumberOfCell, out _curDefectGroup);
        }

        private bool TryGetNextDefectGroup()
        {
            return _dataBaseManager.TryGetNextDefectGroup(NumberOfCell, out _curDefectGroup);
        }

        private bool TryGetPreviousDefectGroup()
        {
            return _dataBaseManager.TryGetPreviousDefectGroup(NumberOfCell, out _curDefectGroup);
        }
        //
        private bool TryGetShotOfLastExit()
        {
            return _dataBaseManager.TryGetShotOfLastExit();
        }

        private bool TryGetFirstShot()
        {
            if (!_dataBaseManager.TryGetFirstShot())
            {
                return false;
            }

            return TryGetFirstDefectGroup();
        }

        private bool TryGetLastShot()
        {
            if (!_dataBaseManager.TryGetLastShot())
            {
                return false;
            }

            return TryGetLastDefectGroup();
        }

        private bool TryGetNextShot()
        {
            if (!_dataBaseManager.TryGetNextShot())
            {
                return false;
            }

            return TryGetFirstDefectGroup();
        }

        private bool TryGetPreviousShot()
        {
            if (!_dataBaseManager.TryGetPreviousShot())
            {
                return false;
            }

            return TryGetLastDefectGroup();
        }
        //
        private bool TryGetSideOfLastExit()
        {
            return _dataBaseManager.TryGetSideOfLastExit();
        }

        private bool TryGetFirstSide()
        {
            if (!_dataBaseManager.TryGetFirstSide())
            {
                return false;
            }

            return TryGetFirstShot();
        }

        private bool TryGetLastSide()
        {
            if(!_dataBaseManager.TryGetLastSide())
            {
                return false;
            }

            return TryGetLastShot();
        }

        private bool TryGetNextSide()
        {
            if (!_dataBaseManager.TryGetNextSide())
            {
                return false;
            }

            return TryGetFirstShot();
        }

        private bool TryGetPreviousSide()
        {
            if (!_dataBaseManager.TryGetPreviousSide())
            {
                return false;
            }

            return TryGetLastShot();
        }

        /*****************************************************************************************/
        #endregion

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
            
            if(TryGetFirstDefectGroup())
            {
                RefreshCellViews(_curDefectGroup);
            }
            _dataBaseManager.SaveConfig();

            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            if (TryGetNextDefectGroup() || TryGetNextShot() || TryGetNextSide())
            {
                RefreshCellViews(_curDefectGroup);
            }
            */
            if (TryGetNextDefectGroup())
            {
            }
            else
            {
                MessageBox.Show("加载下一相机！");
                if (TryGetNextShot())
                {
                }
                else
                {
                    MessageBox.Show("加载下一面！");
                    if (TryGetNextSide())
                    {
                    }
                    else
                    {
                        MessageBox.Show("加载下一块板！");
                        //...
                    }
                }
            }
            
            RefreshCellViews(_curDefectGroup);
            _dataBaseManager.SaveConfig();

            return;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (TryGetPreviousDefectGroup())
            {
            }
            else
            {
                MessageBox.Show("加载上一相机！");
                if (TryGetPreviousShot())
                {
                }
                else
                {
                    MessageBox.Show("加载上一面！");
                    if (TryGetPreviousSide())
                    {
                    }
                    else
                    {
                        MessageBox.Show("加载上一块板！");
                        //...
                    }
                }
            }
            RefreshCellViews(_curDefectGroup);
            _dataBaseManager.SaveConfig();

            return;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (TryGetDefectGroupOfLastExit())
            {
                RefreshCellViews(_curDefectGroup);
            }

            return;
        }
    }
}
