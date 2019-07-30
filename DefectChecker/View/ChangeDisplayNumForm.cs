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
    public partial class ChangeDisplayNumForm : Form
    {
        private int _displayNum = 1;

        public int DisplayNum
        {
            get { return _displayNum; }
            set
            {
                _displayNum = value;
                this.numericUpDown1.Value = _displayNum;
            }
        }

        public ChangeDisplayNumForm()
        {
            InitializeComponent();
            this.numericUpDown1.Maximum = 16;
            this.numericUpDown1.Minimum = 1;
            this.numericUpDown1.Value = DisplayNum;
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            DisplayNum = (int)this.numericUpDown1.Value;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
