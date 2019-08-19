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
        public static void GenDefectRegions(Bitmap bitmap, List<Rectangle> rectangles, double radiusOfExpansion, out List<double> xldXs, out List<double> xldYs, out List<int> xldPointCount, out List<List<int>> defectIndexList)
        {
            xldXs = new List<double>();
            xldYs = new List<double>();
            xldPointCount = new List<int>();
            defectIndexList = new List<List<int>>();
            if (rectangles == null || rectangles.Count<=0)
            {
                return;
            }

            HObject ho_image;
            HTuple hv_Row1OfRectangles = new HTuple();
            HTuple hv_Row2OfRectangles = new HTuple();
            HTuple hv_Column1OfRectangles = new HTuple();
            HTuple hv_Column2OfRectangles = new HTuple();
            HTuple hv_RadiusOfExpansion = new HTuple();
            HTuple hv_XldXs = new HTuple();
            HTuple hv_XldYs = new HTuple();
            HTuple hv_XldPointCount = new HTuple();
            HTuple hv_DefectIndexList = new HTuple();
            HTuple hv_SubDefectNumberList = new HTuple();

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
            hv_RadiusOfExpansion = radiusOfExpansion;
            gen_defect_regions(ho_image, hv_Row1OfRectangles, hv_Row2OfRectangles, hv_Column1OfRectangles, hv_Column2OfRectangles, hv_RadiusOfExpansion, out hv_XldXs, out hv_XldYs, out hv_XldPointCount, out hv_DefectIndexList, out hv_SubDefectNumberList);

            List<int> DefectIndexList = new List<int>();
            List<int> SubDefectNumberList = new List<int>();
            List<int> subDefectIndexList = new List<int>();
            if (hv_DefectIndexList.TupleLength() > 1)
            {
                DefectIndexList.AddRange(hv_DefectIndexList.ToIArr());
            }
            else
            {
                DefectIndexList.Add(hv_DefectIndexList.I);
            }
            if (hv_SubDefectNumberList.TupleLength() > 1)
            {
                SubDefectNumberList.AddRange(hv_SubDefectNumberList.ToIArr());
            }
            else
            {
                SubDefectNumberList.Add(hv_SubDefectNumberList.I);
            }
            
            foreach (var subDefectNumber in SubDefectNumberList)
            {
                subDefectIndexList.Clear();
                for (var index = 0; index < subDefectNumber; ++index)
                {
                    subDefectIndexList.Add(DefectIndexList[0]);
                    DefectIndexList.RemoveAt(0);
                }
                defectIndexList.Add(subDefectIndexList);
            }

            if (hv_XldYs.TupleLength() > 1)
            {
                xldYs.AddRange(hv_XldYs.ToDArr());
            }
            else if (hv_XldYs.TupleLength() == 1)
            {
                xldYs.Add(hv_XldYs.D);
            }
            else
            {
                xldYs.Add(0.0);
            }

            if (hv_XldXs.TupleLength() > 1)
            {
                xldXs.AddRange(hv_XldXs.ToDArr());
            }
            else if (hv_XldXs.TupleLength() == 1)
            {
                xldXs.Add(hv_XldXs.D);
            }
            else
            {
                xldXs.Add(0.0);
            }

            if (hv_XldPointCount.TupleLength() > 1)
            {
                xldPointCount.AddRange(hv_XldPointCount.ToIArr());
            }
            else if (hv_XldPointCount.TupleLength() == 1)
            {
                xldPointCount.Add(hv_XldPointCount.I);
            }
            else
            {
                xldPointCount.Add(0);
            }

            return;
        }

        static private void gen_defect_regions(HObject ho_image, HTuple hv_Row1OfRectangles, HTuple hv_Row2OfRectangles,
      HTuple hv_Column1OfRectangles, HTuple hv_Column2OfRectangles, HTuple hv_RadiusOfExpansion,
      out HTuple hv_XldXs, out HTuple hv_XldYs, out HTuple hv_XldPointCount, out HTuple hv_DefectIndexList,
      out HTuple hv_SubDefectNumberList)
        {




            // Local iconic variables 

            HObject ho_OrgRectangles = null, ho_RegionDilation = null;
            HObject ho_RectanglesDilation = null, ho_RegionUnion = null;
            HObject ho_ConnectedRegions = null, ho_RectangleDilationSelected = null;
            HObject ho_RectangleSelected = null, ho_Contours = null;

            // Local control variables 

            HTuple hv_numOfRectangles = new HTuple();
            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Row2 = new HTuple(), hv_Column2 = new HTuple();
            HTuple hv_Number = new HTuple(), hv_indexOfConnectedRegions = new HTuple();
            HTuple hv_SubDefectIndexList = new HTuple(), hv_indexOfOrgRectangles = new HTuple();
            HTuple hv_IsSubset = new HTuple(), hv_Rows = new HTuple();
            HTuple hv_Columns = new HTuple(), hv_Exception = null;
            HTuple hv_Column2OfRectangles_COPY_INP_TMP = hv_Column2OfRectangles.Clone();
            HTuple hv_RadiusOfExpansion_COPY_INP_TMP = hv_RadiusOfExpansion.Clone();
            HTuple hv_Row2OfRectangles_COPY_INP_TMP = hv_Row2OfRectangles.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_OrgRectangles);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_RectanglesDilation);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RectangleDilationSelected);
            HOperatorSet.GenEmptyObj(out ho_RectangleSelected);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            try
            {
                //dev_update_off();
                HOperatorSet.SetSystem("clip_region", "true");

                hv_XldXs = new HTuple();
                hv_XldYs = new HTuple();
                hv_XldPointCount = new HTuple();
                hv_DefectIndexList = new HTuple();
                hv_SubDefectNumberList = new HTuple();
                try
                {
                    hv_numOfRectangles = 0;
                    HOperatorSet.TupleLength(hv_Row1OfRectangles, out hv_numOfRectangles);
                    if ((int)(new HTuple(hv_numOfRectangles.TupleLessEqual(0))) != 0)
                    {
                        ho_OrgRectangles.Dispose();
                        ho_RegionDilation.Dispose();
                        ho_RectanglesDilation.Dispose();
                        ho_RegionUnion.Dispose();
                        ho_ConnectedRegions.Dispose();
                        ho_RectangleDilationSelected.Dispose();
                        ho_RectangleSelected.Dispose();
                        ho_Contours.Dispose();

                        return;
                    }

                    if ((int)((new HTuple((new HTuple(hv_numOfRectangles.TupleNotEqual(new HTuple(hv_Row2OfRectangles_COPY_INP_TMP.TupleLength()
                        )))).TupleOr(new HTuple(hv_numOfRectangles.TupleNotEqual(new HTuple(hv_Column1OfRectangles.TupleLength()
                        )))))).TupleOr(new HTuple(hv_numOfRectangles.TupleNotEqual(new HTuple(hv_Column2OfRectangles_COPY_INP_TMP.TupleLength()
                        ))))) != 0)
                    {
                        ho_OrgRectangles.Dispose();
                        ho_RegionDilation.Dispose();
                        ho_RectanglesDilation.Dispose();
                        ho_RegionUnion.Dispose();
                        ho_ConnectedRegions.Dispose();
                        ho_RectangleDilationSelected.Dispose();
                        ho_RectangleSelected.Dispose();
                        ho_Contours.Dispose();

                        return;
                    }
                    HOperatorSet.TupleMax2(hv_Row1OfRectangles, hv_Row2OfRectangles_COPY_INP_TMP,
                        out hv_Row2OfRectangles_COPY_INP_TMP);
                    HOperatorSet.TupleMax2(hv_Column1OfRectangles, hv_Column2OfRectangles_COPY_INP_TMP,
                        out hv_Column2OfRectangles_COPY_INP_TMP);
                    ho_OrgRectangles.Dispose();
                    HOperatorSet.GenRectangle1(out ho_OrgRectangles, hv_Row1OfRectangles, hv_Column1OfRectangles,
                        hv_Row2OfRectangles_COPY_INP_TMP, hv_Column2OfRectangles_COPY_INP_TMP);
                    HOperatorSet.TupleMax2(1.5, hv_RadiusOfExpansion_COPY_INP_TMP, out hv_RadiusOfExpansion_COPY_INP_TMP);
                    ho_RegionDilation.Dispose();
                    HOperatorSet.DilationCircle(ho_OrgRectangles, out ho_RegionDilation, hv_RadiusOfExpansion_COPY_INP_TMP);
                    HOperatorSet.SmallestRectangle1(ho_RegionDilation, out hv_Row1, out hv_Column1,
                        out hv_Row2, out hv_Column2);
                    ho_RectanglesDilation.Dispose();
                    HOperatorSet.GenRectangle1(out ho_RectanglesDilation, hv_Row1, hv_Column1,
                        hv_Row2, hv_Column2);
                    ho_RegionUnion.Dispose();
                    HOperatorSet.Union1(ho_RectanglesDilation, out ho_RegionUnion);
                    ho_ConnectedRegions.Dispose();
                    HOperatorSet.Connection(ho_RegionUnion, out ho_ConnectedRegions);
                    HOperatorSet.CountObj(ho_ConnectedRegions, out hv_Number);
                    HTuple end_val28 = hv_Number;
                    HTuple step_val28 = 1;
                    for (hv_indexOfConnectedRegions = 1; hv_indexOfConnectedRegions.Continue(end_val28, step_val28); hv_indexOfConnectedRegions = hv_indexOfConnectedRegions.TupleAdd(step_val28))
                    {
                        ho_RectangleDilationSelected.Dispose();
                        HOperatorSet.SelectObj(ho_ConnectedRegions, out ho_RectangleDilationSelected,
                            hv_indexOfConnectedRegions);
                        hv_SubDefectIndexList = new HTuple();
                        HTuple end_val31 = hv_numOfRectangles;
                        HTuple step_val31 = 1;
                        for (hv_indexOfOrgRectangles = 1; hv_indexOfOrgRectangles.Continue(end_val31, step_val31); hv_indexOfOrgRectangles = hv_indexOfOrgRectangles.TupleAdd(step_val31))
                        {
                            ho_RectangleSelected.Dispose();
                            HOperatorSet.SelectObj(ho_OrgRectangles, out ho_RectangleSelected, hv_indexOfOrgRectangles);
                            HOperatorSet.TestSubsetRegion(ho_RectangleSelected, ho_RectangleDilationSelected,
                                out hv_IsSubset);
                            if ((int)(hv_IsSubset) != 0)
                            {
                                hv_SubDefectIndexList = hv_SubDefectIndexList.TupleConcat(hv_indexOfOrgRectangles);
                            }
                        }
                        ho_Contours.Dispose();
                        HOperatorSet.GenContourRegionXld(ho_RectangleDilationSelected, out ho_Contours,
                            "border");
                        HOperatorSet.GetContourXld(ho_Contours, out hv_Rows, out hv_Columns);
                        hv_XldXs = hv_XldXs.TupleConcat(hv_Columns);
                        hv_XldYs = hv_XldYs.TupleConcat(hv_Rows);
                        hv_XldPointCount = hv_XldPointCount.TupleConcat(new HTuple(hv_Columns.TupleLength()
                            ));
                        hv_SubDefectNumberList = hv_SubDefectNumberList.TupleConcat(new HTuple(hv_SubDefectIndexList.TupleLength()
                            ));
                        hv_DefectIndexList = hv_DefectIndexList.TupleConcat(hv_SubDefectIndexList);
                    }
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    //do nothing.
                }

                ho_OrgRectangles.Dispose();
                ho_RegionDilation.Dispose();
                ho_RectanglesDilation.Dispose();
                ho_RegionUnion.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RectangleDilationSelected.Dispose();
                ho_RectangleSelected.Dispose();
                ho_Contours.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_OrgRectangles.Dispose();
                ho_RegionDilation.Dispose();
                ho_RectanglesDilation.Dispose();
                ho_RegionUnion.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RectangleDilationSelected.Dispose();
                ho_RectangleSelected.Dispose();
                ho_Contours.Dispose();

                throw HDevExpDefaultException;
            }
        }

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
            if (hv_IndexOfRectangles.TupleLength() > 0)
            {
                defectInfoIndexList.AddRange(hv_IndexOfRectangles.ToIArr());
            }
            else
            {
                defectInfoIndexList.Add(0);
            }

            if (hv_NumberListOfRectangles.TupleLength() > 0)
            {
                defectInfoNumberList.AddRange(hv_NumberListOfRectangles.ToIArr());
            }
            else
            {
                defectInfoNumberList.Add(0);
            }

            HOperatorSet.CountObj(ho_ConnectedDilations, out var hv_NumberOfDilations);
            for (var index = 1; index <= hv_NumberOfDilations; ++index)
            {
                SingleDefectRegion defectRegion = new SingleDefectRegion();
                HOperatorSet.SelectObj(ho_ConnectedDilations, out var ho_Dilation, index);
                convert_single_region_to_points(ho_Dilation, out var hv_posYs, out var hv_posXs, out var hv_pointsNum);
                for (var cnt = 0; cnt < defectInfoNumberList[index-1]; ++cnt)
                {
                    defectRegion.DefectInfoIndexList.Add(defectInfoIndexList[0]);
                    defectInfoIndexList.RemoveAt(0);
                }
                if (hv_posYs.TupleLength() > 0)
                {
                    defectRegion.XldYs.AddRange(hv_posYs.ToDArr());
                }
                else
                {
                    defectRegion.XldYs.Add(0.0);
                }

                if (hv_posXs.TupleLength() > 0)
                {
                    defectRegion.XldXs.AddRange(hv_posXs.ToDArr());
                }
                else
                {
                    defectRegion.XldXs.Add(0.0);
                }

                if (hv_pointsNum.TupleLength() > 0)
                {
                    defectRegion.XldPointCount.AddRange(hv_pointsNum.ToIArr());
                }
                else
                {
                    defectRegion.XldPointCount.Add(0);
                }

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
            HObject ho_RectanglesDilation = null, ho_RegionUnion = null;
            HObject ho_ConnectedRegions = null, ho_RectangleDilationSelected = null;
            HObject ho_RectangleSelected = null;

            // Local control variables 

            HTuple hv_numOfRectangles = new HTuple();
            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Row2 = new HTuple(), hv_Column2 = new HTuple();
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
            HOperatorSet.GenEmptyObj(out ho_RectanglesDilation);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RectangleDilationSelected);
            HOperatorSet.GenEmptyObj(out ho_RectangleSelected);
            try
            {
                dev_update_off();
                HOperatorSet.SetSystem("clip_region", "true");

                ho_ConnectedDilations.Dispose();
                HOperatorSet.GenEmptyObj(out ho_ConnectedDilations);

                hv_IndexOfRectangles = new HTuple();
                hv_NumberListOfRectangles = new HTuple();

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
                        ho_RectanglesDilation.Dispose();
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
                        ho_RectanglesDilation.Dispose();
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
                    //HOperatorSet.SmallestRectangle1(ho_RegionDilation, out hv_Row1, out hv_Column1,
                    //    out hv_Row2, out hv_Column2);
                    //ho_RectanglesDilation.Dispose();
                    //HOperatorSet.GenRectangle1(out ho_RectanglesDilation, hv_Row1, hv_Column1,
                    //    hv_Row2, hv_Column2);
                    ho_RegionUnion.Dispose();
                    //HOperatorSet.Union1(ho_RectanglesDilation, out ho_RegionUnion);
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
                    HTuple end_val29 = hv_Number;
                    HTuple step_val29 = 1;
                    for (hv_IndexOfConnectedRegions = 1; hv_IndexOfConnectedRegions.Continue(end_val29, step_val29); hv_IndexOfConnectedRegions = hv_IndexOfConnectedRegions.TupleAdd(step_val29))
                    {
                        hv_NumberOfRectangles = 0;
                        ho_RectangleDilationSelected.Dispose();
                        HOperatorSet.SelectObj(ho_ConnectedRegions, out ho_RectangleDilationSelected,
                            hv_IndexOfConnectedRegions);
                        hv_SubDefectIndexList = new HTuple();
                        HTuple end_val33 = hv_numOfRectangles;
                        HTuple step_val33 = 1;
                        for (hv_IndexOfOrgRectangles = 1; hv_IndexOfOrgRectangles.Continue(end_val33, step_val33); hv_IndexOfOrgRectangles = hv_IndexOfOrgRectangles.TupleAdd(step_val33))
                        {
                            ho_RectangleSelected.Dispose();
                            HOperatorSet.SelectObj(ho_OrgRectangles, out ho_RectangleSelected, hv_IndexOfOrgRectangles);
                            HOperatorSet.TestSubsetRegion(ho_RectangleSelected, ho_RectangleDilationSelected,
                                out hv_IsSubset);
                            if ((int)(hv_IsSubset) != 0)
                            {
                                hv_IndexOfRectangles = hv_IndexOfRectangles.TupleConcat(hv_IndexOfOrgRectangles);
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

                ho_OrgRectangles.Dispose();
                ho_RegionDilation.Dispose();
                ho_RectanglesDilation.Dispose();
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
                ho_RectanglesDilation.Dispose();
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
