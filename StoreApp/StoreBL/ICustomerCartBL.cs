using StoreModels;
using System.Collections.Generic;
namespace StoreBL
{
    public interface ICustomerCartBL
    {
        List<CustomerCart> GetCustomerCarts();
        CustomerCart AddCustomerCart(CustomerCart newCustomerCart);
        CustomerCart GetCustomerCartByIds(int customerId, int locId);
        CustomerCart DeleteCustomerCart(CustomerCart customerCart2BDeleted);
        CustomerCart UpdateCustomerCart(CustomerCart customerCart2BUpdated);
    }
}