using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fundamentals
{
    public class PlayerCharacter
    {
        public string Name { get; set; }
        public int DaysSinceLastLogin { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int NeverLoggedIn
        {
            get
            {
                return -1;
            }
        }

        public DateTime NeverBorn
        {
            get
            {
                return DateTime.MinValue;
            }
        }

        public PlayerCharacter()
        {
            DateOfBirth = NeverBorn; //magic number
            DaysSinceLastLogin = NeverLoggedIn; //magic number
        }
    }
}