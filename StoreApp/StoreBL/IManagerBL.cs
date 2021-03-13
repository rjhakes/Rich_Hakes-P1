using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public interface IManagerBL
    {
        List<Manager> GetManagers();
        Manager AddManager(Manager newManager);
        Manager GetManagerByEmail(string email);
        Manager DeleteManager(Manager manager2BDeleted);
        Manager UpdateManager(Manager manager2BUpdated);
    }
}