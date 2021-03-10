using StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDL
{
    public class CustomerRepoDB : ICustomerRepository
    {
        private readonly StoreDBContext _context;
        public CustomerRepoDB(StoreDBContext context)
        {
            _context = context;
        }
        public Customer AddCustomer(Customer newCustomer)
        {
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            return newCustomer;
        }

        public Customer DeleteCustomer(Customer customer2BDeleted)
        {
            _context.Customers.Remove(customer2BDeleted);
            _context.SaveChanges();
            return customer2BDeleted;
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _context.Customers
                .FirstOrDefault(x => x.CustomerEmail == email);
        }

        public List<Customer> GetCustomers()
        {
            return _context.Customers
                .Select(x => x)
                .ToList();
        }

        public Customer UpdateCustomer(Customer customer2BUpdated)
        {
            Customer oldCustomer = _context.Customers.Find(customer2BUpdated.Id);
            

            _context.Entry(oldCustomer).CurrentValues.SetValues(customer2BUpdated);

            _context.SaveChanges();

            //This method clears the change tracker to drop all tracked entities
            _context.ChangeTracker.Clear();
            return customer2BUpdated;

        }
    }
}
