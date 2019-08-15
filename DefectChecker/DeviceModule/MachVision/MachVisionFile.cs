using System;
using System.Collections.Generic;
using System.Drawing;
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

        private bool ReadRoiInTemplate(string defectName, out Rectangle roi)
        {
            int topLeftX, topLeftY, width, height;
            string str;
            IniHelper iniHelper = new IniHelper();
            if (iniHelper.ReadValue(defectName, "DefectRoiGold ", _fileName, out str) > 0)
            {
                var array = str.Split(',');
                if (array.Length == 4 &&
                    int.TryParse(array[0], out topLeftX) &&
                    int.TryParse(array[1], out topLeftY) &&
                    int.TryParse(array[2], out width) &&
                    int.TryParse(array[3], out height))
                {
                    roi = new Rectangle(topLeftX, topLeftY, width - 1, height - 1);
                    return true;
                }
            }

            roi = new Rectangle(0, 0, 0, 0);
            return false;
        }

        private bool ReadSubDefects(string defectName, out List<Rectangle> subDefects)
        {
            subDefects = new List<Rectangle>();
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
                    Rectangle subDefect;
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

        private bool DecodeSubDefect(string str, out Rectangle subDefect)
        {
            var array = str.Split('#');
            string pointStr;
            if (array!=null && array.Length>0)
            {
                pointStr = array[array.Length-1];
            }
            else
            {
                subDefect = new Rectangle(0, 0, 0, 0);
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
                subDefect = Rectangle.FromLTRB(Convert.ToInt32(topLeftX), Convert.ToInt32(topLeftY), Convert.ToInt32(bottomRightX), Convert.ToInt32(bottomRightY));
                return true;
            }

            subDefect = new Rectangle(0, 0, 0, 0);
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

            Rectangle roi;
            if (!ReadRoiInTemplate(defectName, out roi))
            {
                return false;
            }
            else
            {
                defectInfo.RoiInTemplate = roi;
            }

            List<Rectangle> subDefects;
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
