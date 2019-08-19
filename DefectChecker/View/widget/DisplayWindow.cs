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
using DefectChecker.DefectDataStructure;
using AqVision.Graphic.AqVision.shape;

namespace DefectChecker.View.widget
{
    public partial class DisplayWindow : UserControl
    {
        private DefectCell _defectCell { get; set; }

        public DisplayWindow()
        {
            InitializeComponent();
            ShowModelWindow();
            Refresh();
        }

        private void ConvertRectanglesToAqShapes(DefectRegion defectRegionInfo, out List<AqShap> aqShapes)
        {
            aqShapes = new List<AqShap>();
            DisplayContour.GetContours(defectRegionInfo.XldYs, defectRegionInfo.XldXs, defectRegionInfo.XldPointCount, out aqShapes);
            return;
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
            if (!IsModelWindowHiden())
            {
                RefreshAqDispayOfModel();
            }

            return;
        }

        private void RefreshAqDisplayOfCheck()
        {
            this.aqDisplayOfCheck.Show();
            this.aqDisplayOfCheck.InteractiveGraphics.Clear();
            if (null == _defectCell || null == _defectCell.DefectImage)
            {
                this.aqDisplayOfCheck.Hide();

                return;
            }
            this.aqDisplayOfCheck.Image = _defectCell.DefectImage.Clone() as Bitmap;
            ConvertRectanglesToAqShapes(_defectCell.DefectRegionInfo, out var aqShapes);
            DisplayContour.Display(aqDisplayOfCheck, aqShapes);
            this.aqDisplayOfCheck.FitToScreen();
            this.aqDisplayOfCheck.Update();

            return;
        }

        private void RefreshAqDispayOfModel()
        {
            this.aqDisplayOfModel.Show();
            this.aqDisplayOfModel.InteractiveGraphics.Clear();
            if (null == _defectCell || null == _defectCell.TemplateImage)
            {
                this.aqDisplayOfModel.Hide();

                return;
            }
            this.aqDisplayOfModel.Image = _defectCell.TemplateImage.Clone() as Bitmap;
            this.aqDisplayOfModel.FitToScreen();
            this.aqDisplayOfModel.Update();

            return;
        }

        public void SetDefectCell(DefectCell defectCell)
        {
            _defectCell = null == defectCell ? new DefectCell() : defectCell;

            return;
        }

        public void RefreshWindow()
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
