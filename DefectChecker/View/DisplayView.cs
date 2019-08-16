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
        private int _numberOfDispalyWindows = 1;
        private bool _hasButtonPressed = false;
        private int _indexOfDisplayWindowOnSelected = 0;
        // 
        private List<DefectCell> _defectCells = new List<DefectCell>(); // replace with DataBaseManager.xxx
        private List<DisplayWindow> _displayWindows = new List<DisplayWindow>(); // may need window number control.

        private DataBaseManager _dataBaseManager;
        private MarkDataBase _dataBase;

        public DisplayView()
        {
            InitializeComponent();
            DataBaseInit();

            // 
            _displayWindows.Add(displayWindow1);
        }

        private void DataBaseInit()
        {
            _dataBase = new MarkDataBase();
            _dataBaseManager = new DataBaseManager(_dataBase);

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

            comboBoxProduct.DataSource = _dataBaseManager.ProductNameList;
            comboBoxProduct.Text = _dataBaseManager.ProductName;

            comboBoxBatch.DataSource = _dataBaseManager.BatchNameList;
            comboBoxBatch.Text = _dataBaseManager.BatchName;

            comboBoxBoard.DataSource = _dataBaseManager.BoardNameList;
            comboBoxBoard.Text = _dataBaseManager.BoardName;

            comboBoxSide.DataSource = _dataBaseManager.SideNameList;
            comboBoxSide.Text = _dataBaseManager.SideName;

            comboBoxShot.DataSource = _dataBaseManager.ShotNameList;
            comboBoxShot.Text = _dataBaseManager.ShotName;

            comboBoxDefect.DataSource = _dataBaseManager.DefectNameList;
            comboBoxDefect.Text = _dataBaseManager.DefectName;
            
            this.comboBoxProduct.TextChanged += new System.EventHandler(this.comboBoxProduct_TextChanged);
            this.comboBoxBatch.TextChanged += new System.EventHandler(this.comboBoxBatch_TextChanged);
            this.comboBoxBoard.TextChanged += new System.EventHandler(this.comboBoxBoard_TextChanged);
            this.comboBoxSide.TextChanged += new System.EventHandler(this.comboBoxSide_TextChanged);
            this.comboBoxShot.TextChanged += new System.EventHandler(this.comboBoxShot_TextChanged);
            this.comboBoxDefect.TextChanged += new System.EventHandler(this.comboBoxDefect_TextChanged);
        }

        private void comboBoxProduct_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchProduct(comboBoxProduct.Text);
            ComboBoxRefresh();
            _hasButtonPressed = true;

            return;
        }

        private void comboBoxBatch_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchBatch(comboBoxBatch.Text);
            ComboBoxRefresh();
            _hasButtonPressed = true;

            return;
        }

        private void comboBoxBoard_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchBoard(comboBoxBoard.Text);
            ComboBoxRefresh();
            _hasButtonPressed = true;

            return;
        }

        private void comboBoxSide_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchSide(comboBoxSide.Text);
            ComboBoxRefresh();
            _hasButtonPressed = true;

            return;
        }

        private void comboBoxShot_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchShot(comboBoxShot.Text);
            ComboBoxRefresh();
            _hasButtonPressed = true;

            return;
        }

        private void comboBoxDefect_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchDefect(comboBoxDefect.Text);
            ComboBoxRefresh();
            _hasButtonPressed = true;

            return;
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

        private void RefreshDefectCells()
        {
            _defectCells.Clear();
            _dataBaseManager.GetDefectGroup(_numberOfDispalyWindows, out _defectCells);

            return;
        }

        private void RefreshDisplayWindows()
        {
            RefreshDefectCells();
            if (null == _defectCells || null == _displayWindows)
            {
                return;
            }
            if (_defectCells.Count != _displayWindows.Count)
            {
                MessageBox.Show("数量异常!");

                return;
            }
            for (int indexOfDisplayWindow = 0; indexOfDisplayWindow < _displayWindows.Count; ++indexOfDisplayWindow)
            {
                _displayWindows[indexOfDisplayWindow].defectCell = _defectCells[indexOfDisplayWindow];
                _displayWindows[indexOfDisplayWindow].RefreshWindow();
            }


            return;
        }

        private void MoveCursorToDisplayWindow(int indexOfWindow, List<DisplayWindow> displayWindows)
        {
            if (displayWindows == null)
            {
                return;
            }
            foreach (var displayWindow in displayWindows)
            {
                if (displayWindows.IndexOf(displayWindow) == indexOfWindow)
                {
                    displayWindow.BorderStyle = BorderStyle.Fixed3D;
                }
                else
                {
                    displayWindow.BorderStyle = BorderStyle.None;
                }
            }

            return;
        }

        private void MoveForwardCursor()
        {
            _indexOfDisplayWindowOnSelected = (_indexOfDisplayWindowOnSelected + 1) % _numberOfDispalyWindows;

            return;
        }

        private void MoveBackwardCursor()
        {
            _indexOfDisplayWindowOnSelected = (_indexOfDisplayWindowOnSelected - 1) % _numberOfDispalyWindows;
            if (_indexOfDisplayWindowOnSelected < 0)
            {
                _indexOfDisplayWindowOnSelected += _numberOfDispalyWindows;
            }

            return;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (_hasButtonPressed)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
            switch (keyData)
            {
                // TODO: add manager codes.
                // treate the _indexOfDisplayWindowOnSelected here.
                // and determine whether to RefreshDisplayWindows() or not, according to _indexOfDisplayWindowOnSelected.
                case Keys.Right:
                    if (_dataBaseManager.TrySwitchBackward())
                    {
                        MoveForwardCursor();
                        _hasButtonPressed = true;
                        ComboBoxRefresh();
                    }
                    else
                    {
                        MessageBox.Show("完了！");
                    }
                    break;
                case Keys.Left:
                    if (_dataBaseManager.TrySwitchForward())
                    {
                        MoveBackwardCursor();
                        _hasButtonPressed = true;
                        ComboBoxRefresh();
                    }
                    else
                    {
                        MessageBox.Show("完了！");
                    }
                    break;
                case Keys.Up:
                case Keys.Down:
                    break;
                default:
                    break;
            }
            MoveCursorToDisplayWindow(_indexOfDisplayWindowOnSelected, _displayWindows);

            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_hasButtonPressed)
            {
                RefreshDisplayWindows();
                _hasButtonPressed = false;
            }

            return;
        }

        private void DisplayView_Enter(object sender, EventArgs e)
        {
            this.timer1.Start();
            RefreshDisplayWindows();

            return;
        }

        private void DisplayView_Leave(object sender, EventArgs e)
        {
            this.timer1.Stop();
            RefreshDisplayWindows();

            return;
        }
    }
}
