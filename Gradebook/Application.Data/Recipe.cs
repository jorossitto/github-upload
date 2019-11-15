using Microsoft.EntityFrameworkCore;
using System;

namespace AppCore.Data
{
    [Owned]
    public class Recipe
    {
        public int WaterTemperatureF { get; set; }
        public int GrindSize { get; set; }
        public int GrindOunces { get; set; }
        public int WaterOunces { get; set; }
        public int BrewMinutes { get; set; }
        public string Description { get; set; }

    }
}