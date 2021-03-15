using System;
using System.Collections.Generic;
using StoreDL;
using StoreModels;
namespace StoreBL
{
    public class CustomerCartBL : ICustomerCartBL
    {
        private ICustomerCartRepository _repo;

        public CustomerCartBL(ICustomerCartRepository repo) {
            _repo = repo;
        }

        public CustomerCart AddCustomerCart(CustomerCart newCustomerCart)
        {
            //TODO: Add BL
            return _repo.AddCustomerCart(newCustomerCart);
        }
        public CustomerCart DeleteCustomerCart(CustomerCart customerCart2BDeleted)
        {
            return _repo.DeleteCustomerCart(customerCart2BDeleted);
        }
        public CustomerCart GetCustomerCartByIds(int customerId, int locId) {
            //todo validate
            return _repo.GetCustomerCartByIds(customerId, locId);
        }
        public List<CustomerCart> GetCustomerCarts()
        {
            //TODO Add BL
            return _repo.GetCustomerCarts();
        }
        public CustomerCart UpdateCustomerCart(CustomerCart customerCart2BUpdated)
        {
            return _repo.UpdateCustomerCart(customerCart2BUpdated);
        }

    }
}