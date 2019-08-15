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
        public DefectCell defectCell { get; set; }

        public DisplayWindow()
        {
            InitializeComponent();
            ShowModelWindow();
            Refresh();
        }

        private void ConvertRectanglesToAqShapes(List<Rectangle> rectangles, out List<AqShap> aqShapes)
        {
            aqShapes = new List<AqShap>();
            List<double> posYs = new List<double>();
            List<double> posXs = new List<double>();
            List<int> pointsNums = new List<int>();

            if (null == rectangles)
            {
                return;
            }
            foreach (var rectangle in rectangles)
            {
                posYs.Add(rectangle.Y);
                posYs.Add(rectangle.Y + rectangle.Width);
                posYs.Add(rectangle.Y + rectangle.Width);
                posYs.Add(rectangle.Y);
                posYs.Add(rectangle.Y);

                posXs.Add(rectangle.X);
                posXs.Add(rectangle.X);
                posXs.Add(rectangle.X + rectangle.Height);
                posXs.Add(rectangle.X + rectangle.Height);
                posXs.Add(rectangle.X);

                pointsNums.Add(5);
            }
            DisplayContour.GetContours(posYs, posXs, pointsNums, out aqShapes);

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
            this.aqDisplayOfCheck.InteractiveGraphics.Clear();
            if (null == defectCell || null == defectCell.DefectImage)
            {
                return;
            }
            this.aqDisplayOfCheck.Image = defectCell.DefectImage.Clone() as Bitmap;
            ConvertRectanglesToAqShapes(defectCell.Info.SubDefectList, out var aqShapes);
            DisplayContour.Display(aqDisplayOfCheck, aqShapes);
            this.aqDisplayOfCheck.FitToScreen();
            this.aqDisplayOfCheck.Update();

            return;
        }

        private void RefreshAqDispayOfModel()
        {
            this.aqDisplayOfModel.InteractiveGraphics.Clear();
            if (null == defectCell || null == defectCell.TemplateImage)
            {
                return;
            }
            this.aqDisplayOfModel.Image = defectCell.TemplateImage.Clone() as Bitmap;
            this.aqDisplayOfModel.FitToScreen();
            this.aqDisplayOfModel.Update();

            return;
        }

        public void RefreshWindow()
        {
            // remove it or not is all ok.
            /*
            if (null == defectCell)
            {
                this.Hide();

                return;
            }
            */
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
