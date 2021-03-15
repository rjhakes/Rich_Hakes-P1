using System;
using System.Collections.Generic;
using StoreDL;
using StoreModels;
namespace StoreBL
{
    public class CustomerOrderLineItemBL : ICustomerOrderLineItemBL
    {
        private ICustomerOrderLineItemRepository _repo;

        public CustomerOrderLineItemBL(ICustomerOrderLineItemRepository repo) {
            _repo = repo;
        }

        public CustomerOrderLineItem AddCustomerOrderLineItem(CustomerOrderLineItem newCustomerOrderLineItem)
        {
            //TODO: Add BL
            return _repo.AddCustomerOrderLineItem(newCustomerOrderLineItem);
        }
        public CustomerOrderLineItem DeleteCustomerOrderLineItem(CustomerOrderLineItem customerOrderLineItem2BDeleted)
        {
            return _repo.DeleteCustomerOrderLineItem(customerOrderLineItem2BDeleted);
        }

        public List<CustomerOrderLineItem> GetCustomerOrderLineItemById(int id) {
            //todo validate
            return _repo.GetCustomerOrderLineItemById(id);
        }
        public CustomerOrderLineItem GetCustomerOrderLineItemById(int orderId, int prodId) {
            //todo validate
            return _repo.GetCustomerOrderLineItemById(orderId, prodId);
        }
        public List<CustomerOrderLineItem> GetCustomerOrderLineItems()
        {
            //TODO Add BL
            return _repo.GetCustomerOrderLineItems();
        }
        public CustomerOrderLineItem UpdateCustomerOrderLineItem(CustomerOrderLineItem customerOrderLineItem2BUpdated)
        {
            return _repo.UpdateCustomerOrderLineItem(customerOrderLineItem2BUpdated);
        }

        public int Ident_Curr()
        {
            return _repo.Ident_Curr();
        }
    }
}