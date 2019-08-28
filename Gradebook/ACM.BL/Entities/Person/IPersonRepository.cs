using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public interface IPersonRepository
    {
        void AddPerson(Person newPerson);
        IEnumerable<Person> GetPeople();
        Person GetPerson(int id);
        void UpdatePerson(int id, Person updatedPerson);
        void DeletePerson(int id);

    }
}
