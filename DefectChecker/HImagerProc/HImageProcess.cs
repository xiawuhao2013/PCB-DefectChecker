using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectChecker.HImagerProc
{
    class HImageProcess
    {
        public static void GenDefectRegions(Bitmap bitmap, List<Rectangle> rectangles,
            out List<double> xldXs, out List<double> xldYs, out List<int> xldPointCount,
            out List<List<int>> defectIndexList)
        {
            xldXs = new List<double>();
            xldYs = new List<double>();
            xldPointCount = new List<int>();
            defectIndexList = new List<List<int>>();
            if (rectangles == null || rectangles.Count<=0)
            {
                return;
            }
            for (int i = 0; i < rectangles.Count; i++)
            {
                var rectangle = rectangles[i];
                List<int> defectIndex = new List<int>();
                xldYs.Add(rectangle.Y);
                xldYs.Add(rectangle.Y + rectangle.Height);
                xldYs.Add(rectangle.Y + rectangle.Height);
                xldYs.Add(rectangle.Y);
                xldYs.Add(rectangle.Y);

                xldXs.Add(rectangle.X);
                xldXs.Add(rectangle.X);
                xldXs.Add(rectangle.X + rectangle.Width);
                xldXs.Add(rectangle.X + rectangle.Width);
                xldXs.Add(rectangle.X);

                xldPointCount.Add(5);
                defectIndex.Add(i);
                defectIndexList.Add(defectIndex);
            }
        }

        static void HGenDefectRegions()
        {

        }
    }
}
