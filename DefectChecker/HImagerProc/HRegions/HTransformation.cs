using Aqrose.Framework.Utility.Tools;
using AqVision.Graphic.AqVision.shape;
using HalconDotNet;
using System.Collections.Generic;
using System.Drawing;

namespace DefectChecker.HImagerProc.HRegions
{
    public class HTransformation
    {
        private HTransformation_Machine _hdvelop = new HTransformation_Machine();

        private void ConvertSingleHRegionToPoints(HObject region, out List<double> posYs, out List<double> posXs, out List<int> pointsNums)
        {
            posYs = new List<double>();
            posXs = new List<double>();
            pointsNums = new List<int>();

            _hdvelop.convert_single_region_to_points(region, out var hv_posYs,
    out var hv_posXs, out var hv_pointsNum);
            if (null != hv_posYs && 0 != hv_posYs.Length)
            {
                posYs.AddRange(hv_posYs.DArr);
            }
            if (null != hv_posXs && 0 != hv_posXs.Length)
            {
                posXs.AddRange(hv_posXs.DArr);
            }
            if (null != hv_pointsNum && 0 != hv_posXs.Length)
            {
                pointsNums.Add(hv_pointsNum.I);
            }

            return;
        }

        private void ConvertHRegionsToAqShapes(List<HObject> regions, out List<AqShap> aqShapes)
        {
            aqShapes = new List<AqShap>();
            List<double> posYs = new List<double>();
            List<double> posXs = new List<double>();
            List<int> pointsNums = new List<int>();

            if (null == regions)
            {
                return;
            }

            foreach (var region in regions)
            {
                ConvertSingleHRegionToPoints(region, out var posYsOfElement, out var posXsOfElement, out var pointsNumsOfElement);
                posYs.AddRange(posYsOfElement);
                posXs.AddRange(posXsOfElement);
                pointsNums.AddRange(pointsNumsOfElement);
            }
            DisplayContour.GetContours(posYs, posXs, pointsNums, out aqShapes);

            return;
        }

        private void ConvertRectanglesToHRegions(List<Rectangle> rectangles, out List<HObject> hObjects)
        {
            hObjects = new List<HObject>();
            if (null == rectangles)
            {
                return;
            }
            foreach (var rectangle in rectangles)
            {
                _hdvelop.convert_rectangle_to_region(out var hRegion, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
                hObjects.Add(hRegion);
            }

            return;
        }

        private List<HObject> ConvertHRegionsToCircles(List<HObject> region)
        {
            throw new System.Exception();
        }

        private HObject Dilation(HObject region, int elementSize)
        {
            throw new System.Exception();
        }

        public void ConvertRectanglesToAqShapesInRect(List<Rectangle> rectangles, out List<AqShap> aqRectShapes, bool isUnion = false)
        {
            ConvertRectanglesToHRegions(rectangles, out var rectRegions);
            if (isUnion)
            {
                throw new System.Exception();
            }
            ConvertHRegionsToAqShapes(rectRegions, out aqRectShapes);

            return;
        }

        public void ConvertRectanglesToAqShapesInCircle(List<Rectangle> rectangles, out List<AqShap> aqRectShapes, bool isUnion = false)
        {
            ConvertRectanglesToHRegions(rectangles, out var rectRegions);
            var circleRegions = ConvertHRegionsToCircles(rectRegions);
            if (isUnion)
            {
                throw new System.Exception();
            }
            ConvertHRegionsToAqShapes(circleRegions, out aqRectShapes);

            return;
        }
    }

    partial class HTransformation_Machine
    {
        public void convert_rectangle_to_region(out HObject ho_region, HTuple hv_Left,
    HTuple hv_Top, HTuple hv_Right, HTuple hv_Bottom)
        {



            // Local control variables 

            HTuple hv_Exception = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_region);
            //version: v0.0.1
            try
            {
                ho_region.Dispose();
                HOperatorSet.GenRectangle1(out ho_region, hv_Left.TupleReal(), hv_Top.TupleReal(), hv_Right.TupleReal(), hv_Bottom.TupleReal());
            }
            // catch (Exception) 
            catch (HalconException HDevExpDefaultException1)
            {
                HDevExpDefaultException1.ToHTuple(out hv_Exception);
                ho_region.Dispose();
                HOperatorSet.GenEmptyRegion(out ho_region);
            }


            return;
        }

        public void convert_single_region_to_points(HObject ho_singleRegion, out HTuple hv_posYs,
            out HTuple hv_posXs, out HTuple hv_pointsNum)
        {



            // Local iconic variables 

            HObject ho_Contours = null;

            // Local control variables 

            HTuple hv_Exception = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Contours);
            hv_posYs = new HTuple();
            hv_posXs = new HTuple();
            hv_pointsNum = new HTuple();
            try
            {
                //version: v0.0.1

                try
                {
                    ho_Contours.Dispose();
                    HOperatorSet.GenContourRegionXld(ho_singleRegion, out ho_Contours, "border");
                    HOperatorSet.GetContourXld(ho_Contours, out hv_posYs, out hv_posXs);
                    hv_pointsNum = new HTuple(hv_posYs.TupleLength());
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    hv_posYs = new HTuple();
                    hv_posXs = new HTuple();
                    hv_pointsNum = new HTuple();
                }

                ho_Contours.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Contours.Dispose();

                throw HDevExpDefaultException;
            }
        }

    }
}
