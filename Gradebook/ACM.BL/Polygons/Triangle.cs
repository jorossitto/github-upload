using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class Triangle : AbstractRegularPolygon
    {
        public Triangle(int length) : base(3, length)
        {

        }

        public override double GetArea()
        {
            return SideLength * SideLength * Math.Sqrt(3)/4;
        }

    }
}
