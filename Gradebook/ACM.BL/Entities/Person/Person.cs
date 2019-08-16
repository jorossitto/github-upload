﻿using System;
using System.Collections.Generic;

namespace ACM.BL
{
    public class Person : EntityBase
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime StartDate { get; set; }
        public int Rating { get; set; }
        public string FormatString { get; set; }
        public string FullName
        {
            get
            {
                string fullName = LastName;
                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    if (!string.IsNullOrWhiteSpace(fullName))
                    {
                        fullName += ", ";
                    }
                    fullName += FirstName;
                }
                return fullName;
            }
        }
        public override string ToString() => FullName;

        public override bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrWhiteSpace(FullName)) isValid = false;
            if (string.IsNullOrWhiteSpace(EmailAddress)) isValid = false;

            return isValid;
        }
        public static void Main()
        {
            string branch;
            Console.WriteLine("Enter 1 to Read Stock Data, 2 to ConvertLocaltoSidney");

            branch = Console.ReadLine();
            if (branch == "1")
            {
                PrintPeople();

            }
            else if (branch == "2")
            {
                //ConvertLocalToSidney();
            }

            Console.ReadLine();
        }

        private static void PrintPeople()
        {
            var developers = CreateTestPeopleArray();
            var sales = CreateTestPeopleList();

            IEnumerator<Person> enumerator = developers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.FullName);
            }

            //foreach (var person in sales)
            //{
            //    Console.WriteLine(person.FullName);
            //}
        }

        private static IEnumerable<Person> CreateTestPeopleArray()
        {
            IEnumerable<Person> people = new Person[]
            {
                new Person {Id = 1, FirstName = "Scott"},
                new Person {Id = 2, FirstName = "Chris"}
            };

            return people;
        }

        private static IEnumerable<Person> CreateTestPeopleList()
        {
            IEnumerable<Person> people = new List<Person>
            {
                new Person {Id = 3, FirstName = "Alex"}
            };

            return people;
        }

    }
}
