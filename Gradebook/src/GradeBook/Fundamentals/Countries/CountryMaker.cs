using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fundamentals
{
    public class CountryMaker
    {
        private string _csvFile;

        public CountryMaker(string csvFile)
        {
            _csvFile = csvFile;
        }

        public static Country ReadCountryFromCsvLine(string csvLine)
        {
            string[] parts = csvLine.Split(',');
            string name;
            string code;
            string region;
            string popText;
            switch (parts.Length)
            {
                case 4:
                    name = parts[0];
                    code = parts[1];
                    region = parts[2];
                    popText = parts[3];
                    break;
                case 5:
                    name = parts[0] + ", " + parts[1];
                    name = name.Replace("\"", null).Trim();
                    code = parts[2];
                    region = parts[3];
                    popText = parts[4];
                    break;
                default:
                    throw new Exception($"Can't parse country from csvLine: {csvLine}");
            }

            //TryParse leaves population = 0 if can't parse
            int.TryParse(popText, out int population);
            return new Country(name, code, region, population);
        }

        public List<Country> ReadAllCountriesAsList()
        {
            List<Country> country = new List<Country>();
            using (StreamReader sr = new StreamReader(_csvFile))
            {
                sr.ReadLine();
                string csvLine;
                while ((csvLine = sr.ReadLine()) != null)
                {
                    country.Add(ReadCountryFromCsvLine(csvLine));
                }
            }
            return country;
        }

        public Country[] ReadFirstNCountries(int nCountries)
        {
            Country[] countries = new Country[nCountries];
            using (StreamReader sr = new StreamReader(_csvFile))
            {
                sr.ReadLine();
                for (int i = 0; i < nCountries; i++)
                {
                    string csvLine = sr.ReadLine();
                    if ((csvLine = sr.ReadLine()) != null)
                    {
                        countries[i] = ReadCountryFromCsvLine(csvLine);
                    }
                    else
                    {
                        return countries;
                    }
                }
            }
            return countries;
        }


        public Dictionary<string, List<Country>> ReadAllRegions()
        {
            var countries = new Dictionary<string, List<Country>>();
            using (StreamReader sr = new StreamReader(_csvFile))
            {
                sr.ReadLine();
                string csvLine;
                while ((csvLine = sr.ReadLine()) != null)
                {
                    Country country = ReadCountryFromCsvLine(csvLine);
                    if (countries.ContainsKey(country.Region))
                    {
                        countries[country.Region].Add(country);
                    }
                    else
                    {
                        List<Country> countriesInRegion = new List<Country>() { country };
                        countries.Add(country.Region, countriesInRegion);
                    }
                }
            }
            return countries;
        }

        public Dictionary<string, Country> ReadAllCountries()
        {
            var countries = new Dictionary<string, Country>();

            using (StreamReader sr = new StreamReader(_csvFile))
            {
                sr.ReadLine();
                string csvLine;
                while ((csvLine = sr.ReadLine()) != null)
                {
                    Country country = ReadCountryFromCsvLine(csvLine);
                    countries.Add(country.Code, country);
                }
            }
            return countries;
        }

        public void RemoveCommaCountries(List<Country> countries)
        {
            countries.RemoveAll(x => x.Name.Contains(','));
        }

        public void PrintCountries(string filepath)
        {
            int userInput = AskForCountryAmount();
            if (userInput == 0)
            {
                return;
            }

            List<Country> countries = ReadAllCountriesAsList();
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

        public int AskForCountryAmount()
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

        private List<Country> CleanCountries(List<Country> countries)
        {
            var filteredCountries = from country in countries
                                    where !country.Name.Contains(',')
                                    select country;

            List<Country> newList = new List<Country>();
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

        private static void CountryText(int count, Country country)
        {
            Console.WriteLine($"{count}: {PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: " +
                $"{country.Name}");
        }

        //private void OrderXCountriesByName(List<Country> countries, int amount)
        //{
        //    int count = 0;
        //    foreach (Country country in countries.OrderBy(x => x.Name).Take(amount))
        //    {
        //        count++;
        //        CountryText(count, country);
        //    }
        //}

        private List<Country> OrderbyName(List<Country> countries)
        {
            List<Country> newList = new List<Country>();
            foreach (Country country in countries.OrderBy(x => x.Name))
            {
                newList.Add(country);
            }
            return newList;
        }

        public Dictionary<string, List<Country>> PrintCountriesByRegion()
        {
            int userInput = AskForCountryAmount();
            if (userInput == 0)
            {
                return null;
            }
            var countries = ReadAllRegions();

            foreach (string region in countries.Keys)
            {
                Console.WriteLine(region);
            }

            Console.WriteLine("Which of the above regions do you want?");
            string chosenRegion = Console.ReadLine();

            int loopSize = Math.Min(userInput, countries[chosenRegion].Count);

            int count = 0;

            if (countries.ContainsKey(chosenRegion))
            {
                foreach (Country country in countries[chosenRegion].Take(loopSize))
                {
                    count++;
                    CountryText(count, country);
                }
            }
            else
            {
                Console.WriteLine("That is not a valid region");
            }
            return countries;
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

        public void PrintAllCountries()
        {
            List<Country> countries = ReadAllCountriesAsList();

            //foreach (Country country in countries)
            for (int i = 0; i < countries.Count; i++)
            {
                Country country = countries[i];
                Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: " +
                    $"{country.Name}");
            }

            Console.WriteLine($"{countries.Count} countries");
        }

        public void PrintSelectedCountry()
        {
            Dictionary<string, Country> countries = ReadAllCountries();

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

        private static void PrintXCountriesAscendingOrDecending(List<Country> countries, int loopSize, bool assending)
        {
            if (assending)
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
                for (int i = countries.Count - 1; i >= 0; i--)
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

    }
}