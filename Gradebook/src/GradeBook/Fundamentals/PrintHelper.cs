using System;

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
            Math math = new Math();
            int loopSize = math.Min(amount, collection.Length);
            for (int i = 0; i < loopSize; i++)
            {
                string item = (string)collection[i];
                Console.WriteLine(item);
            }
        }
    }

}