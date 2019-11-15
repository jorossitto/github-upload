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
            AddUnitToLocation(locationId);
            AddUnitToLocation(locationId);
        }

        private static void AddUnitToLocation(int locationId)
        {
            var unit = new Unit { LocationId = 1, Acquired = DateTime.Now, BrewerTypeId = 1 };
            businessDbContext.Units.Add(unit);
            businessDbContext.SaveChanges();
        }

        private static void CreateSamuraiWithBetterName()
        {
            var samurai = new Samurai
            {
                Name = "Jack le Black",
                BetterName = PersonFullName.Create("Jack", "Black")
            };

            businessDbContext.Samurais.Add(samurai);
            businessDbContext.SaveChanges();
        }
    }
}
