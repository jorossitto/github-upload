using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fundamentals
{
    public class PlayerCharacter
    {
        public string Name { get; set; }
        public int? DaysSinceLastLogin { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsNoob { get; set; }

        //public PlayerCharacter()
        //{
        //    DateOfBirth = null; //magic number
        //    DaysSinceLastLogin = null; //magic number
        //}
    }
}