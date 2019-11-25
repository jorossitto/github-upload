using System.Collections.Generic;

namespace AppCore.Entities
{
    public interface ICustomer
    {
         List<Address> AddressList { get; set; }
         int Id { get; }
         int CustomerType { get; set; }
         string EmailAddress { get; set; }
         string LastName { get; set; }
         string FirstName { get; set; }
         bool IsGold { get; set; }
         string FullName { get; }

    }
}
