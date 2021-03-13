using StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDL
{
    public class ManagerRepoDB : IManagerRepository
    {
        private readonly StoreDBContext _context;
        public ManagerRepoDB(StoreDBContext context)
        {
            _context = context;
        }
        public Manager AddManager(Manager newManager)
        {
            _context.Managers.Add(newManager);
            _context.SaveChanges();
            return newManager;
        }

        public Manager DeleteManager(Manager manager2BDeleted)
        {
            _context.Managers.Remove(manager2BDeleted);
            _context.SaveChanges();
            return manager2BDeleted;
        }

        public Manager GetManagerByEmail(string email)
        {
            return _context.Managers
                .FirstOrDefault(x => x.ManagerEmail == email);
        }

        public List<Manager> GetManagers()
        {
            return _context.Managers
                .Select(x => x)
                .ToList();
        }

        public Manager UpdateManager(Manager manager2BUpdated)
        {
            Manager oldManager = _context.Managers.Find(manager2BUpdated.Id);


            _context.Entry(oldManager).CurrentValues.SetValues(manager2BUpdated);

            _context.SaveChanges();

            //This method clears the change tracker to drop all tracked entities
            _context.ChangeTracker.Clear();
            return manager2BUpdated;

        }
    }
}
