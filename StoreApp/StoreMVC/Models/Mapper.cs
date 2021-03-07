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
                CustomerPasswordHash = null,
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
                //CustomerPasswordHash = customer.CustomerPasswordHash,
                CustomerPhone = customer.CustomerPhone,
                CustomerAddress = customer.CustomerAddress,
                CustomerId = customer.Id
            };
        }
        public Customer cast2Customer(CustomerEditVM customer2bCasted)
        {
            return new Customer
            {
                Id = customer2bCasted.CustomerId,
                CustomerName = customer2bCasted.CustomerName,
                CustomerEmail = customer2bCasted.CustomerEmail,
                //CustomerPasswordHash = customer2bCasted.CustomerPasswordHash,
                CustomerPhone = customer2bCasted.CustomerPhone,
                CustomerAddress = customer2bCasted.CustomerAddress
            };
        }
    }
}
