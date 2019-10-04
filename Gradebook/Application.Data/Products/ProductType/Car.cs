using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace Application.Data
{
    public class Car : Product, ILoggable
    {
        public int Year { get; set; }
        public string Manufacturer { get; set; }
        public double Displacement { get; set; }
        public int Cylinders { get; set; }
        public int City { get; set; }
        public int Highway { get; set; }
        public int Combined { get; set; }

        public static void Main()
        {
            string branch;
            Console.WriteLine("Enter 1 to Read Stock Data, 2 to ConvertLocaltoSidney");

            branch = Console.ReadLine();
            if (branch == "1")
            {
                HandleCars();
            }
            else if (branch == "2")
            {
                //ConvertLocalToSidney();
            }

            Console.ReadLine();
        }
        private static void HandleCars()
        {
            var cars = ProcessFile("fuel.csv");

            var query = cars.OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name);

            foreach(var car in query.Take(10))
            {
                Console.WriteLine($"Name :{car.Name}, Fuel: {car.Combined}");
            }

            PrintHelper.LineBreak();


            query =
                from car in cars
                where car.Manufacturer == "BMW" && car.Year == 2019
                orderby car.Combined descending, car.Name
                select car;

            var top = cars  .OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name)
                            .Select(c => c)
                            .First(c => c.Manufacturer == "BMW" && c.Year == 2019);
            Console.WriteLine(top.Name);
            PrintHelper.LineBreak();

            var result = cars.Any(c => c.Manufacturer == "Ford");

            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"Name :{car.Name}, Fuel: {car.Combined}");
            }

            PrintHelper.LineBreak();
        }

        private static List<Car> ProcessFile(string path)
        {
            return
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .Select(ParseFromCSV)
                    .ToList();
        }

        private static List<Car> ProjectFileData(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .ToCar();

            return query.ToList();
        }

        internal static Car ParseFromCSV(string line)
        {
            var columns = line.Split(',');
            return new Car
            {
                Year = int.Parse(columns[0]),
                Manufacturer = columns[1],
                Name = columns[2],
                Displacement = double.Parse(columns[3]),
                Cylinders = int.Parse(columns[4]),
                City = int.Parse(columns[5]),
                Highway = int.Parse(columns[6]),
                Combined = int.Parse(columns[7])
            };
        }

    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');
                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }

        }
    }


}
