using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fundamentals
{
    public class PrintHelper
    {
        public void PrintAllOf(object[] collection)
        {
            foreach (string item in collection)
            {
                Console.WriteLine(item);
            }
        }

        public void PrintAllOf(object[] collection, int amount)
        {
            int loopSize = Math.Min(amount, collection.Length);
            for (int i = 0; i < loopSize; i++)
            {
                string item = (string)collection[i];
                Console.WriteLine(item);
            }
        }
    }

}