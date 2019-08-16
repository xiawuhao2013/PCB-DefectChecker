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
        private const int _maxNumberOfDisplayWindows = 10;
        private int _numberOfDispalyWindows = 5;
        private bool _hasButtonPressed = false;
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
            RefreshDisplayWindows(this.tableLayoutPanelImage, _numberOfDispalyWindows);
        }

        #region DisplayWindowNumber

        private void ClearTable(TableLayoutPanel table)
        {
            table.Controls.Clear();
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();

            return;
        }

        private void UniformTable(TableLayoutPanel table)
        {
            for (int count = 0; count < table.RowCount; ++count)
            {
                table.RowStyles.Add(new RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            }
            for (int count = 0; count < table.ColumnCount; ++count)
            {
                table.ColumnStyles.Add(new ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            }

            return;
        }

        private void SetTable(TableLayoutPanel table, int numberOfCell)
        {
            numberOfCell = Math.Max(1, numberOfCell);
            numberOfCell = Math.Min(_maxNumberOfDisplayWindows, numberOfCell);
            table.RowCount = Convert.ToInt32(Math.Round(Math.Sqrt(numberOfCell)));
            table.ColumnCount = Convert.ToInt32(Math.Ceiling(Math.Sqrt(numberOfCell)));

            return;
        }

        private void RefreshDisplayWindows(TableLayoutPanel table, int number)
        {
            _displayWindows.Clear();
            ClearTable(table);
            SetTable(table, number);
            UniformTable(table);

            for (int index = 0; index < number; ++index)
            {
                DisplayWindow widget = new DisplayWindow();
                widget.Dock = DockStyle.Fill;
                widget.Margin = new System.Windows.Forms.Padding(1);
                _displayWindows.Add(widget);
                table.Controls.Add(widget, index % number, index / number);
            }

            return;
        }

        #endregion

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
            _hasButtonPressed = true;

            return;
        }

        private void comboBoxBatch_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchBatch(comboBoxBatch.Text);
            _hasButtonPressed = true;

            return;
        }

        private void comboBoxBoard_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchBoard(comboBoxBoard.Text);
            _hasButtonPressed = true;

            return;
        }

        private void comboBoxSide_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchSide(comboBoxSide.Text);
            _hasButtonPressed = true;

            return;
        }

        private void comboBoxShot_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchShot(comboBoxShot.Text);
            _hasButtonPressed = true;

            return;
        }

        private void comboBoxDefect_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchDefect(comboBoxDefect.Text);
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
                _displayWindows[indexOfDisplayWindow].SetDefectCell(_defectCells[indexOfDisplayWindow]);
                _displayWindows[indexOfDisplayWindow].RefreshWindow();
            }


            return;
        }

        private void FocusCurrentDisplayWindow()
        {
            int focus = -1;
            if (null == _displayWindows)
            {
                return;
            }
            if (_dataBaseManager.DefectNameList != null && _dataBaseManager.DefectNameList.Contains(_dataBaseManager.DefectName))
            {
                focus = _dataBaseManager.DefectNameList.IndexOf(_dataBaseManager.DefectName) % _numberOfDispalyWindows;
            }

            foreach (var displayWindow in _displayWindows)
            {
                if (-1 != focus && _displayWindows.IndexOf(displayWindow) == focus)
                {
                    displayWindow.BorderStyle = BorderStyle.Fixed3D;
                    continue;
                }
                displayWindow.BorderStyle = BorderStyle.None;
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
                        _hasButtonPressed = true;
                    }
                    else
                    {
                        MessageBox.Show("完了！");
                    }
                    break;
                case Keys.Left:
                    if (_dataBaseManager.TrySwitchForward())
                    {
                        _hasButtonPressed = true;
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

            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_hasButtonPressed)
            {
                ComboBoxRefresh();
                FocusCurrentDisplayWindow();
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
