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
        public static void ReadStockData()
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

            Console.ReadLine();
        }


    }
}
