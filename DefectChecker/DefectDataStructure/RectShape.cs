using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectChecker.DefectDataStructure
{
    public class RectShape : ShapeBase
    {
        private double _pointTopLeftX;
        private double _pointTopLeftY;
        private double _pointBottomRightX;
        private double _pointBottomRightY;

        public RectShape(double topLeftX, double topLeftY, double bottomRightX, double bottomRightY)
        {
            _pointTopLeftX = topLeftX;
            _pointTopLeftY = topLeftY;
            _pointBottomRightX = bottomRightX;
            _pointBottomRightY = bottomRightY;
        }
    }
}
