using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DefectChecker.DefectDataStructure
{
    public class SingleDefectRegion
    {
        public List<double> XldXs { get; set; } = new List<double>();
        public List<double> XldYs { get; set; } = new List<double>();
        public List<int> XldPointCount { get; set; } = new List<int>();
        public List<int> DefectInfoIndexList { get; set; } = new List<int>();

        public SingleDefectRegion()
        {
        }
    }
}
