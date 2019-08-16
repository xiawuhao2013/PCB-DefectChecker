using Aqrose.Framework.Utility.Tools;
using AqVision.Graphic.AqVision.shape;
using HalconDotNet;
using System.Collections.Generic;
using System.Drawing;

namespace DefectChecker.HImagerProc.HRegions
{
    public class HTransformation
    {
        private void ConvertHRegionToPoints(HRegion region, out List<double> posYs, out List<double> posXs, out List<int> pointsNums)
        {
            throw new System.Exception();
        }

        private List<AqShap> ConvertHRegionsToAqShapes(List<HRegion> regions)
        {
            List<AqShap> aqShapes = new List<AqShap>();
            List<double> posYs = new List<double>();
            List<double> posXs = new List<double>();
            List<int> pointsNums = new List<int>();

            if (null == regions)
            {
                return aqShapes;
            }

            foreach (var region in regions)
            {
                ConvertHRegionToPoints(region, out var posYsOfElement, out var posXsOfElement, out var pointsNumsOfElement);
                posYs.AddRange(posYsOfElement);
                posXs.AddRange(posXsOfElement);
                pointsNums.AddRange(pointsNumsOfElement);
            }
            DisplayContour.GetContours(posYs, posXs, pointsNums, out aqShapes);

            return aqShapes;
        }

        private List<HRegion> ConvertRectanglesToHRegions(List<Rectangle> rectangles, bool isUnion = false)
        {
            throw new System.Exception();
        }

        private List<HRegion> ConvertHRegionsToCircles(List<HRegion> region, bool isUnion = false)
        {
            throw new System.Exception();
        }

        private HRegion Dilation(HRegion region, int elementSize)
        {
            throw new System.Exception();
        }

        public void ConvertRectanglesToAqShapesInRect(List<Rectangle> rectangles, out List<AqShap> aqRectShapes, bool isUnion = false)
        {
            var rectRegions = ConvertRectanglesToHRegions(rectangles, isUnion);
            aqRectShapes = ConvertHRegionsToAqShapes(rectRegions);

            return;
        }

        public void ConvertRectanglesToAqShapesInCircle(List<Rectangle> rectangles, out List<AqShap> aqRectShapes, bool isUnion = false)
        {
            var rectRegions = ConvertRectanglesToHRegions(rectangles, isUnion);
            var circleRegions = ConvertHRegionsToCircles(rectRegions, isUnion);
            aqRectShapes = ConvertHRegionsToAqShapes(circleRegions);

            return;
        }
    }
}
