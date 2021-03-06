using System.Collections.Generic;
using System;

namespace StoreModels
{
    /// <summary>
    /// This class should contain all the fields and properties that define a customer order. 
    /// </summary>
    public class Order
    {
        private string date;
        private Customer customer;
        private string locationName;
        private Address locationAddress;
        private List<Item> cart;
        private double total;


        public string Date { get; set; }
        public Customer Customer { get; set; }
        public string LocationName { get; set; }

        public Address LocationAddress { get; set; }

        public List<Item> Cart { get; set; }
        public double Total { get; set; }

        public override string ToString() => $"\n\tDate:\t\t\t{this.Date}\n\tLocation Name\t\t{this.LocationName}\n\tLocation Address--\t{this.LocationAddress}"; //\n\tCart:\t{this.Cart}\n\tTotal:\t{this.Total}";
    }
}