using System.Collections.Generic;
using Model = StoreModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreModels;
namespace StoreDL
{
    public class CustomerOrderHistoryRepoDB : ICustomerOrderHistoryRepository
    {
        private StoreDBContext _context;
        public CustomerOrderHistoryRepoDB(StoreDBContext context)
        {
            _context = context;
        }
        public CustomerOrderHistory AddCustomerOrderHistory(CustomerOrderHistory newCustomerOrderHistory)
        {
            _context.CustomerOrderHistories.Add(newCustomerOrderHistory);
            _context.SaveChanges();
            return newCustomerOrderHistory;
        }

        public CustomerOrderHistory DeleteCustomerOrderHistory(CustomerOrderHistory customerOrderHistory2BDeleted)
        {
            _context.CustomerOrderHistories.Remove(customerOrderHistory2BDeleted);
            _context.SaveChanges();
            return customerOrderHistory2BDeleted;
        }

        public CustomerOrderHistory GetCustomerOrderHistoryById(int id)
        {
            return _context.CustomerOrderHistories
            .AsNoTracking()
            .Select(x => x)
            .ToList()
            .FirstOrDefault(x => x.OrderId == id);
        }

        public List<CustomerOrderHistory> GetCustomerOrderHistories()
        {
            return _context.CustomerOrderHistories.AsNoTracking().Select(x => x).ToList();
        }

        public CustomerOrderHistory UpdateCustomerOrderHistory(CustomerOrderHistory customerOrderHistory2BUpated)
        {
            CustomerOrderHistory oldCustomerOrderHistory = _context.CustomerOrderHistories.Find(customerOrderHistory2BUpated.Id);
            _context.Entry(oldCustomerOrderHistory).CurrentValues.SetValues(customerOrderHistory2BUpated);

            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return customerOrderHistory2BUpated;
        }
    }
}