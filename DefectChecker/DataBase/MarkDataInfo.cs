using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectChecker.DataBase
{
    public enum EMarkDataType
    {
        OK = 0,
        NG,
        Undefined
    };

    public class MarkDataInfo
    {
        public string ProductName { get; set; }
        public string BatchName { get; set; }
        public string BoardName { get; set; }
        public string SideName { get; set; }
        public string ShotName { get; set; }
        public string DefectName { get; set; }
        public Dictionary<int, EMarkDataType>  RegionMarkTypes { get; set; }

        public MarkDataInfo(string productName, string batchName, string boardName, string sideName,
            string shotName, string defectName)
        {
            ProductName = productName;
            BatchName = batchName;
            BoardName = boardName;
            SideName = sideName;
            ShotName = shotName;
            DefectName = defectName;
            RegionMarkTypes = new Dictionary<int, EMarkDataType>();
        }

        public MarkDataInfo(MarkDataInfo markDataInfo)
        {
            ProductName = markDataInfo.ProductName;
            BatchName = markDataInfo.BatchName;
            BoardName = markDataInfo.BoardName;
            SideName = markDataInfo.SideName;
            ShotName = markDataInfo.ShotName;
            DefectName = markDataInfo.DefectName;
            RegionMarkTypes = new Dictionary<int, EMarkDataType>();
            foreach (var regionMarkType in markDataInfo.RegionMarkTypes)
            {
                AddMarks(regionMarkType.Key, regionMarkType.Value);
            }
        }

        public void ClearMarks()
        {
            if (RegionMarkTypes!=null)
            {
                RegionMarkTypes.Clear();
            }
            RegionMarkTypes = new Dictionary<int, EMarkDataType>();
        }

        public void AddMarks(int regionIndex, EMarkDataType markType)
        {
            if (RegionMarkTypes==null)
            {
                RegionMarkTypes = new Dictionary<int, EMarkDataType>();
            }

            if (RegionMarkTypes.ContainsKey(regionIndex))
            {
                RegionMarkTypes[regionIndex] = markType;
            }
            else
            {
                RegionMarkTypes.Add(regionIndex, markType);
            }
        }

        public string MarksToString()
        {
            if (RegionMarkTypes == null)
            {
                RegionMarkTypes = new Dictionary<int, EMarkDataType>();
            }

            string marksString = "";
            foreach (var regionMarkType in RegionMarkTypes)
            {
                int regionIndex = regionMarkType.Key;
                EMarkDataType markType = regionMarkType.Value;
                marksString += regionIndex.ToString() + "," + markType.ToString()+"#";
            }

            return marksString;
        }

        public void SetMarksByString(string markStr)
        {
            ClearMarks();
            var array = markStr.Split('#');
            foreach (var s in array)
            {
                var mark = s.Split(',');
                if (mark!=null && mark.Length==2)
                {
                    int regionIndex;
                    EMarkDataType markType;
                    if (int.TryParse(mark[0], out regionIndex) &&
                        EMarkDataType.TryParse(mark[1], out markType))
                    {
                        AddMarks(regionIndex, markType);
                    }
                }
            }
        }
    }
}
