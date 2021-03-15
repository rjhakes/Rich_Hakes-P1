using StoreModels;
using System.Collections.Generic;
namespace StoreBL
{
    public interface ICustomerOrderHistoryBL
    {
        List<CustomerOrderHistory> GetCustomerOrderHistories();
        CustomerOrderHistory AddCustomerOrderHistory(CustomerOrderHistory newCustomerOrderHistory);
        CustomerOrderHistory GetCustomerOrderHistoryById(int id);
        CustomerOrderHistory DeleteCustomerOrderHistory(CustomerOrderHistory customerOrderHistory2BDeleted);
        CustomerOrderHistory UpdateCustomerOrderHistory(CustomerOrderHistory customerOrderHistory2BUpdated);
    }
}