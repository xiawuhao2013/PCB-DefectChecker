using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectChecker.DefectDataStructure
{
    public class DefectInfo
    {
        public int CodeNum { get; set; }
        public RectShape RoiInTemplate { get; set; }
        public List<ShapeBase> SubDefectList { get; set; }
    }
}
