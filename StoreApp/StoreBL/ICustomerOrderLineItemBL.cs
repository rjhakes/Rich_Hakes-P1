using StoreModels;
using System.Collections.Generic;
namespace StoreBL
{
    public interface ICustomerOrderLineItemBL
    {
        List<CustomerOrderLineItem> GetCustomerOrderLineItems();
        CustomerOrderLineItem AddCustomerOrderLineItem(CustomerOrderLineItem newCustomerOrderLineItem);

        List<CustomerOrderLineItem> GetCustomerOrderLineItemById(int id);
        CustomerOrderLineItem GetCustomerOrderLineItem(int id);
        CustomerOrderLineItem GetCustomerOrderLineItemById(int orderId, int prodId);
        CustomerOrderLineItem DeleteCustomerOrderLineItem(CustomerOrderLineItem customerOrderLineItem2BDeleted);
        CustomerOrderLineItem UpdateCustomerOrderLineItem(CustomerOrderLineItem customerOrderLineItem2BUpdated);
        int Ident_Curr();
    }
}