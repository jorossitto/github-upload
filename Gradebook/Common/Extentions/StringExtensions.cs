using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class StringExtensions
    {
        /// <summary>
        /// Inserts spaces before each capital letter in a string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string InsertSpaces(this string source)
        {
            string result = string.Empty;
            if(!string.IsNullOrWhiteSpace(source))
            {
                foreach (char letter in source)
                {
                    if(char.IsUpper(letter))
                    {
                        result = result.Trim();
                        result += " ";
                    }
                    result += letter;
                }
            }
            result = result.Trim();
            return result;
        }

        public static double ToDouble(this string data)
        {
            //var type = data.GetType();
            double result = double.Parse(data);
            return result;
        }
    }
}
