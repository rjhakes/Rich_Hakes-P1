using System;
using System.Collections.Generic;
using StoreDL;
using StoreModels;
namespace StoreBL
{
    public class CustomerOrderHistoryBL : ICustomerOrderHistoryBL
    {
        private ICustomerOrderHistoryRepository _repo;

        public CustomerOrderHistoryBL(ICustomerOrderHistoryRepository repo) {
            _repo = repo;
        }

        public CustomerOrderHistory AddCustomerOrderHistory(CustomerOrderHistory newCustomerOrderHistory)
        {
            //TODO: Add BL
            return _repo.AddCustomerOrderHistory(newCustomerOrderHistory);
        }
        public CustomerOrderHistory DeleteCustomerOrderHistory(CustomerOrderHistory customerOrderHistory2BDeleted)
        {
            return _repo.DeleteCustomerOrderHistory(customerOrderHistory2BDeleted);
        }
        public List<CustomerOrderHistory> GetCustomerOrderHistoryById(int id) {
            //todo validate
            return _repo.GetCustomerOrderHistoryById(id);
        }
        public List<CustomerOrderHistory> GetCustomerOrderHistories()
        {
            //TODO Add BL
            return _repo.GetCustomerOrderHistories();
        }
        public CustomerOrderHistory UpdateCustomerOrderHistory(CustomerOrderHistory customerOrderHistory2BUpdated)
        {
            return _repo.UpdateCustomerOrderHistory(customerOrderHistory2BUpdated);
        }
    }
}