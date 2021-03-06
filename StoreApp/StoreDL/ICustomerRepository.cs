using StoreModels;
using System.Collections.Generic;

namespace StoreDL
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        Customer AddCustomer(Customer newCustomer);
        Customer GetCustomerByEmail(string email);
        Customer DeleteCustomer(Customer customer2BDeleted);
        Customer UpdateCustomer(Customer customer2BUpdated);
    }
}