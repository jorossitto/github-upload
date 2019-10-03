using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class PersonRepository
    {
        public PersonRepository()
        {
        }

        //Person[] tempPerson = new Person[1];

        public IEnumerable<Person> GetPeople()
        {
            var people = Person.CreateTestPeopleArray();
            return people;
        }
        public Person GetPerson(int id)
        {
            var people = Person.CreateTestPeopleArray().ToArray();
            return people[id];
        }

    }
}
