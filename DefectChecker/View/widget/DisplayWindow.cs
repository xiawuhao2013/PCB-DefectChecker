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

namespace DefectChecker.View.widget
{
    public partial class DisplayWindow : UserControl
    {
        private List<double> _posXsOfRoi = new List<double>();
        private List<double> _posYsOfRoi = new List<double>();
        private List<int> _pointsNumOfRoi = new List<int>();

        public bool IsShowModel { get; set; }

        public Bitmap ImageOfCheck = null;
        public Bitmap ImageOfModel = null;

        public DisplayWindow()
        {
            InitializeComponent();
            ShowModelWindow();
            Refresh();
        }

        private bool IsModelWindowHiden()
        {
            return this.splitContainer1.SplitterDistance == this.splitContainer1.Width;
        }

        private void RefreshTitle()
        {
            this.labelOfCheckTitle.Text = @"AAAAA";
            if (!IsModelWindowHiden())
            {
                this.labelOfModelTitle.Text = @"BBBBB";
            }

            return;
        }

        private void RefreshInfo()
        {
            this.labelOfCheckInfo.Text = @"aaaaa";
            if (!IsModelWindowHiden())
            {
                this.labelOfModelInfo.Text = @"bbbbb";
            }

            return;
        }

        private void RefreshAqDisplay()
        {
            RefreshAqDisplayOfCheck();
            if (0 != this.splitContainer1.Panel2.Width)
            {
                RefreshAqDispayOfModel();
            }

            return;
        }

        private void RefreshAqDisplayOfCheck()
        {
            this.aqDisplayOfCheck.InteractiveGraphics.Clear();
            if (null == ImageOfCheck)
            {
                return;
            }
            this.aqDisplayOfCheck.Image = ImageOfCheck;
            DisplayContour.GetContours(_posYsOfRoi, _posXsOfRoi, _pointsNumOfRoi, out var aqShapes);
            DisplayContour.Display(aqDisplayOfCheck, aqShapes);
            this.aqDisplayOfCheck.FitToScreen();
            this.aqDisplayOfCheck.Update();

            return;
        }

        private void RefreshAqDispayOfModel()
        {
            this.aqDisplayOfModel.InteractiveGraphics.Clear();
            if (null == ImageOfModel)
            {
                return;
            }
            this.aqDisplayOfModel.Image = ImageOfModel;
            this.aqDisplayOfModel.FitToScreen();
            this.aqDisplayOfModel.Update();

            return;
        }

        public void Refresh()
        {
            RefreshAqDisplay();
            RefreshTitle();
            RefreshInfo();

            return;
        }

        public void HideModelWindow()
        {
            this.splitContainer1.SplitterDistance = this.splitContainer1.Width;

            return;
        }

        public void ShowModelWindow()
        {
            this.splitContainer1.Panel2.Show();

            return;
        }
    }
}
