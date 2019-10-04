using System;
using TestNinja.Fundamentals;
using Fundamentals;
using System.Collections.Generic;
using ACM.BL;
using ACM.BL.Entities;
using Common;
using System.Windows;

namespace GradeBook
{


    class Program
    {
        static string filepath = @"C:\Users\risottj\Documents\UnitTesting\Gradebook\Pop by Largest Final.csv";
        static CountryMaker countryMaker = new CountryMaker(filepath);
        static PrintHelper printHelper = new PrintHelper();

        static void Main(string[] args)
        {
            //mainChoice();
            //var window = new PedometerWin();
            //window.PedometerWin();
        }



        static void mainChoice()
        {
            string branch;

            Console.WriteLine("Type 1 to Create a player char; 2 to Run the Grade Program, 3 to run Tick Tack Toe, " +
                "4 to run StockAnalysis");
            branch = Console.ReadLine();

            if (branch == "1")
            {
                CreatePlayerCharacters();
            }
            else if (branch == "2")
            {
                RunGradeBookProgram();
            }
            else if (branch == "3")
            {
                RunTickTackToeProgram();
            }
            else if (branch == "4")
            {
                StockAnalyzer.mainMethod();
            }
            else if (branch == "5")
            {
                DateHelper.mainMethod();
            }
            else if (branch == "6")
            {
                PolygonProgram.Main();
            }
            else if (branch == "7")
            {
                UsingLinq.mainMethod();
            }
            else if (branch == "8")
            {
                Person.Main();
            }

            else if (branch == "9")
            {
                Employee.Main();
            }
            else if (branch == "a")
            {
                Movie.Main();
            }
            else if (branch == "b")
            {
                //Car.Main();
            }
        }

        private static void CreatePlayerCharacters()
        {
            PlayerCharacter sarah = new PlayerCharacter(SpecialDefence.DiamondSkin)
            {
                FirstName = "Sarah"
            };
            PlayerCharacter amrit = new PlayerCharacter(SpecialDefence.IronBones)
            {
                FirstName = "Amrit"
            };

            PlayerCharacter gentry = new PlayerCharacter(SpecialDefence.Null)
            {
                FirstName = "gentry"
            };

            sarah.Hit(10);
            amrit.Hit(10);
            gentry.Hit(10);

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
            string[] daysofweek = DateHelper.GetDaysOfWeek();
            printHelper.PrintAllOf(daysofweek, x);
        }
    }
}
