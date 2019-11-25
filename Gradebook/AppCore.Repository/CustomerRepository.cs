using AppCore.Entities;
using System.Linq;
using System.Collections.Generic;


namespace AppCore.Repository
{
    public class CustomerRepository
    {
        public CustomerRepository()
        {
            addressRepository = new AddressRepository();
            addressRepository.RetrieveByCustomerId(1);
        }

        public AddressRepository addressRepository { get; private set; }
        public Customer Retrieve(int customerId)
        {
            Customer customer =  new Customer(customerId);
            if (customerId == 1)
            {
                customer.EmailAddress = "fbaggins@hobbiton.me";
                customer.FirstName = "Frodo";
                customer.LastName = "Baggins";
                customer.AddressList = addressRepository.RetrieveByCustomerId(customerId).ToList();
            }
            return customer;
        }
        /// <summary>
        /// Makes (amount) of default customers
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public IEnumerable<Customer> MakeDefaultCustomers(int amount)
        {
           List<Customer> customers = new List<Customer>();

            for (int i = 0; i < amount; i++)
            {
                Customer customer = new Customer(i);
                customer.EmailAddress = i + "@testEmail.com";
                customer.FirstName = "FirstName" + i;
                customer.LastName = "LastName" + i;
                customer.AddressList = addressRepository.RetrieveByCustomerId(i).ToList();
                customers.Add(customer);
            }
            return customers;
        }

        public void Add(Customer customer)
        {

        }

        public bool Save()
        {
            return true;
        }

        internal void Update()
        {
            //throw new NotImplementedException();
        }
        public static CustomerRepository CreateDefaultCustomerRepository()
        {
            return new CustomerRepository()
            {
            };
        }
    }
}
