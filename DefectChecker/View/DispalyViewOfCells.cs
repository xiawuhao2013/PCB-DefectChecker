using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AqVision.Controls;
using Aqrose.Framework.Utility.Tools;

namespace DefectChecker.View
{
    public partial class DispalyViewOfCells : UserControl
    {
        private const string _paramFileOfUI = @"\ParamFile-UI.xml";
        private int _numberOfCell = 4;
        private Dictionary<int, AqDisplay> _cellViewList = new Dictionary<int, AqDisplay>();
        
        public Dictionary<int, AqDisplay> CellViewList { get { return _cellViewList; } }
        public int NumberOfCell { get { return _numberOfCell; } }

        public DispalyViewOfCells()
        {
            InitializeComponent();
            //
            LoadConfig();
            InitializeViewListOnTable(this.tableLayoutPanelOfDefectCells);
        }

        //


        private void ResetCellViewListOnTable(TableLayoutPanel table, int numberOfCell)
        {
            ResetNumberOfCell(numberOfCell);
            SaveConfig();
            InitializeViewListOnTable(table);

            return;
        }

        private void CalcWidgetsLayout(out int rowsOfCell, out int colsOfCell)
        {
            rowsOfCell = Convert.ToInt32(Math.Round(Math.Sqrt(_numberOfCell)));
            colsOfCell = Convert.ToInt32(Math.Ceiling(Math.Sqrt(_numberOfCell)));

            return;
        }

        private void UniforTableLayout(TableLayoutPanel table)
        {
            for (var i = 0; i < table.RowCount; ++i)
            {
                table.RowStyles.Add(new RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            }
            for (var i = 0; i < table.ColumnCount; ++i)
            {
                table.ColumnStyles.Add(new ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            }

            return;
        }

        private void ResetTableLayout(TableLayoutPanel table)
        {
            table.Controls.Clear();
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();

            CalcWidgetsLayout(out var rowsOfCell, out var colsOfCell);
            table.RowCount = rowsOfCell;
            table.ColumnCount = colsOfCell;

            UniforTableLayout(table);

            return;
        }

        private void InitializeViewListOnTable(TableLayoutPanel table)
        {
            ResetTableLayout(table);
            _cellViewList.Clear();
            for (var index = 0; index < _numberOfCell; ++index)
            {
                var cellView = new AqDisplay();
                cellView.Dock = DockStyle.Fill;
                cellView.Margin = new System.Windows.Forms.Padding(1);
                _cellViewList.Add(index, cellView);
                table.Controls.Add(cellView, index % _numberOfCell, index / _numberOfCell);
            }

            return;
        }

        private void ResetNumberOfCell(int number)
        {
            _numberOfCell = Math.Max(1, number);

            return;
        }

        private void LoadConfig()
        {
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.ReadParameter(Application.StartupPath + _paramFileOfUI);
            var res = xmlParameter.GetParamData(@"numberOfCell");
            if ("" != res)
            {
                _numberOfCell = Convert.ToInt32(res);
            }

            return;
        }

        private void SaveConfig()
        {
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.Add(@"numberOfCell", _numberOfCell);
            xmlParameter.WriteParameter(Application.StartupPath + _paramFileOfUI);

            return;
        }


        //


        public void UpdateNumberOfCellDisplay()
        {
            ChangeDisplayNumForm form = new ChangeDisplayNumForm();
            form.DisplayNum = _numberOfCell;
            if (form.ShowDialog() == DialogResult.OK)
            {
                ResetCellViewListOnTable(this.tableLayoutPanelOfDefectCells, form.DisplayNum);
            }

            return;
        }
    }
}
