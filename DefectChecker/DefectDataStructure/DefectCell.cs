using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectChecker.DefectDataStructure
{
    public class DefectCell
    {
        public Bitmap DefectImage { get; set; }
        public Bitmap TemplateImage { get; set; }
        public Bitmap GerberImage { get; set; }
        public DefectInfo Info { get; set; }

        public DefectCell()
        {
            DefectImage = null;
            TemplateImage = null;
            GerberImage = null;
            Info = new DefectInfo();
        }
    }
}
