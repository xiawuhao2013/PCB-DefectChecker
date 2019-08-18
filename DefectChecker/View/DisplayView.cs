using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AqVision.Controls;
using DefectChecker.DataBase;
using DefectChecker.DefectDataStructure;
using DefectChecker.View.widget;

namespace DefectChecker.View
{
    public partial class DisplayView : UserControl
    {
        private const int _maxNumberOfDisplayWindows = 10;
        private int _numberOfDispalyWindows = 1;
        // 
        private List<DefectCell> _defectCells = new List<DefectCell>(); // replace with DataBaseManager.xxx
        private List<DisplayWindow> _displayWindows = new List<DisplayWindow>(); // may need window number control.

        private Bitmap _bitmapGerberSideA;
        private Bitmap _bitmapGerberSideB;
        private AqDisplay _aqDisplayGerberSideA = new AqDisplay();
        private AqDisplay _aqDisplayGerberSideB = new AqDisplay();

        private DataBaseManager _dataBaseManager = new DataBaseManager();

        public DisplayView()
        {
            InitializeComponent();
            InitGerberWindows();
            InitDisplayWindows(this.tableLayoutPanelImage, _numberOfDispalyWindows);
            
            InitDataBase();
            //timer1.Start();
        }

        #region Init

        private void InitGerberWindows()
        {
            this.tableLayoutPanelGerber.Controls.Clear();
            this.tableLayoutPanelGerber.RowStyles.Clear();
            this.tableLayoutPanelGerber.ColumnStyles.Clear();
            this.tableLayoutPanelGerber.RowCount = 1;
            this.tableLayoutPanelGerber.ColumnCount = 2;
            this.tableLayoutPanelGerber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelGerber.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelGerber.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            _aqDisplayGerberSideA.Dock = DockStyle.Fill;
            _aqDisplayGerberSideB.Dock = DockStyle.Fill;
            this.tableLayoutPanelGerber.Controls.Add(_aqDisplayGerberSideA, 0, 0);
            this.tableLayoutPanelGerber.Controls.Add(_aqDisplayGerberSideB, 1, 0);
        }

        private void InitDisplayWindows(TableLayoutPanel table, int number)
        {
            _displayWindows.Clear();
            table.Controls.Clear();
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();

            number = Math.Max(1, number);
            number = Math.Min(_maxNumberOfDisplayWindows, number);
            table.RowCount = Convert.ToInt32(Math.Round(Math.Sqrt(number)));
            table.ColumnCount = Convert.ToInt32(Math.Ceiling(Math.Sqrt(number)));

            for (int count = 0; count < table.RowCount; ++count)
            {
                table.RowStyles.Add(new RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            }
            for (int count = 0; count < table.ColumnCount; ++count)
            {
                table.ColumnStyles.Add(new ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            }

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

        private void InitDataBase()
        {
            RefreshComboBox();
            RefreshGerberWindows();
            RefreshDisplayWindows();
            //_aqDisplayGerberSideA.Image
        }

        #endregion

        #region Refresh Windows
        private void RefreshComboBox()
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

        private void RefreshGerberWindows()
        {
            _dataBaseManager.GetGerberImage("SideA", out _bitmapGerberSideA);
            _dataBaseManager.GetGerberImage("SideB", out _bitmapGerberSideB);
            _aqDisplayGerberSideA.Image = _bitmapGerberSideA;
            _aqDisplayGerberSideB.Image = _bitmapGerberSideB;
            _aqDisplayGerberSideA.FitToScreen();
            _aqDisplayGerberSideB.FitToScreen();
        }
        
        private void RefreshDisplayWindows()
        {
            _defectCells.Clear();
            _dataBaseManager.GetDefectGroup(_numberOfDispalyWindows, out _defectCells);
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

        #endregion

        #region Event

        private void comboBoxProduct_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchProduct(comboBoxProduct.Text);
            RefreshComboBox();
            FocusCurrentDisplayWindow();
            RefreshDisplayWindows();
            return;
        }

        private void comboBoxBatch_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchBatch(comboBoxBatch.Text);
            RefreshComboBox();
            FocusCurrentDisplayWindow();
            RefreshDisplayWindows();
            return;
        }

        private void comboBoxBoard_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchBoard(comboBoxBoard.Text);
            RefreshComboBox();
            FocusCurrentDisplayWindow();
            RefreshDisplayWindows();
            return;
        }

        private void comboBoxSide_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchSide(comboBoxSide.Text);
            RefreshComboBox();
            FocusCurrentDisplayWindow();
            RefreshDisplayWindows();
            return;
        }

        private void comboBoxShot_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchShot(comboBoxShot.Text);
            RefreshComboBox();
            FocusCurrentDisplayWindow();
            RefreshDisplayWindows();
            return;
        }

        private void comboBoxDefect_TextChanged(object sender, EventArgs e)
        {
            _dataBaseManager.SwitchDefect(comboBoxDefect.Text);
            RefreshComboBox();
            FocusCurrentDisplayWindow();
            RefreshDisplayWindows();
            return;
        }

        #endregion


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
            switch (keyData)
            {
                // TODO: add manager codes.
                // treate the _indexOfDisplayWindowOnSelected here.
                // and determine whether to InitDisplayWindows() or not, according to _indexOfDisplayWindowOnSelected.
                case Keys.Right:
                    if (_dataBaseManager.TrySwitchBackward())
                    {
                        RefreshComboBox();
                        FocusCurrentDisplayWindow();
                        RefreshDisplayWindows();
                    }
                    else
                    {
                        MessageBox.Show("完了！");
                    }
                    break;
                case Keys.Left:
                    if (_dataBaseManager.TrySwitchForward())
                    {
                        RefreshComboBox();
                        FocusCurrentDisplayWindow();
                        RefreshDisplayWindows();
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
            if (_dataBaseManager.TrySwitchBackward())
            {
                RefreshComboBox();
                FocusCurrentDisplayWindow();
                RefreshDisplayWindows();
            }
            else
            {
                MessageBox.Show("完了！");
            }
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (_hasButtonPressed)
        //    {
        //        RefreshComboBox();
        //        FocusCurrentDisplayWindow();
        //        RefreshDisplayWindows();
        //        _hasButtonPressed = false;
        //    }

        //    return;
        //}
    }
}
