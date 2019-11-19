using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.Data;
using AppCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace AppCore.ConsoleUI
{
    class Program
    {
        private static int locationId = 1;
        public static BusinessDBContext businessDbContext = new BusinessDBContext();

        static void Main(string[] args)
        {
            //EntityCoreMapping.Main();
            //AddUnitToLocation(locationId);
            //AddUnitToLocation(locationId);
            //LazyLoadLocationEmployees();
            //LazyLoadWithoutContext();
            //LazyLoadWithNewContext();
            //StoreAndRetrieveABrewerType();
            SinglePropertyGroup();
            //MultiplePropertyGroupBy();
        }

        private static void MultiplePropertyGroupBy()
        {
            //using (var context = new BusinessDBContext())
            //{
            //    var unitGrouping = context.Units
            //        .GroupBy(o => new { o.BrewerTypeId, o.Location.Address1 })
            //        .Select(g => new
            //        {
            //            g.Key.BrewerTypeId,
            //            g.Key.Address1,
            //            Cost = g.Sum(u => u.Cost),
            //            Count = g.Count()
            //        }).ToList();

            //    foreach (var item in unitGrouping)
            //    {
            //        Console.WriteLine($"LocationId: {item.Address1}");
            //        Console.WriteLine($"TypeID: {item.BrewerTypeId}");
            //        Console.WriteLine($"#Units: {item.Count}");
            //        Console.WriteLine($"Total Cost: {item.Cost}");
            //        Console.WriteLine($"Average Cost: {item.Cost / item.Count}");
            //    }
            //}

        }

        private static void SinglePropertyGroup()
        {
            using (var context = new BusinessDBContext())
            {
                var unitGrouping = context.Units
                    .GroupBy(o => o.BrewerTypeId)
                    .OrderBy(g => g.Sum(u => u.Cost))
                    .Where(g => g.Count()>1)
                    .Select(g => new
                    {
                        BrewerTypeId = g.Key,
                        Cost = g.Sum(u => u.Cost),
                        Count = g.Count()

                    }).ToList();

                foreach (var item in unitGrouping)
                {
                    Console.WriteLine($"TypeID: {item.BrewerTypeId}");
                    Console.WriteLine($"#Units: {item.Count}");
                    Console.WriteLine($"Total Cost: {item.Cost}");
                    Console.WriteLine($"Average Cost: {item.Cost/ item.Count}");
                }
            }
        }

        private static void StoreAndRetrieveABrewerType()
        {
            var brewerType = new BrewerType
            {
                Description = "Aerator Hand Press ",
                Color = System.Drawing.Color.Green
            };
            using(var context = new BusinessDBContext())
            {
                context.BrewerTypes.Add(brewerType);
                context.SaveChanges();
            }
            using(var context = new BusinessDBContext())
            {
                var types = context.BrewerTypes.ToList();
            }
        }

        private static void LazyLoadWithNewContext()
        {
            Location location;
            using(var context = new BusinessDBContext())
            {
                location = context.Locations.FirstOrDefault();
            }

            using(var context2 = new BusinessDBContext())
            {
                context2.Locations.Attach(location);
                var employees = location.Employees;
                Console.WriteLine($"Second employee count: {employees.Count}");
            }
        }

        private static void LazyLoadWithoutContext()
        {
            Location location;
            using (var context = new BusinessDBContext())
            {
                location = context.Locations.FirstOrDefault();
            }
            var employees = location.Employees;
            Console.WriteLine($"First employee count: {employees.Count}");
        }

        private static void LazyLoadLocationEmployees()
        {
            using(var context = new BusinessDBContext())
            {
                context.ChangeTracker.LazyLoadingEnabled = true;
                var parse = DateTime.Parse("5am");
                var timeofday = parse.TimeOfDay;
                var hours = $"{timeofday.ToString()}";

                var locations = context.Locations.ToList();
                foreach (var l in locations)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{l.Address1}, Hours {l.Hours}");
                    if(DateTime.Parse(l.OpenTime).TimeOfDay <= TimeSpan.FromHours(7))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        foreach(var employee in l.Employees)
                        {
                            Console.WriteLine($"{employee.Name} {(employee.Barista ? " (Barista)" : "")}");
                        }
                    }
                }
            }
            Console.ReadKey();
        }
        private static void AddUnitToLocation(int locationId)
        {
            var unit = new Unit { LocationId = locationId, Acquired = DateTime.Now, BrewerTypeId = 1 };
            businessDbContext.Units.Add(unit);
            businessDbContext.SaveChanges();
        }
    }
}
