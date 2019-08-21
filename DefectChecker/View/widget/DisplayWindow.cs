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

        private bool IsModelWindowHiden()
        {
            return this.splitContainer1.SplitterDistance == this.splitContainer1.Width;
        }

        private void RefreshTitle()
        {
            //this.labelOfCheckTitle.Text = @"AAAAA";
            //if (!IsModelWindowHiden())
            //{
            //    this.labelOfModelTitle.Text = @"BBBBB";
            //}

            return;
        }

        private void RefreshInfo()
        {
            //this.labelOfCheckInfo.Text = @"aaaaa";
            //if (!IsModelWindowHiden())
            //{
            //    this.labelOfModelInfo.Text = @"bbbbb";
            //}

            return;
        }

        private void RefreshAqDisplay(bool isSelected = false, int indexOfDefectRegion = 0)
        {
            RefreshAqDisplayOfCheck(isSelected, indexOfDefectRegion);
            if (!IsModelWindowHiden())
            {
                RefreshAqDispayOfModel();
            }

            return;
        }

        private void RefreshAqDisplayOfCheck(bool isSelected = false, int indexOfDefectRegion = 0)
        {
            this.aqDisplayOfCheck.Show();
            this.aqDisplayOfCheck.InteractiveGraphics.Clear();
            if (null == _defectCell || null == _defectCell.DefectImage)
            {
                this.aqDisplayOfCheck.Hide();

                return;
            }
            this.aqDisplayOfCheck.Image = _defectCell.DefectImage.Clone() as Bitmap;
            foreach (var defectRegion in _defectCell.DefectRegions)
            {
                List<AqShap> aqShape = new List<AqShap>();
                if (isSelected && indexOfDefectRegion == _defectCell.DefectRegions.IndexOf(defectRegion))
                {
                    DisplayContour.GetContours(defectRegion.XldYs, defectRegion.XldXs, defectRegion.XldPointCount, out aqShape, AqVision.Graphic.AqColorEnum.Green);
                }
                else
                {
                    DisplayContour.GetContours(defectRegion.XldYs, defectRegion.XldXs, defectRegion.XldPointCount, out aqShape, AqVision.Graphic.AqColorEnum.Red);
                }
                DisplayContour.Display(aqDisplayOfCheck, aqShape);
            }
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

        public void RefreshWindow(bool isSelected = false, int indexOfDefectRegion = 0)
        {
            RefreshAqDisplay(isSelected, indexOfDefectRegion);
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
