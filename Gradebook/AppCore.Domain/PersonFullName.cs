using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Domain
{
    //[Owned]
    public class PersonFullName
    {
        #region variables
        public int Id { get; set; }
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public string fullName => $"{firstName} {lastName}";
        public string fullNameReverse => $"{lastName}, {firstName}";
        #endregion

        #region constructor
        private PersonFullName(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        private PersonFullName()
        {

        }
        #endregion

        #region Methods
        public static PersonFullName Create(string firstName, string lastName)
        {
            return new PersonFullName(firstName, lastName);
        }

        public static PersonFullName Empty()
        {
            return new PersonFullName("", "");
        }

        public bool IsEmpty()
        {
            return lastName == "" & firstName == "";
        }

        public override bool Equals(object obj)
        {
            return obj is PersonFullName name &&
                   firstName == name.firstName &&
                   lastName == name.lastName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(firstName, lastName);
        }

        public static bool operator ==(PersonFullName left, PersonFullName right)
        {
            return EqualityComparer<PersonFullName>.Default.Equals(left, right);
        }

        public static bool operator !=(PersonFullName left, PersonFullName right)
        {
            return !(left == right);
        }
        #endregion
    }
}
