using System;
using TestNinja.Fundamentals;
using Fundamentals;
using System.Collections.Generic;

namespace GradeBook
{

    class Program
    {
        static DateHelper date = new DateHelper();
        static string filepath = @"C:\Users\risottj\Documents\UnitTesting\Gradebook\Pop by Largest Final.csv";
        static CountryMaker countryMaker = new CountryMaker(filepath);
        static PrintHelper printHelper = new PrintHelper();

        static void Main(string[] args)
        {
            PlayerCharacter player = new PlayerCharacter();
            player.Name = "Sarah";
            player.DaysSinceLastLogin = 42;
            PlayerDisplayer.Write(player);
            Console.ReadLine();
        }

        private static void RunGradeBookProgram()
        {
            Mocking.BookProgram.SetupBook();
        }

        private static void RunTickTackToeProgram()
        {
            Game game = new Game();
            game.PlayGame();
            Console.WriteLine("Game over");
        }

        private static void RunPrintCountriesByRegionProgram()
        {
            countryMaker.PrintCountriesByRegion();
        }

        private static void RunPrintXDaysProgram(int x)
        {
            string[] daysofweek = date.GetDaysOfWeek();
            printHelper.PrintAllOf(daysofweek, x);
        }


    }
}
