using System;
using System.Globalization;

namespace TestNinja.Fundamentals
{
    public static class DateHelper
    {
        static Calendar calendar = CultureInfo.InvariantCulture.Calendar;
        static string[] daysOfWeek = {"Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"};

        public static DateTime FirstOfNextMonth(DateTime date)
        {
            return date.Month == 12 ? new DateTime(date.Year + 1, 1, 1) : new DateTime(date.Year, date.Month + 1, 1);
        }

        public static void DaysOfWeek(string[] args)
        {
            Console.WriteLine("Which day do you want to display");
            Console.Write("(Monday = 1, etc.) > ");
            int iDay = int.Parse(Console.ReadLine());

            string chosenDay = daysOfWeek[iDay-1];
            Console.WriteLine($"That day is {chosenDay}");
        }

        public static string[] GetDaysOfWeek()
        {
            return daysOfWeek;
        }

        public static void mainMethod()
        {
            string branch;
            Console.WriteLine("Enter 1 to Read Stock Data, 2 to ConvertLocaltoSidney, 3 to GetAllTimezones," +
                "4 to do DateTimeParsing, 5 to Add Time, 6 to Work With Calanders, 7 to Convert to Unix  ");

            branch = Console.ReadLine();
            if (branch == "1")
            {
                ExtendingDates();
            }
            else if (branch == "2")
            {
                ConvertLocalToSidney();
            }
            else if (branch == "3")
            {
                GetAllTimezones();
            }
            else if (branch == "4")
            {
                DateTimeParsing();
            }
            else if (branch == "5")
            {
                AddingTime();
            }
            else if (branch == "6")
            {
                WorkingWithCalanders();
            }
            else if (branch == "7")
            {
                ConvertToUnix();
            }

            Console.ReadLine();
        }

        private static void ConvertLocalToSidney()
        {
            var now = DateTime.Now;
            var now2 = DateTimeOffset.Now;

            TimeZoneInfo sydneyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. Australia Standard Time");

            var sydneyTime = TimeZoneInfo.ConvertTime(now, sydneyTimeZone);

            Console.WriteLine(now);
            Console.WriteLine(now2);
            Console.WriteLine(sydneyTime);
        }

        private static void GetAllTimezones()
        {
            var time = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(+10));

            foreach (var timeZone in TimeZoneInfo.GetSystemTimeZones())
            {
                if (timeZone.GetUtcOffset(time) == time.Offset)
                {
                    Console.WriteLine(timeZone);
                }
            }
        }

        private static void DateTimeParsing()
        {
            var date = "9/10/2019 10:00:00 PM";

            var parsedDateOffset = DateTimeOffset.ParseExact(date, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

            parsedDateOffset = parsedDateOffset.ToOffset(TimeSpan.FromHours(10));

            var formattedDate = parsedDateOffset.ToString("yyyy-MMM-dd");

            Console.WriteLine(parsedDateOffset);
            Console.WriteLine(formattedDate);

            formattedDate = parsedDateOffset.ToString("s");
            Console.WriteLine(formattedDate);

            formattedDate = parsedDateOffset.ToString("o");
            Console.WriteLine(formattedDate);

            date = "9/10/2019 10:00:00 PM +02:00";
            var parsedDate = DateTime.Parse(date, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

            Console.WriteLine(parsedDate);
            Console.WriteLine(parsedDate.Kind);

            //bestpractice
            var UniversalTime = DateTimeOffset.UtcNow;

            Console.WriteLine($"Best Practice: {UniversalTime}");
            Console.WriteLine($"Best Practice Local Time: {UniversalTime.ToLocalTime()}");

            //Convert from local to universal
            var localTime = DateTimeOffset.Now;
            Console.WriteLine($"Best Practice Universal Time: {localTime.ToUniversalTime()}");

        }

        private static void AddingTime()
        {
            var timeSpan = new TimeSpan(60, 100, 200);

            Console.WriteLine($"Total Days: {timeSpan.TotalDays}");
            Console.WriteLine($"Days: {timeSpan.Days}");
            Console.WriteLine($"Hours: {timeSpan.Hours}");
            Console.WriteLine($"Minutes: {timeSpan.Minutes}");
            Console.WriteLine($"Seconds: {timeSpan.Seconds}");

            //adding time
            var start = DateTimeOffset.UtcNow;
            var end = start.AddSeconds(45);

            TimeSpan difference = end - start;
            Console.WriteLine($"Seconds: {difference.TotalSeconds}");
        }

        private static void WorkingWithCalanders()
        {

            var start = new DateTimeOffset(2007, 12, 31, 0, 0, 0, TimeSpan.Zero);
            var week = calendar.GetWeekOfYear(start.DateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            Console.WriteLine($"Week: {week}");
            var isoWeek = GetISO8601Weekofyear(start.DateTime);
            Console.WriteLine($"Week: {isoWeek}");
        }

        public static int GetISO8601Weekofyear(DateTime time)
        {
            DayOfWeek day = calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        private static void ExtendingDates()
        {
            var contractDate = new DateTimeOffset(2020, 2, 29, 0, 0, 0, TimeSpan.Zero);

            Console.WriteLine(contractDate);

            contractDate = ExtendContract(contractDate, 1);

            Console.WriteLine(contractDate);
        }

        public static DateTimeOffset ExtendContract(DateTimeOffset current, int months)
        {
            var newContractDate = current.AddMonths(months).AddTicks(-1);
            return new DateTimeOffset(newContractDate.Year, newContractDate.Month,
                DateTime.DaysInMonth(newContractDate.Year, newContractDate.Month), 23, 59, 59, current.Offset);
        }

        public static bool IsBetween(this DateTimeOffset source, DateTimeOffset start, DateTimeOffset end)
        {
            return source > start && source < end;
        }

        public static void ConvertToUnix()
        {
            var timestamp = 1562335678;

            var unixDateStart = new DateTime(1970, 01, 01, 00, 00, 00, DateTimeKind.Utc);

            var result = DateTimeOffset.FromUnixTimeSeconds(timestamp);

            Console.WriteLine(result);

            var result2 = unixDateStart.AddSeconds(timestamp);

            Console.WriteLine(new DateTimeOffset(result2).ToUnixTimeSeconds());

            Console.WriteLine(result2);
        }

    }
}