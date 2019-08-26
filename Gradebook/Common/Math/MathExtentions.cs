using System;
using System.Collections.Generic;

namespace Common
{
    [System.Runtime.InteropServices.Guid("8869D315-BA35-4327-AEC0-FE4457B58364")]
    public class MathExtentions
    {
        public static int Add(int a, int b)
        { 
            return a + b;
        }
        
        public static int Max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        public static double Max(double a, double b)
        {
            return (a > b) ? a : b;
        }

        public static float Min(float a, float b)
        {
            return (a < b) ? a : b;
        }

        public static int Min(int a, int b)
        {
            return (a < b) ? a : b;
        }

        public static double Min(double a, double b)
        {
            return (a < b) ? a : b;
        }

        public static IEnumerable<int> GetOddNumbers(int limit)
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
        public static float CalculatePercentOfGoalSteps(string goalSteps, string actualSteps)
        {
            float goalStepCount = 0;
            float actualStepCount = 0;

            if (string.IsNullOrWhiteSpace(goalSteps))
            {
                throw new ArgumentException("Goal must be entered", "goalSteps");
            }

            if (string.IsNullOrWhiteSpace(actualSteps))
            {
                throw new ArgumentException("Actual steps must be entered", "actualSteps");
            }


            if (!float.TryParse(goalSteps, out goalStepCount))
            {
                throw new ArgumentException("Goal must be numeric", "goalSteps");
            }

            if (!float.TryParse(actualSteps, out actualStepCount))
            {
                throw new ArgumentException("Actual steps be numeric", "actualSteps");
            }

            return CalculatePercentOfGoalSteps(goalStepCount, actualStepCount);
        }

        public static float CalculatePercentOfGoalSteps(float goalStepCount, float actualStepCount)
        {
            if (goalStepCount <= 0)
            {
                throw new ArgumentException("Goal must be greater than 0", "goalStepCount");
            }

            return (actualStepCount / goalStepCount) * 100;
        }
    }
}