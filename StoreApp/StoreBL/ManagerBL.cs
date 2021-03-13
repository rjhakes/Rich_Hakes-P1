using System;
using System.Collections.Generic;
using StoreDL;
using StoreModels;

namespace StoreBL
{
    public class ManagerBL : IManagerBL
    {
        private IManagerRepository _repo;

        public ManagerBL(IManagerRepository repo) {
            _repo = repo;
        }

        public Manager AddManager(Manager newManager)
        {
            //TODO: Add BL
            return _repo.AddManager(newManager);
        }
        
        public Manager DeleteManager(Manager manager2BDeleted)
        {
            return _repo.DeleteManager(manager2BDeleted);
        }
        public Manager GetManagerByEmail(string email) {
            //todo validate
            return _repo.GetManagerByEmail(email);
        }
        public List<Manager> GetManagers()
        {
            //TODO Add BL
            return _repo.GetManagers();
        }
        public Manager UpdateManager(Manager manager2BUpdated)
        {
            return _repo.UpdateManager(manager2BUpdated);
        }
    }
}
