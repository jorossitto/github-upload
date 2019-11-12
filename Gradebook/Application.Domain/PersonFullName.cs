using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Domain
{
    public class PersonFullName
    {
        #region variables
        public int PersonFullNameID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName => $"{firstName} {lastName}";
        public string fullNameReverse => $"{lastName}, {firstName}";
        #endregion

        #region constructor
        public PersonFullName(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
        #endregion

    }
}
