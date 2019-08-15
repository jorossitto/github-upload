using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public static class StockAnalyzer
    {
        public static void mainMethod()
        {
            string branch;
            Console.WriteLine("Enter 1 to Read Stock Data, 2 to ConvertLocaltoSidney");

            branch = Console.ReadLine();
            if (branch == "1")
            {
                ReadStockData();
            }
            else if (branch == "2")
            {
                //ConvertLocalToSidney();
            }

            Console.ReadLine();
        }

        private static void ReadStockData()
        {
            var lines = File.ReadAllLines(@"StockData.csv");
            foreach (var line in lines.Skip(1))
            {
                var segments = line.Split(',');
                var tradeDate = DateTime.Parse(segments[1]);

                Console.WriteLine(tradeDate.ToLongDateString());

            }

            foreach (var line in lines.Skip(1))
            {
                var segments = line.Split(',');
                var tradeDate = DateTime.Parse(segments[1], CultureInfo.GetCultureInfo("en-GB"));

                Console.WriteLine(tradeDate.ToLongDateString());
                
            }
        }
    }
}
