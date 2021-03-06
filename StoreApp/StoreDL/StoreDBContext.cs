using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreModels;

namespace StoreDL
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext(DbContextOptions options) : base(options)
        {
        }

        protected StoreDBContext()
        {
        }

        public DbSet<StoreModels.Customer> Customers{ get; set; }
        public DbSet<StoreModels.CustomerCart> CustomerCarts { get; set; }
        public DbSet<StoreModels.ManagerCart> ManagerCarts { get; set; }
        public DbSet<StoreModels.CustomerOrderHistory> CustomerOrderHistories { get; set; }
        public DbSet<StoreModels.StoreOrderHistory> StoreOrderHistories { get; set; }
        public DbSet<StoreModels.CustomerOrderLineItem> CustomerOrderLineItems { get; set; }
        public DbSet<StoreModels.StoreOrderLineItem> StoreOrderLineItems { get; set; }
        public DbSet<StoreModels.Location> Locations { get; set; }
        public DbSet<StoreModels.Manager> Managers { get; set; }
        public DbSet<StoreModels.Product> Products { get; set; }
        public DbSet<StoreModels.InventoryLineItem> InventoryLineItems { get; set; }

    }
}
