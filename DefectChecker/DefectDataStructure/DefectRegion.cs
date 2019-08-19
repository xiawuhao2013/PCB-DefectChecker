using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DefectChecker.DefectDataStructure
{
    public class DefectRegion
    {
        public List<double> XldXs { get; set; }
        public List<double> XldYs { get; set; }
        public List<int> XldPointCount { get; set; }
        public List<List<int>> DefectIndexList { get; set; }

        public DefectRegion()
        {
            XldXs = new List<double>();
            XldYs = new List<double>();
            XldPointCount = new List<int>();
            DefectIndexList = new List<List<int>>();
        }
    }
}
