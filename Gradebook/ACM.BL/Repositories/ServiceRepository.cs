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
    public class ServiceRepository : IPersonRepository
    {
        WebClient client = new WebClient();
        string baseUri = "https://localhost:5001";

        public IEnumerable<Person> GetPeople()
        {
            string result = client.DownloadString(baseUri);
            IEnumerable<Person> people = JsonConvert.DeserializeObject<IEnumerable<Person>>(result);
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

        public void DeletePerson(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(int id, Person updatedPerson)
        {
            throw new NotImplementedException();
        }
    }
}
