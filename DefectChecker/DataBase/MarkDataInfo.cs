using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectChecker.DataBase
{
    public class MarkDataInfo
    {
        public string ProductName { get; set; }
        public string BatchName { get; set; }
        public string BoardName { get; set; }
        public string SideName { get; set; }
        public string ShotName { get; set; }
        public string DefectName { get; set; }
        public Dictionary<int, MarkRegionInfo> MarkRegionInfos { get; set; }

        public MarkDataInfo(string productName, string batchName, string boardName, string sideName,
            string shotName, string defectName)
        {
            ProductName = productName;
            BatchName = batchName;
            BoardName = boardName;
            SideName = sideName;
            ShotName = shotName;
            DefectName = defectName;
            MarkRegionInfos = new Dictionary<int, MarkRegionInfo>();
        }

        public MarkDataInfo(MarkDataInfo markDataInfo)
        {
            ProductName = markDataInfo.ProductName;
            BatchName = markDataInfo.BatchName;
            BoardName = markDataInfo.BoardName;
            SideName = markDataInfo.SideName;
            ShotName = markDataInfo.ShotName;
            DefectName = markDataInfo.DefectName;
            MarkRegionInfos = new Dictionary<int, MarkRegionInfo>();
            foreach (var regionMarkType in markDataInfo.MarkRegionInfos)
            {
                AddMarks(regionMarkType.Key, regionMarkType.Value);
            }
        }

        public void ClearMarks()
        {
            if (MarkRegionInfos != null)
            {
                MarkRegionInfos.Clear();
            }
            MarkRegionInfos = new Dictionary<int, MarkRegionInfo>();
        }

        public void AddMarks(int regionIndex, MarkRegionInfo markRegionInfo)
        {
            if (MarkRegionInfos == null)
            {
                MarkRegionInfos = new Dictionary<int, MarkRegionInfo>();
            }

            if (MarkRegionInfos.ContainsKey(regionIndex))
            {
                MarkRegionInfos[regionIndex] = markRegionInfo;
            }
            else
            {
                MarkRegionInfos.Add(regionIndex, markRegionInfo);
            }
        }

        public string MarksToString()
        {
            if (MarkRegionInfos == null)
            {
                MarkRegionInfos = new Dictionary<int, MarkRegionInfo>();
            }

            string marksString = "";
            foreach (var regionMarkType in MarkRegionInfos)
            {
                int regionIndex = regionMarkType.Key;
                MarkRegionInfo markType = regionMarkType.Value;
                marksString += regionIndex.ToString() + ":" + markType.GenString()+"#";
            }

            return marksString;
        }

        public void SetMarksByString(string markStr)
        {
            ClearMarks();
            var array = markStr.Split('#');
            if (array==null)
            {
                return;
            }

            for (int i = 0; i < array.Length-1; i++)
            {
                var s = array[i];
                var mark = s.Split(':');
                if (mark!=null && mark.Length==2)
                {
                    int regionIndex;
                    MarkRegionInfo markType;
                    if (int.TryParse(mark[0], out regionIndex) &&
                        MarkRegionInfo.TryParse(mark[1], out markType))
                    {
                        AddMarks(regionIndex, markType);
                    }
                }
            }
        }
    }
}
