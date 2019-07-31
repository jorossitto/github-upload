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
            //PlayerCharacter[] players = new PlayerCharacter[3]
            //{
            //    new PlayerCharacter {Name = "Sarah"},
            //    new PlayerCharacter(),
            //    null
            //};

            PlayerCharacter[] players = null;

            string p1 = players?[0]?.Name;
            string p2 = players?[1]?.Name;
            string p3 = players?[2]?.Name;

            Console.ReadLine();
            PlayerCharacter player = null;
            int days = player?.DaysSinceLastLogin ?? -1;


            //int days = player.DaysSinceLastLogin.Value;
            Console.WriteLine(days);
            //player.Name = "Sarah";
            //player.DaysSinceLastLogin = 42;
            //PlayerDisplayer.Write(player);

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
