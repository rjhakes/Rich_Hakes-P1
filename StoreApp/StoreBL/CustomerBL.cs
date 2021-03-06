using System;
using System.Collections.Generic;
using StoreDL;
using StoreModels;

namespace StoreBL
{
    public class CustomerBL : ICustomerBL
    {
        private ICustomerRepository _repo;

        public CustomerBL(ICustomerRepository repo) {
            _repo = repo;
        }

        public Customer AddCustomer(Customer newCustomer)
        {
            //TODO: Add BL
            return _repo.AddCustomer(newCustomer);
        }
        public Customer DeleteCustomer(Customer customer2BDeleted)
        {
            return _repo.DeleteCustomer(customer2BDeleted);
        }
        public Customer GetCustomerByEmail(string email) {
            //todo validate
            return _repo.GetCustomerByEmail(email);
        }
        public List<Customer> GetCustomers()
        {
            //TODO Add BL
            return _repo.GetCustomers();
        }
        public Customer UpdateCustomer(Customer customer2BUpdated)
        {
            return _repo.UpdateCustomer(customer2BUpdated);
        }

        
    }
}
