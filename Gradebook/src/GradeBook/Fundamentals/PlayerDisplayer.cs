using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fundamentals
{
    /// <summary>
    /// Class needs PlayerCharacter
    /// </summary>
    class PlayerDisplayer
    {
        public static void Write(PlayerCharacter player)
        {
            Console.WriteLine(player.Name);
            if(player.DaysSinceLastLogin == player.NeverLoggedIn)
            {
                Console.WriteLine("No value for Last Login");
            }
            else
            {
                Console.WriteLine(player.DaysSinceLastLogin);
            }

            if(player.DateOfBirth == player.NeverBorn)
            {
                Console.WriteLine("No date of birth specified");
            }
            else
            {
                Console.WriteLine(player.DateOfBirth);
            }
        }
    }
}