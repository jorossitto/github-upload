using System;
using TestNinja.Fundamentals;
using Fundamentals;
using System.Collections.Generic;

namespace GradeBook
{

    class Program
    {
        static PrintHelper printHelper = new PrintHelper();
        static DateHelper date = new DateHelper();
        static string filepath = @"C:\Users\risottj\Documents\UnitTesting\Gradebook\Pop by Largest Final.csv";
        static CsvReader reader = new CsvReader(filepath);

        static void Main(string[] args)
        {
            //printHelper.PrintAllCountries(filepath);
            printHelper.PrintCountries(filepath);
        }

        private static void PrintXDays(int x)
        {
            string[] daysofweek = date.GetDaysOfWeek();
            printHelper.PrintAllOf(daysofweek, x);
        }

        private static void SetupBook()
        {
            IBook book = new DiskBook("Scotts Grade Book");
            book.GradeAdded += OnGradeAdded;

            EnterGrades(book);

            var stats = book.GetStatistics();

            Console.WriteLine($"For the book named {book.Name:N1}");
            Console.WriteLine($"The lowest grade is {stats.Low:N1}");
            Console.WriteLine($"The highest grade is {stats.High:N1}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The letter grade is {stats.Letter:N1}");
        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter a grade or 'q' to quit");
                var input = Console.ReadLine();
                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }

            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }
    }
}
