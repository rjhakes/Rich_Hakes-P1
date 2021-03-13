using StoreModels;
using System.Collections.Generic;
namespace StoreDL
{
    public interface IManagerRepository
    {
         List<Manager> GetManagers();
         Manager AddManager(Manager newManager);
         Manager GetManagerByEmail(string email);
        Manager DeleteManager(Manager manager2BDeleted);
        Manager UpdateManager(Manager manager2BUpdated);
    }
}