using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fundamentals
{
    public class Country
    {
        public string Name { get; }
        public string Code { get; }
        public string Region { get; }
        public int Population { get; }

        public Country(string name, string code, string region, int population)
        {
            Name = name;
            Code = code;
            Region = region;
            Population = population;
        }
    }
}