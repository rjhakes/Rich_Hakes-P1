using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public interface ICustomerBL
    {
        List<Customer> GetCustomers();
        Customer AddCustomer(Customer newCustomer);
        Customer GetCustomerByEmail(string email);
        Customer DeleteCustomer(Customer customer2BDeleted);
        Customer UpdateCustomer(Customer customer2BUpdated);
    }
}