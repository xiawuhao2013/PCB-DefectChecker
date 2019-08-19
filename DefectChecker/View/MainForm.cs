using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefectChecker.View
{
    public partial class MainForm : Form
    {
        private ConfigView _uiConfigView = new ConfigView();
        private DisplayView _displayView = new DisplayView();
        private DataBaseView _uiDataBaseView = new DataBaseView();

        public MainForm()
        {
            InitializeComponent();
            InitDisplay();
        }

        private void InitDisplay()
        {
            _uiConfigView.Dock = DockStyle.Fill;
            this.panelConfigView.Controls.Add(_uiConfigView);

            _uiDataBaseView.Dock = DockStyle.Fill;
            this.panelDataBaseView.Controls.Add(_uiDataBaseView);

            _displayView.Dock = DockStyle.Fill;
            this.panelOfDisplayView.Controls.Add(_displayView);
            this.tabControl1.SelectedIndex = 1;

            return;
        }
    }
}
