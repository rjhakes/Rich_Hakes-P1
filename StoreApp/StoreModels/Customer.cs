using System;
namespace StoreModels
{
    /// <summary>
    /// This class should contain necessary properties and fields for customer info.
    /// </summary>
    public class Customer
    {
        //TODO: add more properties to identify the customer
        private string custName;
        private string savedPasswordHash;
        private string custEmail;
        private string custPhoneNumber;
        private string custAddress;


        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPasswordHash { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerAddress { get; set; }

        public int Id { get; set; }


        public override string ToString() => $"\n\tName:\t\t{this.CustomerName}\n\tEmail:\t\t{this.CustomerEmail}\n\tPhone:\t\t{this.CustomerPhone}\n\tAddress:\t{this.CustomerAddress.ToString()}";
        /*public bool Equals(Customer value) {
            if (value == null) {
                return false;
            }
            return value.CustomerName == this.CustomerName && value.CustomerEmail == this.CustomerEmail && value.CustomerPhone == this.CustomerPhone;
        }*/
    }
}