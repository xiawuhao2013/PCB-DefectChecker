using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefectChecker.HImagerProc;

namespace DefectChecker.DefectDataStructure
{
    public class DefectCell
    {
        private int _dilationPixel = 8;

        public Bitmap DefectImage { get; set; }
        public Bitmap TemplateImage { get; set; }
        public Bitmap GerberImage { get; set; }
        public Rectangle RoiInTemplate { get; set; }
        public List<DefectInfo> DefectInfos { get; set; }
        public DefectRegion DefectRegionInfo { get; set; }

        public DefectCell()
        {
            DefectImage = null;
            TemplateImage = null;
            GerberImage = null;
            RoiInTemplate = new Rectangle(0,0,0,0);
            DefectInfos = new List<DefectInfo>();
            DefectRegionInfo = new DefectRegion();
        }

        public bool GenRegionFromRect()
        {
            if (DefectImage == null)
            {
                return false;
            }

            var rects = new List<Rectangle>();
            foreach (var defectInfo in DefectInfos)
            {
                rects.Add(defectInfo.DefectRect);
            }

            List<double> xldXs;
            List<double> xldYs;
            List<int> xldPointCount;
            List<List<int>> defectIndexList;
            HImageProcess.GenDefectRegions(DefectImage, rects, 5.5, out xldXs, out xldYs, out xldPointCount, out defectIndexList);
            DefectRegionInfo.XldXs = xldXs;
            DefectRegionInfo.XldYs = xldYs;
            DefectRegionInfo.XldPointCount = xldPointCount;
            DefectRegionInfo.DefectIndexList = defectIndexList;
            return true;
        }
    }
}
