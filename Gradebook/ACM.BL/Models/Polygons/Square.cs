using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class Square : ConcreteRegularPolygon
    {
        public Square(int length) : base(4, length)
        {

        }

        public override double GetArea()
        {
            return SideLength * SideLength;
        }

    }
}
