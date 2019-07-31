using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefectChecker.Common;
using DefectChecker.DefectDataStructure;

namespace DefectChecker.DeviceModule.MachVision
{
    class MachVisionFile
    {
        private string _fileName;

        public MachVisionFile(string filePath, string shotName)
        {
            if (shotName == "Shot0")
            {
                _fileName = filePath + "\\" + "ResultShot_0.ini";
            }
            else
            {
                _fileName = filePath + "\\" + "ResultShot_1.ini";
            }
        }

        private bool ReadCodeNum(string defectName, out int codeNum)
        {
            codeNum = 0;

            string str;
            IniHelper iniHelper = new IniHelper();
            if (iniHelper.ReadValue(defectName, "DefectCode", _fileName, out str)>0)
            {
                var array = str.Split(',');
                if (array.Length > 0 && int.TryParse(array[0], out codeNum))
                {
                    return true;
                }
            }

            return false;
        }

        private bool ReadRoiInTemplate(string defectName, out RectShape roi)
        {
            double topLeftX, topLeftY, width, height;
            string str;
            IniHelper iniHelper = new IniHelper();
            if (iniHelper.ReadValue(defectName, "DefectRoiGold ", _fileName, out str) > 0)
            {
                var array = str.Split(',');
                if (array.Length == 4 &&
                    double.TryParse(array[0], out topLeftX) &&
                    double.TryParse(array[1], out topLeftY) &&
                    double.TryParse(array[2], out width) &&
                    double.TryParse(array[3], out height))
                {
                    roi = new RectShape(topLeftX, topLeftY, topLeftX + width - 1, topLeftY + height - 1);
                    return true;
                }
            }

            roi = new RectShape(0, 0, 0, 0);
            return false;
        }

        private bool ReadSubDefects(string defectName, out List<ShapeBase> subDefects)
        {
            subDefects = new List<ShapeBase>();
            IniHelper iniHelper = new IniHelper();
            string str;
            if (iniHelper.ReadValue(defectName, "NumSubDefect", _fileName, out str)<=0)
            {
                return false;
            }
            int subDefectNum;
            if (!int.TryParse(str, out subDefectNum))
            {
                return false;
            }

            for (int i = 0; i < subDefectNum; i++)
            {
                string subDefectName = string.Format("SD_{0:0000}", i);
                if (iniHelper.ReadValue(defectName, subDefectName, _fileName, out str)<=0)
                {
                    return false;
                }

                var array = str.Split('|');
                foreach (var subStr in array)
                {
                    if (subStr==null || subStr.Length==0)
                    {
                        continue;
                    }
                    ShapeBase subDefect;
                    if (DecodeSubDefect(subStr, out subDefect))
                    {
                        subDefects.Add(subDefect);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool DecodeSubDefect(string str, out ShapeBase subDefect)
        {
            var array = str.Split('#');
            string pointStr;
            if (array!=null && array.Length>0)
            {
                pointStr = array[array.Length-1];
            }
            else
            {
                subDefect = new RectShape(0, 0, 0, 0);
                return false;
            }
            var point = pointStr.Split(',');
            int len = point.Length;
            
            double topLeftX, topLeftY, bottomRightX, bottomRightY;
            if (len== 10 &&
                double.TryParse(point[0], out topLeftX) &&
                double.TryParse(point[1], out topLeftY) &&
                double.TryParse(point[4], out bottomRightX) &&
                double.TryParse(point[5], out bottomRightY))
            {
                subDefect = new RectShape(topLeftX, topLeftY, bottomRightX, bottomRightY);
                return true;
            }

            subDefect = new RectShape(0, 0, 0, 0);
            return false;
        }

        public bool ReadDefectInfo(string defectName, out DefectInfo defectInfo)
        {
            defectInfo = new DefectInfo();

            int codeNum;
            if (!ReadCodeNum(defectName, out codeNum))
            {
                return false;
            }
            else
            {
                defectInfo.CodeNum = codeNum;
            }

            RectShape roi;
            if (!ReadRoiInTemplate(defectName, out roi))
            {
                return false;
            }
            else
            {
                defectInfo.RoiInTemplate = roi;
            }

            List<ShapeBase> subDefects;
            if (!ReadSubDefects(defectName, out subDefects))
            {
                return false;
            }
            else
            {
                defectInfo.SubDefectList = subDefects;
            }

            return true;
        }

        public bool ReadDefectInfoList(List<string> defectList, out List<DefectInfo> defectInfoList)
        {
            defectInfoList = new List<DefectInfo>();
            return true;
        }

    }
}
