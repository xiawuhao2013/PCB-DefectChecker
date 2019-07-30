using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefectChecker.View
{
    public partial class DisplayView : UserControl
    {
        private DispalyViewOfCells _displayViewOfCells = new DispalyViewOfCells();

        public DisplayView()
        {
            InitializeComponent();
            this.panelOfInfo.Visible = false;
            RefreshDiplayViewCells();
        }

        private void RefreshDiplayViewCells()
        {
            this.splitContainer1.Panel2.Controls.Clear();
            _displayViewOfCells.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Add(_displayViewOfCells);

            return;
        }

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
    }
}
