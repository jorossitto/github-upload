using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class CsvRepository : IPersonRepository
    {
        string Path;
        public CsvRepository(string path = @"StockData.csv")
        {
            var Path = path;
        }
        public IEnumerable<Person> GetPeople()
        {
            var people = new List<Person>();
            if(File.Exists(Path))
            {
                using (var reader = new StreamReader(Path))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        var elements = line.Split(',');
                        var person = new Person()
                        {
                            Id = Int32.Parse(elements[0]),
                            FirstName = elements[1],
                            LastName = elements[2],
                            StartDate = DateTime.Parse(elements[3]),
                            Rating = Int32.Parse(elements[4]),
                            FormatString = elements[5],
                        };
                        people.Add(person);
                    }
                }
            }

            return people;
        }

        public Person GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        public void AddPerson(Person newPerson)
        {
            throw new NotImplementedException();
        }
        public void UpdatePerson(int id, Person updatedPerson)
        {
            throw new NotImplementedException();
        }
        public void DeletePerson(int id)
        {
            throw new NotImplementedException();
        }


    }
}
