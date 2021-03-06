using System;
namespace StoreModels
{
    public class Manager
    {
        private string managerName;
        private string managerEmail;
        private string savedPasswordHash;
        private string managerPhoneNumber;
        private int managerLocId;
        
        public int Id { get; set; }

        public string ManagerName { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerPasswordHash { get; set; }
        public string ManagerPhone { get; set; }
        public int ManagerLocId { get; set; }

        public override string ToString() => $"\n\t Name:\t{this.ManagerName}\n\tEmail:\t{this.ManagerEmail}\n\tPhone:\t{this.ManagerPhone}";
    }
}