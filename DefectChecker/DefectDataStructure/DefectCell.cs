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
        private List<SingleDefectRegion> _defectRegions = new List<SingleDefectRegion>();

        public Bitmap DefectImage { get; set; }
        public Bitmap TemplateImage { get; set; }
        public Bitmap GerberImage { get; set; }
        public Rectangle RoiInTemplate { get; set; }
        public List<DefectInfo> DefectInfos { get; set; }
        public List<SingleDefectRegion> DefectRegions
        {
            get { return _defectRegions; }
            set { _defectRegions = value; }
        }

        public DefectCell()
        {
            DefectImage = null;
            TemplateImage = null;
            GerberImage = null;
            RoiInTemplate = new Rectangle(0,0,0,0);
            DefectInfos = new List<DefectInfo>();
            DefectRegions = new List<SingleDefectRegion>();
        }

        public bool GenRegionFromRect(int dilationPixel)
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

            HImageProcess.GenDefectRegions(DefectImage, rects, dilationPixel, out _defectRegions);
            return true;
        }
    }
}
