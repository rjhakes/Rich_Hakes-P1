using System.Collections.Generic;
using Model = StoreModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreModels;
namespace StoreDL
{
    public class CustomerOrderLineItemRepoDB : ICustomerOrderLineItemRepository
    {
        private StoreDBContext _context;
        public CustomerOrderLineItemRepoDB(StoreDBContext context)
        {
            _context = context;
        }
        public CustomerOrderLineItem AddCustomerOrderLineItem(CustomerOrderLineItem newCustomerOrderLineItem)
        {
            _context.CustomerOrderLineItems.Add(newCustomerOrderLineItem);
            _context.SaveChanges();
            return newCustomerOrderLineItem;
        }

        public CustomerOrderLineItem DeleteCustomerOrderLineItem(CustomerOrderLineItem customerOrderLineItem2BDeleted)
        {
            _context.CustomerOrderLineItems.Remove(customerOrderLineItem2BDeleted);
            _context.SaveChanges();
            return customerOrderLineItem2BDeleted;
        }

        public List<CustomerOrderLineItem> GetCustomerOrderLineItemById(int id)
        {
            return _context.CustomerOrderLineItems
                .AsNoTracking()
                .Where(x => x.OrderId == id)
                .Select(x => x)
                .ToList();
                //.FirstOrDefault(x => x.OrderId == id);
        }
        public CustomerOrderLineItem GetCustomerOrderLineItem(int id)
        {
            return _context.CustomerOrderLineItems
                .FirstOrDefault(x => x.Id == id);
        }
        public CustomerOrderLineItem GetCustomerOrderLineItemById(int orderId, int prodId)
        {
            return _context.CustomerOrderLineItems
                .AsNoTracking()
                .Select(x => x)
                .ToList()
                .FirstOrDefault(x => x.OrderId == orderId && x.ProdId == prodId);
        }

        public List<CustomerOrderLineItem> GetCustomerOrderLineItems()
        {
            return _context.CustomerOrderLineItems
                .AsNoTracking()
                .Select(x => x)
                .ToList();
        }

        public CustomerOrderLineItem UpdateCustomerOrderLineItem(CustomerOrderLineItem customerOrderLineItem2BUpated)
        {
            CustomerOrderLineItem oldCustomerOrderLineItem = _context.CustomerOrderLineItems.Find(customerOrderLineItem2BUpated.Id);
            _context.Entry(oldCustomerOrderLineItem).CurrentValues.SetValues(customerOrderLineItem2BUpated);

            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return customerOrderLineItem2BUpated;
        }
        public int Ident_Curr()
        {
            //int? intIdt = _context.CustomerOrderLineItems.Max()
            return _context.CustomerOrderLineItems.Max(x => x.Id);
        }
    }
}