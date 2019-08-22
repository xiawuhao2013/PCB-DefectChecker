using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aqrose.Framework.Utility.Tools;
using DefectChecker.DefectDataStructure;
using HalconDotNet;

namespace DefectChecker.HImagerProc
{
    class HImageProcess
    {
        public static void GenDefectRegions(Bitmap bitmap, List<Rectangle> rectangles, double radiusOfDilation, out List<SingleDefectRegion> defectRegions)
        {
            defectRegions = new List<SingleDefectRegion>();
            HObject ho_image;
            HObject ho_ConnectedDilations;

            HTuple hv_Row1OfRectangles = new HTuple();
            HTuple hv_Row2OfRectangles = new HTuple();
            HTuple hv_Column1OfRectangles = new HTuple();
            HTuple hv_Column2OfRectangles = new HTuple();
            HTuple hv_RadiusOfDilation = new HTuple();
            HTuple hv_IndexOfRectangles = new HTuple();
            HTuple hv_NumberListOfRectangles = new HTuple();

            // initialation
            HOperatorSet.GenEmptyObj(out ho_image);
            ho_image.Dispose();
            ImageOperateTools.Bitmap8HObjectBpp8(bitmap, out ho_image);
            for (int i = 0; i < rectangles.Count; i++)
            {
                var rectangle = rectangles[i];
                hv_Row1OfRectangles.Append(new HTuple(rectangle.Top));
                hv_Row2OfRectangles.Append(new HTuple(rectangle.Bottom));
                hv_Column1OfRectangles.Append(new HTuple(rectangle.Left));
                hv_Column2OfRectangles.Append(new HTuple(rectangle.Right));
            }
            hv_RadiusOfDilation = radiusOfDilation;
            dilate_regions_pro(ho_image, out ho_ConnectedDilations, hv_Row1OfRectangles, hv_Row2OfRectangles, hv_Column1OfRectangles, hv_Column2OfRectangles, hv_RadiusOfDilation, out hv_IndexOfRectangles, out hv_NumberListOfRectangles);
            List<int> defectInfoIndexList = new List<int>();
            List<int> defectInfoNumberList = new List<int>();
            if (hv_IndexOfRectangles.TupleLength() > 0 && hv_NumberListOfRectangles.TupleLength() > 0)
            {
                defectInfoIndexList.AddRange(hv_IndexOfRectangles.ToIArr());
                defectInfoNumberList.AddRange(hv_NumberListOfRectangles.ToIArr());
            }
            else
            {
                return;
            }

            HOperatorSet.CountObj(ho_ConnectedDilations, out var hv_NumberOfDilations);
            for (var index = 1; index <= hv_NumberOfDilations; ++index)
            {
                SingleDefectRegion defectRegion = new SingleDefectRegion();
                HOperatorSet.SelectObj(ho_ConnectedDilations, out var ho_Dilation, index);

                // DefectRegion.DefectInfoIndexList
                for (var cnt = 0; cnt < defectInfoNumberList[index-1]; ++cnt)
                {
                    defectRegion.DefectInfoIndexList.Add(defectInfoIndexList[0]);
                    defectInfoIndexList.RemoveAt(0);
                }

                // DefectRegion.Xldxxx
                convert_single_region_to_points(ho_Dilation, out var hv_posYs, out var hv_posXs, out var hv_pointsNum);
                if (hv_posYs.TupleLength() > 0)
                {
                    defectRegion.XldYs.AddRange(hv_posYs.ToDArr());
                }
                if (hv_posXs.TupleLength() > 0)
                {
                    defectRegion.XldXs.AddRange(hv_posXs.ToDArr());
                }
                if (hv_pointsNum.TupleLength() > 0)
                {
                    defectRegion.XldPointCount.AddRange(hv_pointsNum.ToIArr());
                }

                // DefectRegion.SmallestRect
                HOperatorSet.SmallestRectangle1(ho_Dilation, out var hv_Row1, out var hv_Column1, out var hv_Row2, out var hv_Column2);
                defectRegion.SmallestRect = Rectangle.FromLTRB(hv_Column1.I, hv_Row1.I, hv_Column2.I, hv_Row2.I);

                defectRegions.Add(defectRegion);
            }

            return;
        }



