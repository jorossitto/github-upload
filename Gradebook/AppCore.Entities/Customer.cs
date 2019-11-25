using Common;
using System.Collections.Generic;

namespace AppCore.Entities
{
    public class Customer : Person, ILoggable, ICustomer
    {
        public List<Address> AddressList { get; set; }
        public int CustomerType { get; set; }

        public bool IsGold { get; set; }

        public Customer(): this(0) //chain calls constructors with the 0 customerId
        {
            
        }

        public Customer(int customerId)
        {
            Id = customerId;
            AddressList = new List<Address>();
        }



        public string Log() => $"{Id}: {FullName} Email: {EmailAddress} Status: {EntityState.ToString()}";
        


        public static Customer CreateDefaultCustomer()
        {
            return new Customer(1)
            {
                EmailAddress = "fbaggins@hobbiton.me",
                FirstName = "Frodo",
                LastName = "Baggins",
                AddressList = null
            };
        }


    }
}
