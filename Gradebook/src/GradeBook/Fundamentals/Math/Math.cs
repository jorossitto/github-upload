using System.Collections.Generic;

namespace Test.Fundamentals
{
    public class Math
    {
        public int Add(int a, int b)
        { 
            return a + b;
        }
        
        public static int Max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        public int Min(int a, int b)
        {
            return (a < b) ? a : b;
        }

        public IEnumerable<int> GetOddNumbers(int limit)
        {
            for (var i = 0; i <= limit; i++)
                if (i % 2 != 0)
                    yield return i; 
        }

        public static int RoundToNearest(int exact, int accuracy)
        {
            int adjusted = exact + accuracy / 2;
            return adjusted - adjusted % accuracy;
        }
        public static long GetHighestPowerOfTen(int x)
        {
            long result = 1;
            while (x > 0)
            {
                x /= 10;
                result *= 10;
            }
            return result;
        }
    }
}