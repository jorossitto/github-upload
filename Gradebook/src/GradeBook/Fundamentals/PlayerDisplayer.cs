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
            if(string.IsNullOrWhiteSpace(player.Name))
            {
                Console.WriteLine("Player name is null or white space");
            }
            else
            {
                Console.WriteLine(player.Name);
            }

            int days = player.DaysSinceLastLogin ?? -1;
            //int days = player.DaysSinceLastLogin.HasValue ? player.DaysSinceLastLogin.Value : -1;
            
            //int days = player.DaysSinceLastLogin.GetValueOrDefault(-1);
            Console.WriteLine($"{days} days since last login");

            if (player.DaysSinceLastLogin.HasValue)
            {
                Console.WriteLine(player.DaysSinceLastLogin.Value);
            }
            else
            {
                Console.WriteLine("No value for Last Login");
            }

            if(player.DateOfBirth == null)
            {
                Console.WriteLine("No date of birth specified");
            }
            else
            {
                Console.WriteLine(player.DateOfBirth);
            }

            if(player.IsNoob == null)
            {
                Console.WriteLine("player newbie status is unknown");
            }
            else if (player.IsNoob == true)
            {
                Console.WriteLine("Player is newbie");
            }
            else
            {
                Console.WriteLine("Player is experianced");
            }
        }
    }
}