        static private void dilate_regions_pro(HObject ho_image, out HObject ho_ConnectedDilations,
      HTuple hv_Row1OfRectangles, HTuple hv_Row2OfRectangles, HTuple hv_Column1OfRectangles,
      HTuple hv_Column2OfRectangles, HTuple hv_RadiusOfDilation, out HTuple hv_IndexOfRectangles,
      out HTuple hv_NumberListOfRectangles)
        {




            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_OrgRectangles = null, ho_RegionDilation = null;
            HObject ho_RegionUnion = null, ho_ConnectedRegions = null, ho_RectangleDilationSelected = null;
            HObject ho_RectangleSelected = null;

            // Local control variables 

            HTuple hv_numOfRectangles = new HTuple();
            HTuple hv_Number = new HTuple(), hv_IndexOfConnectedRegions = new HTuple();
            HTuple hv_NumberOfRectangles = new HTuple(), hv_SubDefectIndexList = new HTuple();
            HTuple hv_IndexOfOrgRectangles = new HTuple(), hv_IsSubset = new HTuple();
            HTuple hv_Exception = null;
            HTuple hv_Column2OfRectangles_COPY_INP_TMP = hv_Column2OfRectangles.Clone();
            HTuple hv_RadiusOfDilation_COPY_INP_TMP = hv_RadiusOfDilation.Clone();
            HTuple hv_Row2OfRectangles_COPY_INP_TMP = hv_Row2OfRectangles.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ConnectedDilations);
            HOperatorSet.GenEmptyObj(out ho_OrgRectangles);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RectangleDilationSelected);
            HOperatorSet.GenEmptyObj(out ho_RectangleSelected);
            try
            {
                dev_update_off();
                HOperatorSet.SetSystem("clip_region", "true");
                //
                ho_ConnectedDilations.Dispose();
                HOperatorSet.GenEmptyObj(out ho_ConnectedDilations);
                //
                hv_IndexOfRectangles = new HTuple();
                hv_NumberListOfRectangles = new HTuple();
                //
                try
                {
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.DispObj(ho_image, HDevWindowStack.GetActive());
                    }
                    hv_numOfRectangles = 0;
                    HOperatorSet.TupleLength(hv_Row1OfRectangles, out hv_numOfRectangles);
                    if ((int)(new HTuple(hv_numOfRectangles.TupleLessEqual(0))) != 0)
                    {
                        ho_OrgRectangles.Dispose();
                        ho_RegionDilation.Dispose();
                        ho_RegionUnion.Dispose();
                        ho_ConnectedRegions.Dispose();
                        ho_RectangleDilationSelected.Dispose();
                        ho_RectangleSelected.Dispose();

                        return;
                    }
                    if ((int)((new HTuple((new HTuple(hv_numOfRectangles.TupleNotEqual(new HTuple(hv_Row2OfRectangles_COPY_INP_TMP.TupleLength()
                        )))).TupleOr(new HTuple(hv_numOfRectangles.TupleNotEqual(new HTuple(hv_Column1OfRectangles.TupleLength()
                        )))))).TupleOr(new HTuple(hv_numOfRectangles.TupleNotEqual(new HTuple(hv_Column2OfRectangles_COPY_INP_TMP.TupleLength()
                        ))))) != 0)
                    {
                        ho_OrgRectangles.Dispose();
                        ho_RegionDilation.Dispose();
                        ho_RegionUnion.Dispose();
                        ho_ConnectedRegions.Dispose();
                        ho_RectangleDilationSelected.Dispose();
                        ho_RectangleSelected.Dispose();

                        return;
                    }
                    HOperatorSet.TupleMax2(hv_Row1OfRectangles, hv_Row2OfRectangles_COPY_INP_TMP,
                        out hv_Row2OfRectangles_COPY_INP_TMP);
                    HOperatorSet.TupleMax2(hv_Column1OfRectangles, hv_Column2OfRectangles_COPY_INP_TMP,
                        out hv_Column2OfRectangles_COPY_INP_TMP);
                    ho_OrgRectangles.Dispose();
                    HOperatorSet.GenRectangle1(out ho_OrgRectangles, hv_Row1OfRectangles, hv_Column1OfRectangles,
                        hv_Row2OfRectangles_COPY_INP_TMP, hv_Column2OfRectangles_COPY_INP_TMP);
                    HOperatorSet.TupleMax2(1.5, hv_RadiusOfDilation_COPY_INP_TMP, out hv_RadiusOfDilation_COPY_INP_TMP);
                    ho_RegionDilation.Dispose();
                    HOperatorSet.DilationCircle(ho_OrgRectangles, out ho_RegionDilation, hv_RadiusOfDilation_COPY_INP_TMP);
                    ho_RegionUnion.Dispose();
                    HOperatorSet.Union1(ho_RegionDilation, out ho_RegionUnion);
                    ho_ConnectedRegions.Dispose();
                    HOperatorSet.Connection(ho_RegionUnion, out ho_ConnectedRegions);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.SortRegion(ho_ConnectedRegions, out ExpTmpOutVar_0, "first_point",
                            "true", "row");
                        ho_ConnectedRegions.Dispose();
                        ho_ConnectedRegions = ExpTmpOutVar_0;
                    }
                    HOperatorSet.CountObj(ho_ConnectedRegions, out hv_Number);
                    HTuple end_val27 = hv_Number;
                    HTuple step_val27 = 1;
                    for (hv_IndexOfConnectedRegions = 1; hv_IndexOfConnectedRegions.Continue(end_val27, step_val27); hv_IndexOfConnectedRegions = hv_IndexOfConnectedRegions.TupleAdd(step_val27))
                    {
                        hv_NumberOfRectangles = 0;
                        ho_RectangleDilationSelected.Dispose();
                        HOperatorSet.SelectObj(ho_ConnectedRegions, out ho_RectangleDilationSelected,
                            hv_IndexOfConnectedRegions);
                        hv_SubDefectIndexList = new HTuple();
                        HTuple end_val31 = hv_numOfRectangles;
                        HTuple step_val31 = 1;
                        for (hv_IndexOfOrgRectangles = 1; hv_IndexOfOrgRectangles.Continue(end_val31, step_val31); hv_IndexOfOrgRectangles = hv_IndexOfOrgRectangles.TupleAdd(step_val31))
                        {
                            ho_RectangleSelected.Dispose();
                            HOperatorSet.SelectObj(ho_OrgRectangles, out ho_RectangleSelected, hv_IndexOfOrgRectangles);
                            HOperatorSet.TestSubsetRegion(ho_RectangleSelected, ho_RectangleDilationSelected,
                                out hv_IsSubset);
                            if ((int)(hv_IsSubset) != 0)
                            {
                                hv_IndexOfRectangles = hv_IndexOfRectangles.TupleConcat(hv_IndexOfOrgRectangles - 1);
                                hv_NumberOfRectangles = hv_NumberOfRectangles + 1;
                            }
                        }
                        hv_NumberListOfRectangles = hv_NumberListOfRectangles.TupleConcat(hv_NumberOfRectangles);
                    }
                    ho_ConnectedDilations.Dispose();
                    ho_ConnectedDilations = ho_ConnectedRegions.CopyObj(1, -1);
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    //do nothing.
                }
                //
                ho_OrgRectangles.Dispose();
                ho_RegionDilation.Dispose();
                ho_RegionUnion.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RectangleDilationSelected.Dispose();
                ho_RectangleSelected.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_OrgRectangles.Dispose();
                ho_RegionDilation.Dispose();
                ho_RegionUnion.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RectangleDilationSelected.Dispose();
                ho_RectangleSelected.Dispose();

                throw HDevExpDefaultException;
            }
        }

        static private void convert_single_region_to_points(HObject ho_singleRegion, out HTuple hv_posYs,
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
                dev_update_off();
                HOperatorSet.SetSystem("clip_region", "false");

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

        static private void dev_update_off()
        {

            // Initialize local and output iconic variables 
            //This procedure sets different update settings to 'off'.
            //This is useful to get the best performance and reduce overhead.
            //
            // dev_update_pc(...); only in hdevelop
            // dev_update_var(...); only in hdevelop
            // dev_update_window(...); only in hdevelop

            return;
        }
    }
}
