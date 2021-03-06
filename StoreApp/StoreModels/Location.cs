using System.Collections.Generic;

namespace StoreModels
{
    /// <summary>
    /// This class should contain all the fields and properties that define a store location.
    /// </summary>
    public class Location
    {
        private string locAddress;
        private string locName;
        private string locPhone;

        public int Id { get; set; }

        public string LocAddress { get; set; }
        public string LocName { get; set; }
        public string LocPhone { get; set; }
        //public Inventory Inventory { get; set; }

        //TODO: add some property for the location inventory

        public override string ToString() => $"\tLocation Name:\t{this.LocName}\n\tLocation Phone:\t{this.LocPhone}\n\tAddress:\t{this.LocAddress.ToString()}\n";
    }
}