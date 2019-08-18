using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectChecker.DefectDataStructure
{
    public class DefectInfo
    {
        public int CodeNum { get; set; }
        public List<Rectangle> RectList { get; set; }

        public DefectInfo()
        {
            CodeNum = -1;
            RectList = new List<Rectangle>();
        }
    }
}
