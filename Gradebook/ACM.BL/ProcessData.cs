﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public delegate int BizRulesDelegate(int x, int y);

    public class ProcessData
    {
        public void Process (int x, int y, BizRulesDelegate del)
        {
            var result = del(x, y);
            Console.WriteLine(result);
        }

        public void ProcessAction(int x, int y, Action<int, int> action)
        {
            action(x, y);
            Console.WriteLine("Action has been processed");
        }
    }
}
