using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DefectChecker.DataBase;

namespace DefectChecker.DefectDataStructure
{
    public class SingleDefectRegion
    {
        public EMarkDataType MarkRegionType { get; set; } = EMarkDataType.Undefined;
        public Rectangle SmallestRect { get; set; } = new Rectangle(0, 0, 0, 0);
        public List<double> XldXs { get; set; } = new List<double>();
        public List<double> XldYs { get; set; } = new List<double>();
        public List<int> XldPointCount { get; set; } = new List<int>();
        public List<int> DefectInfoIndexList { get; set; } = new List<int>();

        public SingleDefectRegion()
        {
        }
    }
}
