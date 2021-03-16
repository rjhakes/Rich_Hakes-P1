using StoreModels;
using System.Collections.Generic;
namespace StoreDL
{
    public interface ICustomerOrderLineItemRepository
    {
        List<CustomerOrderLineItem> GetCustomerOrderLineItems();
        CustomerOrderLineItem AddCustomerOrderLineItem(CustomerOrderLineItem newCustomerOrderLineItem);
        List<CustomerOrderLineItem> GetCustomerOrderLineItemById(int id);
        CustomerOrderLineItem GetCustomerOrderLineItemById(int orderId, int prodId);
        CustomerOrderLineItem GetCustomerOrderLineItem(int id);
        CustomerOrderLineItem DeleteCustomerOrderLineItem(CustomerOrderLineItem customerOrderLineItem2BDeleted);
        CustomerOrderLineItem UpdateCustomerOrderLineItem(CustomerOrderLineItem customerOrderLineItem2BUpdated);
        int Ident_Curr();
    }
}