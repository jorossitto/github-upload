using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ACM.BL.Entities
{ 
    public class Employee : Person
    {
        public IEnumerable<Job> _JobList { get; set; }

        public new static void Main()
        {
            string branch;
            Console.WriteLine("Enter 1 to Read Stock Data, 2 to ConvertLocaltoSidney");

            branch = Console.ReadLine();
            if (branch == "1")
            {
                PrintEmployee();

            }
            else if (branch == "2")
            {
                //ConvertLocalToSidney();
            }

            Console.ReadLine();
        }

        private static void PrintEmployee()
        {
            Func<int, int> Square = x => x*x;

            Func<int, int, int> add = (x, y) => x + y;

            void Write(int x) => Console.WriteLine(x);

            void LineBreak() => Console.WriteLine("************");

            Write(Square(add(3, 5)));

            var developers = CreateTestEmployeeArray();
            var sales = CreateTestEmployeeList();

            Console.WriteLine(developers.Count());

            LineBreak();
            IEnumerator<Employee> enumerator = developers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.FullName);
            }

            LineBreak();
            var query = developers.Where(e => e.FullName.StartsWith("S"));
            foreach (var employee in query)
            {
                Console.WriteLine(employee.FullName);
            }
            LineBreak();
            query = developers.Where(e => e.FullName.Length == 5).OrderBy(e => e.FullName);

            foreach (var employee in query)
            {
                Console.WriteLine(employee.FullName);
            }
            LineBreak();
        }

        private static IEnumerable<Employee> CreateTestEmployeeArray()
        {
            IEnumerable<Employee> employee = new Employee[]
            {
                new Employee {Id = 1, FirstName = "Scott"},
                new Employee {Id = 2, FirstName = "Chris"},
                new Employee {Id = 4, FirstName = "Bobby"}
            };

            return employee;
        }

        private static IEnumerable<Employee> CreateTestEmployeeList()
        {
            IEnumerable<Employee> employee = new List<Employee>
            {
                new Employee {Id = 3, FirstName = "Alex"}
            };

            return employee;
        }

        /// <summary>
        /// Makes (amount) of default customers
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static IEnumerable<Employee> MakeDefaultCustomers(int amount)
        {
            List<Employee> employees = new List<Employee>();

            for (int i = 0; i < amount; i++)
            {
                Employee employee = new Employee();
                employee.EmailAddress = i + "@testEmail.com";
                employee.FirstName = "FirstName" + i;
                employee.LastName = "LastName" + i;
                employee._JobList = Job.MakeDefaultJobs(i);
                employees.Add(employee);
            }
            return employees;
        }


    }

}