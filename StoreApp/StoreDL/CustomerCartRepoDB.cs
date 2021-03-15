using System.Collections.Generic;
using Model = StoreModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreModels;
namespace StoreDL
{
    public class CustomerCartRepoDB : ICustomerCartRepository
    {
        private StoreDBContext _context;
        public CustomerCartRepoDB(StoreDBContext context)
        {
            _context = context;
        }
        public CustomerCart AddCustomerCart(CustomerCart newCustomerCart)
        {
            _context.CustomerCarts.Add(newCustomerCart);
            _context.SaveChanges();
            return newCustomerCart;
        }

        public CustomerCart DeleteCustomerCart(CustomerCart customerCart2BDeleted)
        {
            _context.CustomerCarts.Remove(customerCart2BDeleted);
            _context.SaveChanges();
            return customerCart2BDeleted;
        }

        public CustomerCart GetCustomerCartByIds(int customerId, int locId)
        {
            return _context.CustomerCarts
            .AsNoTracking()
            .Select(x => x)
            .ToList()
            .FirstOrDefault(x => x.CustId == customerId && x.LocId == locId);
        }

        public List<CustomerCart> GetCustomerCarts()
        {
            return _context.CustomerCarts
                .AsNoTracking()
                .Select(x => x)
                .ToList();
        }

        public CustomerCart UpdateCustomerCart(CustomerCart customerCart2BUpated)
        {
            CustomerCart oldCustomerCart = _context.CustomerCarts.Find(customerCart2BUpated.Id);
            _context.Entry(oldCustomerCart).CurrentValues.SetValues(customerCart2BUpated);

            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return customerCart2BUpated;
        }
        
    }
}