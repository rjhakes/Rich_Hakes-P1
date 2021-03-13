using StoreModels;

namespace StoreMVC.Models
{
    public interface IMapper
    {
        Customer cast2Customer(CustomerCRVM customer2BCasted);
        CustomerIndexVM cast2CustomerIndexVM(Customer customer2BCasted);
        CustomerCRVM cast2CustomerCRVM(Customer customer);
        CustomerEditVM cast2CustomerEditVM(Customer customer);
        Customer cast2Customer(CustomerEditVM customer2bCasted);
        bool verifyPW(string pwHash, string newPW);
        Manager cast2Manager(ManagerCRVM manager2BCasted);
        ManagerIndexVM cast2ManagerIndexVM(Manager manager2BCasted);
        ManagerCRVM cast2ManagerCRVM(Manager manager);
        ManagerEditVM cast2ManagerEditVM(Manager manager);
        Manager cast2Manager(ManagerEditVM manager2bCasted);
        Location cast2Location(LocCRVM location2BCasted);
        LocIndexVM cast2LocationIndexVM(Location location2BCasted);
        LocCRVM cast2LocationCRVM(Location location);
        LocEditVM cast2LocationEditVM(Location location);
        Location cast2Location(LocEditVM location2bCasted);
    }
}