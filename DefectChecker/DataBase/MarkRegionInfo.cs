using DefectChecker.DefectDataStructure;
using System;
using System.Collections.Generic;
using System.Drawing;
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

    public class MarkRegionInfo
    {
        private EMarkDataType MarkRegionType { get; set; }
        public Rectangle SmallestRect { get; set; }
        public Dictionary<int, DefectInfo> DefectInfos { get; set; }

        public MarkRegionInfo()
        {
            MarkRegionType = EMarkDataType.Undefined;
            SmallestRect = new Rectangle(0, 0, 0, 0);
            DefectInfos = new Dictionary<int, DefectInfo>();
        }

        public bool SetByDefectCell(DefectCell defectCell, int regionIndex, EMarkDataType markType)
        {
            if (defectCell == null || 
                defectCell.DefectRegions==null ||
                regionIndex<0 || 
                regionIndex>=defectCell.DefectRegions.Count ||
                defectCell.DefectRegions[regionIndex] == null ||
                defectCell.DefectRegions[regionIndex].DefectInfoIndexList == null ||
                defectCell.DefectRegions[regionIndex].DefectInfoIndexList.Count <= 0 ||
                defectCell.DefectInfos == null ||
                defectCell.DefectInfos.Count<=0)
            {
                return false;
            }

            SingleDefectRegion defectRegion = defectCell.DefectRegions[regionIndex];
            MarkRegionType = markType;
            SmallestRect = new Rectangle(defectRegion.SmallestRect.X, defectRegion.SmallestRect.Y,
                defectRegion.SmallestRect.Width, defectRegion.SmallestRect.Height);
            DefectInfos = new Dictionary<int, DefectInfo>();
            foreach (var index in defectRegion.DefectInfoIndexList)
            {
                if (index<0 || index>=defectCell.DefectInfos.Count)
                {
                    return false;
                }
                DefectInfo defectInfo = new DefectInfo();
                defectInfo.CodeNum = defectCell.DefectInfos[index].CodeNum;
                Rectangle rect = defectCell.DefectInfos[index].DefectRect;
                defectInfo.DefectRect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
                if (DefectInfos.ContainsKey(index))
                {
                    return false;
                }
                else
                {
                    DefectInfos.Add(index, defectInfo);
                }
            }

            return true;
        }

        public static bool TryParse(string str, out MarkRegionInfo markRegionInfo)
        {
            markRegionInfo = new MarkRegionInfo();
            var array = str.Split(';');
            if (array==null || array.Length<3)
            {
                return false;
            }
            
            var headArray = array[0].Split(',');
            EMarkDataType markType;
            int x, y, w, h;
            if (headArray==null || headArray.Length!=5||
                !EMarkDataType.TryParse(headArray[0], out markType) ||
                !int.TryParse(headArray[1], out x) ||
                !int.TryParse(headArray[2], out y) ||
                !int.TryParse(headArray[3], out w) ||
                !int.TryParse(headArray[4], out h))
            {
                return false;
            }

            markRegionInfo.MarkRegionType = markType;
            markRegionInfo.SmallestRect = new Rectangle(x, y, w, h);
            markRegionInfo.DefectInfos = new Dictionary<int, DefectInfo>();
            for (int i = 1; i < array.Length-1; i++)
            {
                var infoArray = array[i].Split(',');
                int index, codeNum;
                int infoX, infoY, infoW, infoH;
                if (infoArray == null || infoArray.Length != 6 ||
                    !int.TryParse(infoArray[0], out index) ||
                    !int.TryParse(infoArray[1], out codeNum) ||
                    !int.TryParse(infoArray[2], out infoX) ||
                    !int.TryParse(infoArray[3], out infoY) ||
                    !int.TryParse(infoArray[4], out infoW) ||
                    !int.TryParse(infoArray[5], out infoH))
                {
                    return false;
                }
                DefectInfo defectInfo = new DefectInfo();
                defectInfo.CodeNum = codeNum;
                defectInfo.DefectRect = new Rectangle(infoX, infoY, infoW, infoH);
                if (markRegionInfo.DefectInfos.ContainsKey(index))
                {
                    return false;
                }
                else
                {
                    markRegionInfo.DefectInfos.Add(index, defectInfo);
                }
            }

            return true;
        }

        public string GenString()
        {
            string str = MarkRegionType.ToString() + "," +
                         SmallestRect.X.ToString() + "," +
                         SmallestRect.Y.ToString() + "," +
                         SmallestRect.Width.ToString() + "," +
                         SmallestRect.Height.ToString() + ";";
            foreach (var defectInfo in DefectInfos)
            {
                int index = defectInfo.Key;
                DefectInfo info = defectInfo.Value;
                str += index.ToString() + "," +
                       info.CodeNum.ToString() + "," +
                       info.DefectRect.X.ToString() + "," +
                       info.DefectRect.Y.ToString() + "," +
                       info.DefectRect.Width.ToString() + "," +
                       info.DefectRect.Height.ToString() + ";";
            }

            return str;
        }
    }
}
