using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double> grades = new List<double>() {12.7, 10.3, 6.11, 4.1};
            grades.Add(56.1);

            var result = 0.0;
            foreach (var grade in grades)
            {
                result += grade;
            }
            result = result/grades.Count;

            Console.WriteLine($"The average grade is {result} ");
            if(args.Length > 0)
            {
                Console.WriteLine($"Hello, {args[0]}!");
            }
            else
            {
                Console.WriteLine("Hello!");
            }
            
        }
    }
}
