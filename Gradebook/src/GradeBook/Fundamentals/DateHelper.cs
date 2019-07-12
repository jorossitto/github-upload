using System;

namespace TestNinja.Fundamentals
{
    public class DateHelper
    {
        string[] daysOfWeek = {"Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"};

        public static DateTime FirstOfNextMonth(DateTime date)
        {
            return date.Month == 12 ? new DateTime(date.Year + 1, 1, 1) : new DateTime(date.Year, date.Month + 1, 1);
        }

        public void DaysOfWeek(string[] args)
        {
            Console.WriteLine("Which day do you want to display");
            Console.Write("(Monday = 1, etc.) > ");
            int iDay = int.Parse(Console.ReadLine());

            string chosenDay = daysOfWeek[iDay-1];
            Console.WriteLine($"That day is {chosenDay}");
        }

        public string[] GetDaysOfWeek()
        {
            return daysOfWeek;
        }
    }
}