using StoreBL;
using StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class Mapper : IMapper
    {
        public CustomerIndexVM cast2CustomerIndexVM(Customer customer2BCasted)
        {
            return new CustomerIndexVM
            {
                CustomerName = customer2BCasted.CustomerName,
                CustomerEmail = customer2BCasted.CustomerEmail,
                CustomerPhone = customer2BCasted.CustomerPhone,
                CustomerAddress = customer2BCasted.CustomerAddress
            };
        }

        public Customer cast2Customer(CustomerCRVM customer2BCasted)
        {
            return new Customer
            {
                CustomerName = customer2BCasted.CustomerName,
                CustomerEmail = customer2BCasted.CustomerEmail,
                CustomerPasswordHash = Convert.ToBase64String(new StoreBL.PasswordHash(customer2BCasted.CustomerPasswordHash).ToArray()),
                CustomerPhone = customer2BCasted.CustomerPhone,
                CustomerAddress = customer2BCasted.CustomerAddress
            };
        }
        public CustomerCRVM cast2CustomerCRVM(Customer customer)
        {
            return new CustomerCRVM
            {
                CustomerName = customer.CustomerName,
                CustomerEmail = customer.CustomerEmail,
                CustomerPhone = customer.CustomerPhone,
                CustomerAddress = customer.CustomerAddress
            };
        }
        public CustomerEditVM cast2CustomerEditVM(Customer customer)
        {
            return new CustomerEditVM
            {
                CustomerName = customer.CustomerName,
                CustomerEmail = customer.CustomerEmail,
                CustomerPasswordHash = "",
                CustomerPhone = customer.CustomerPhone,
                CustomerAddress = customer.CustomerAddress,
                CustomerId = customer.Id
            };
        }
        public Customer cast2Customer(CustomerEditVM customer2BCasted)
        {

            return new Customer
            {
                Id = customer2BCasted.CustomerId,
                CustomerName = customer2BCasted.CustomerName,
                CustomerEmail = customer2BCasted.CustomerEmail,
                CustomerPasswordHash = Convert.ToBase64String(new StoreBL.PasswordHash(customer2BCasted.CustomerPasswordHash).ToArray()),
                CustomerPhone = customer2BCasted.CustomerPhone,
                CustomerAddress = customer2BCasted.CustomerAddress
            };
        }

        public bool verifyPW(string pwHash, string newPW)
        {
            byte[] hashBytes = Convert.FromBase64String(pwHash);
            PasswordHash customerPasswordHash = new PasswordHash(hashBytes);
            //Customer _customer = _customerBL.GetCustomerByEmail(userEmail);
            return customerPasswordHash.Verify(newPW);
        }
    }
}
