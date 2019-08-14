using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DefectChecker.DataBase;
using DefectChecker.DefectDataStructure;
using DefectChecker.View.widget;

namespace DefectChecker.View
{
    public partial class DisplayView : UserControl
    {
        private bool _canRefreshDisplayWindows = true;
        // 
        private List<DefectCell> _defectCells = null; // replace with DataBaseManager.xxx
        private List<DisplayWindow> _displayWindows = null; // may need window number control.

        private DataBaseManager _dataBaseManager;
        private MarkDataBase _dataBase;

        public DisplayView()
        {
            InitializeComponent();
            DataBaseInit();

            // 
            _displayWindows.Add(displayWindow1);
        }

        private void EnableCanRefreshDisplayWindows()
        {
            _canRefreshDisplayWindows = true;

            return;
        }

        private void DataBaseInit()
        {
            _dataBaseManager = new DataBaseManager();
            _dataBase = _dataBaseManager.DataBase;

            ComboBoxRefresh();
        }

        private void ComboBoxRefresh()
        {
            this.comboBoxProduct.TextChanged -= new System.EventHandler(this.comboBoxProduct_TextChanged);
            this.comboBoxBatch.TextChanged -= new System.EventHandler(this.comboBoxBatch_TextChanged);
            this.comboBoxBoard.TextChanged -= new System.EventHandler(this.comboBoxBoard_TextChanged);
            this.comboBoxSide.TextChanged -= new System.EventHandler(this.comboBoxSide_TextChanged);
            this.comboBoxShot.TextChanged -= new System.EventHandler(this.comboBoxShot_TextChanged);
            this.comboBoxDefect.TextChanged -= new System.EventHandler(this.comboBoxDefect_TextChanged);

            comboBoxProduct.DataSource = _dataBase.ProductNameList;
            comboBoxProduct.Text = _dataBase.ProductName;

            comboBoxBatch.DataSource = _dataBase.BatchNameList;
            comboBoxBatch.Text = _dataBase.BatchName;

            comboBoxBoard.DataSource = _dataBase.BoardNameList;
            comboBoxBoard.Text = _dataBase.BoardName;

            comboBoxSide.DataSource = _dataBase.SideNameList;
            comboBoxSide.Text = _dataBase.SideName;

            comboBoxShot.DataSource = _dataBase.ShotNameList;
            comboBoxShot.Text = _dataBase.ShotName;

            comboBoxDefect.DataSource = _dataBase.DefectNameList;
            comboBoxDefect.Text = _dataBase.DefectName;
            
            this.comboBoxProduct.TextChanged += new System.EventHandler(this.comboBoxProduct_TextChanged);
            this.comboBoxBatch.TextChanged += new System.EventHandler(this.comboBoxBatch_TextChanged);
            this.comboBoxBoard.TextChanged += new System.EventHandler(this.comboBoxBoard_TextChanged);
            this.comboBoxSide.TextChanged += new System.EventHandler(this.comboBoxSide_TextChanged);
            this.comboBoxShot.TextChanged += new System.EventHandler(this.comboBoxShot_TextChanged);
            this.comboBoxDefect.TextChanged += new System.EventHandler(this.comboBoxDefect_TextChanged);
        }

        private void comboBoxProduct_TextChanged(object sender, EventArgs e)
        {
            string productName = comboBoxProduct.Text;
            _dataBase.UpdateProductName(productName);
            ComboBoxRefresh();
        }

        private void comboBoxBatch_TextChanged(object sender, EventArgs e)
        {
            string batchName = comboBoxBatch.Text;
            _dataBase.UpdateBatchName(batchName);
            ComboBoxRefresh();
        }

        private void comboBoxBoard_TextChanged(object sender, EventArgs e)
        {
            string boardName = comboBoxBoard.Text;
            _dataBase.UpdateBoardName(boardName);
            ComboBoxRefresh();
        }

        private void comboBoxSide_TextChanged(object sender, EventArgs e)
        {
            string sideName = comboBoxSide.Text;
            _dataBase.UpdateSideName(sideName);
            ComboBoxRefresh();
        }

        private void comboBoxShot_TextChanged(object sender, EventArgs e)
        {
            string shotName = comboBoxShot.Text;
            _dataBase.UpdateShotName(shotName);
            ComboBoxRefresh();
        }

        private void comboBoxDefect_TextChanged(object sender, EventArgs e)
        {
            string defectName = comboBoxDefect.Text;
            _dataBase.UpdateDefectName(defectName);
            ComboBoxRefresh();
        }

        private void ShowModelOnDisplayWindows(List<DisplayWindow> displayWindows)
        {
            if (null == displayWindows)
            {
                return;
            }
            foreach (var displayWindow in displayWindows)
            {
                displayWindow.ShowModelWindow();
            }

            return;
        }

        private void HideModelOnDisplayWindows(List<DisplayWindow> displayWindows)
        {
            if (null == displayWindows)
            {
                return;
            }
            foreach (var displayWindow in displayWindows)
            {
                displayWindow.HideModelWindow();
            }

            return;
        }

        private void RefreshDisplayWindows(List<DefectCell> defectCells, List<DisplayWindow> displayWindows)
        {
            if (null == defectCells || null == displayWindows)
            {
                return;
            }
            if (defectCells.Count != displayWindows.Count)
            {
                MessageBox.Show("数量异常!");

                return;
            }
            for (int indexOfDisplayWindow = 0; indexOfDisplayWindow < displayWindows.Count; ++indexOfDisplayWindow)
            {
                displayWindows[indexOfDisplayWindow].defectCell = defectCells[indexOfDisplayWindow];
                displayWindows[indexOfDisplayWindow].RefreshWindow();
            }

            return;
        }

        private void MoveCursorToDisplayWindow(int indexOfWindow, List<DisplayWindow> displayWindows)
        {
            if (displayWindows == null)
            {
                return;
            }
            if (indexOfWindow < displayWindows.Count)
            {
                displayWindows[indexOfWindow].Focus();
            }

            return;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (_canRefreshDisplayWindows)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
            int indexOfDisplayWindowOnSelected = 0;
            switch (keyData)
            {
                // TODO: add manager codes.
                // judge the moment to refresh all displayWindows, or move the cursor within displayWindows.
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    EnableCanRefreshDisplayWindows();
                    break;
                default:
                    break;
            }
            MoveCursorToDisplayWindow(indexOfDisplayWindowOnSelected, _displayWindows);

            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_canRefreshDisplayWindows)
            {
                RefreshDisplayWindows(_defectCells, _displayWindows);
                _canRefreshDisplayWindows = false;
            }

            return;
        }

        private void DisplayView_Enter(object sender, EventArgs e)
        {
            this.timer1.Start();
            EnableCanRefreshDisplayWindows();

            return;
        }

        private void DisplayView_Leave(object sender, EventArgs e)
        {
            this.timer1.Stop();
            EnableCanRefreshDisplayWindows();

            return;
        }
    }
}
