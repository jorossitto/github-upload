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
        private void OrderXCountriesByName(List<Country> countries, int amount)
        {
            int count = 0;
            foreach (Country country in countries.OrderBy(x=>x.Name).Take(amount))
            {
                count++;
                CountryText(count, country);
            }
        }


        private static void CountryText(int count, Country country)
        {
            Console.WriteLine($"{count}: {PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: " +
                $"{country.Name}");
        }

        private List<Country> CleanCountries(List<Country> countries)
        {
            var filteredCountries = from country in countries
                                     where !country.Name.Contains(',')
                                     select country;

            List < Country > newList = new List<Country>();
            foreach (Country country in filteredCountries)
            {
                newList.Add(country);
            }
            return newList;
        }
        private List<Country> TakeXCountries(List<Country> countries, int amount)
        {
            var filteredCountries = countries.Take(amount);
            List<Country> newList = new List<Country>();

            foreach (Country country in filteredCountries)
            {
                newList.Add(country);
            }
            return newList;
        }
        private List<Country> OrderbyName(List<Country> countries)
        {
            List<Country> newList = new List<Country>();
            foreach (Country country in countries.OrderBy(x => x.Name))
            {
                newList.Add(country);
            }
            return newList;
        }

        public int AskForInput()
        {
            Console.Write("Enter number of countries to display> ");
            bool inputIsInt = int.TryParse(Console.ReadLine(), out int userInput);
            if (!inputIsInt || userInput <= 0)
            {
                Console.WriteLine("you must type in a +ve interger. Exiting");
                return 0;
            }
            return userInput;
        }
        public void PrintCountries(string filepath)
        {
            int userInput = AskForInput(); 
            if (userInput == 0)
            {
                return;
            }

            CsvReader reader = new CsvReader(filepath);
            List<Country> countries = reader.ReadAllCountriesAsList();
            int loopSize = Math.Min(userInput, countries.Count);

            countries = CleanCountries(countries);
            countries = TakeXCountries(countries, loopSize);

            int count = 0;
            foreach (Country country in countries)
            {
                count++;
                CountryText(count, country);
            }

            //bool assending = AssendingOrDecending();
            //PrintXCountriesAscendingOrDecending(countries, loopSize, assending);
        }

        private bool AssendingOrDecending()
        {
            Console.WriteLine("Type 1 if you would like the data to be assending");
            string userInput = Console.ReadLine();
            if(userInput != "1")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void AddNorwayAndFinland()
        {
            Country norway = new Country("Norway", "NOR", "Europe", 5_282_223);
            Country finland = new Country("Finland", "FIN", "Europe", 5_511_303);

            Dictionary<string, Country> countries = new Dictionary<string, Country>();
            countries.Add(norway.Code, norway);
            countries.Add(finland.Code, finland);

            Country selectedCountry = countries[norway.Code];
            Console.WriteLine(selectedCountry.Name);

            foreach (var item in countries.Values)
            {
                Console.WriteLine(item.Name);
            }
        }

        private static void PrintXCountriesAscendingOrDecending(List<Country> countries, int loopSize, bool assending)
        {
            if(assending)
            {
                for (int i = 0; i < countries.Count; i++)
                {
                    if (i > 0 && (i % loopSize == 0))
                    {
                        Console.WriteLine("Hit return to continue, anything else to quit");
                        if (Console.ReadLine() != "")
                        {
                            break;
                        }
                    }
                    Country country = countries[i];
                    Console.WriteLine($"{i + 1}: {PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: " +
                            $"{country.Name}");
                }
            }
            else
            {
                for (int i = countries.Count-1; i >= 0; i--)
                {
                    int displayIndex = countries.Count - 1 - i;
                    if (displayIndex > 0 && (displayIndex % loopSize == 0))
                    {
                        Console.WriteLine("Hit return to continue, anything else to quit");
                        if (Console.ReadLine() != "")
                        {
                            break;
                        }
                    }
                    Country country = countries[i];
                    Console.WriteLine($"{displayIndex + 1}: {PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: " +
                            $"{country.Name}");
                }
            }

        }

        public void PrintAllCountries(string filepath)
        {
            CsvReader reader = new CsvReader(filepath);
            List<Country> countries = reader.ReadAllCountriesAsList();

            //foreach (Country country in countries)
            for (int i = 0; i < countries.Count; i++)
            {
                Country country = countries[i];
                Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: " +
                    $"{country.Name}");
            }

            Console.WriteLine($"{countries.Count} countries");
        }

        public void PrintSelectedCountry(string filepath)
        {
            CsvReader reader = new CsvReader(filepath);
            Dictionary<string, Country> countries = reader.ReadAllCountries();

            Console.WriteLine("Which country code do you want to look up? ");
            string userInput = Console.ReadLine();
            CheckCountryIsValid(countries, userInput);
        }

        private static void CheckCountryIsValid(Dictionary<string, Country> countries, string userInput)
        {
            bool gotCountry = countries.TryGetValue(userInput, out Country country);
            if (!gotCountry)
            {
                Console.WriteLine($"Sorry, there is no country with code, {userInput}");
            }
            else
            {
                Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: " +
                    $"{country.Name}");
            }
        }

        
    }

}