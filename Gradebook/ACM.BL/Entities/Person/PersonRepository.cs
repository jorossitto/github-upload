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
        Person[] tempPerson = new Person[1];
        public Person[] GetPeople()
        {
            return tempPerson;
        }
        public Person GetPerson(int id)
        {
            return tempPerson[0];
        }

    }
}
