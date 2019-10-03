using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class PolygonProgram
    {
        public static void Main()
        {
            var square = new Square(5);
            DisplayPolygon("Square", square);
            Console.Read();

            var triangle = new Triangle(5);
            DisplayPolygon("Triangle", triangle);

            var octagon = new Octagon(5);
            DisplayPolygon("Octagon", octagon);

            Console.Read();
        }

        private static void DisplayPolygon(string polygonType, dynamic polygon)
        {
            throw new NotImplementedException();
        }

    }
}
