using StoreBL;
using StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class Mapper : IMapper
    {
        public CustomerIndexVM cast2CustomerIndexVM(Customer customer2BCasted)
        {
            return new CustomerIndexVM
            {
                CustomerName = customer2BCasted.CustomerName,
                CustomerEmail = customer2BCasted.CustomerEmail,
                CustomerPhone = customer2BCasted.CustomerPhone,
                CustomerAddress = customer2BCasted.CustomerAddress
            };
        }

        public Customer cast2Customer(CustomerCRVM customer2BCasted)
        {
            return new Customer
            {
                CustomerName = customer2BCasted.CustomerName,
                CustomerEmail = customer2BCasted.CustomerEmail,
                CustomerPasswordHash = Convert.ToBase64String(new StoreBL.PasswordHash(customer2BCasted.CustomerPasswordHash).ToArray()),
                CustomerPhone = customer2BCasted.CustomerPhone,
                CustomerAddress = customer2BCasted.CustomerAddress
            };
        }
        public CustomerCRVM cast2CustomerCRVM(Customer customer)
        {
            return new CustomerCRVM
            {
                CustomerName = customer.CustomerName,
                CustomerEmail = customer.CustomerEmail,
                CustomerPhone = customer.CustomerPhone,
                CustomerAddress = customer.CustomerAddress
            };
        }
        public CustomerEditVM cast2CustomerEditVM(Customer customer)
        {
            return new CustomerEditVM
            {
                CustomerName = customer.CustomerName,
                CustomerEmail = customer.CustomerEmail,
                CustomerPasswordHash = "",
                CustomerPhone = customer.CustomerPhone,
                CustomerAddress = customer.CustomerAddress,
                CustomerId = customer.Id
            };
        }
        public Customer cast2Customer(CustomerEditVM customer2BCasted)
        {

            return new Customer
            {
                Id = customer2BCasted.CustomerId,
                CustomerName = customer2BCasted.CustomerName,
                CustomerEmail = customer2BCasted.CustomerEmail,
                CustomerPasswordHash = Convert.ToBase64String(new StoreBL.PasswordHash(customer2BCasted.CustomerPasswordHash).ToArray()),
                CustomerPhone = customer2BCasted.CustomerPhone,
                CustomerAddress = customer2BCasted.CustomerAddress
            };
        }

        public bool verifyPW(string pwHash, string newPW)
        {
            byte[] hashBytes = Convert.FromBase64String(pwHash);
            PasswordHash PasswordHash = new PasswordHash(hashBytes);
            //Customer _customer = _customerBL.GetCustomerByEmail(userEmail);
            return PasswordHash.Verify(newPW);
        }

        public Manager cast2Manager(ManagerCRVM manager2BCasted)
        {
            return new Manager
            {
                ManagerName = manager2BCasted.ManagerName,
                ManagerEmail = manager2BCasted.ManagerEmail,
                ManagerPasswordHash = Convert.ToBase64String(new StoreBL.PasswordHash(manager2BCasted.ManagerPasswordHash).ToArray()),
                ManagerPhone = manager2BCasted.ManagerPhone,
                ManagerLocId= manager2BCasted.ManagerLocId
            };
        }

        public ManagerIndexVM cast2ManagerIndexVM(Manager manager2BCasted)
        {
            return new ManagerIndexVM
            {
                ManagerName = manager2BCasted.ManagerName,
                ManagerEmail = manager2BCasted.ManagerEmail,
                ManagerPhone = manager2BCasted.ManagerPhone,
                ManagerLocId = manager2BCasted.ManagerLocId
            };
        }

        public ManagerCRVM cast2ManagerCRVM(Manager manager)
        {
            return new ManagerCRVM
            {
                ManagerName = manager.ManagerName,
                ManagerEmail = manager.ManagerEmail,
                ManagerPhone = manager.ManagerPhone,
                ManagerLocId = manager.ManagerLocId
            };
        }

        public ManagerEditVM cast2ManagerEditVM(Manager manager)
        {
            return new ManagerEditVM
            {
                ManagerName = manager.ManagerName,
                ManagerEmail = manager.ManagerEmail,
                ManagerPasswordHash = "",
                ManagerPhone = manager.ManagerPhone,
                ManagerLocId = manager.ManagerLocId,
                ManagerId = manager.Id
            };
        }

        public Manager cast2Manager(ManagerEditVM manager2BCasted)
        {
            return new Manager
            {
                Id = manager2BCasted.ManagerId,
                ManagerName = manager2BCasted.ManagerName,
                ManagerEmail = manager2BCasted.ManagerEmail,
                ManagerPasswordHash = Convert.ToBase64String(new StoreBL.PasswordHash(manager2BCasted.ManagerPasswordHash).ToArray()),
                ManagerPhone = manager2BCasted.ManagerPhone,
                ManagerLocId = manager2BCasted.ManagerLocId
            };
        }

        public Location cast2Location(LocCRVM location2BCasted)
        {
            return new Location
            {
                LocName = location2BCasted.LocName,
                LocAddress = location2BCasted.LocAddress,
                LocPhone = location2BCasted.LocPhone,
            };
        }

        public LocIndexVM cast2LocationIndexVM(Location location2BCasted)
        {
            return new LocIndexVM
            {
                LocName = location2BCasted.LocName,
                LocAddress = location2BCasted.LocAddress,
                LocPhone = location2BCasted.LocPhone,
                LocId = location2BCasted.Id
            };
        }

        public LocCRVM cast2LocationCRVM(Location location)
        {
            return new LocCRVM
            {
                LocName = location.LocName,
                LocAddress = location.LocAddress,
                LocPhone = location.LocPhone,
            };
        }

        public LocEditVM cast2LocationEditVM(Location location)
        {
            return new LocEditVM
            {
                LocName = location.LocName,
                LocAddress = location.LocAddress,
                LocPhone = location.LocPhone,
                LocId = location.Id
            };
        }

        public Location cast2Location(LocEditVM location2BCasted)
        {
            return new Location
            {
                Id = location2BCasted.LocId,
                LocName = location2BCasted.LocName,
                LocAddress = location2BCasted.LocAddress,
                LocPhone = location2BCasted.LocPhone,
            };
        }

        public Product cast2Product(ProductCRVM product2BCasted)
        {
            return new Product
            {
                ProdName = product2BCasted.ProdName,
                ProdPrice = product2BCasted.ProdPrice,
                ProdCategory = product2BCasted.ProdCategory,
                ProdBrandName = product2BCasted.ProdBrandName,
                Description = product2BCasted.Description,
            };
        }

        public ProductIndexVM cast2ProductIndexVM(Product product2BCasted)
        {
            return new ProductIndexVM
            {
                ProdName = product2BCasted.ProdName,
                ProdPrice = product2BCasted.ProdPrice,
                ProdCategory = product2BCasted.ProdCategory,
                ProdBrandName = product2BCasted.ProdBrandName,
                Description = product2BCasted.Description,
                Id = product2BCasted.Id
            };
        }

        public ProductCRVM cast2ProductCRVM(Product product)
        {
            return new ProductCRVM
            {
                ProdName = product.ProdName,
                ProdPrice = product.ProdPrice,
                ProdCategory = product.ProdCategory,
                ProdBrandName = product.ProdBrandName,
                Description = product.Description,
            };
        }

        public ProductEditVM cast2ProductEditVM(Product product)
        {
            return new ProductEditVM
            {
                ProdName = product.ProdName,
                ProdPrice = product.ProdPrice,
                ProdCategory = product.ProdCategory,
                ProdBrandName = product.ProdBrandName,
                Description = product.Description,
                ProdId = product.Id
            };
        }

        public Product cast2Product(ProductEditVM product2BCasted)
        {
            return new Product
            {
                Id = product2BCasted.ProdId,
                ProdName = product2BCasted.ProdName,
                ProdPrice = product2BCasted.ProdPrice,
                ProdCategory = product2BCasted.ProdCategory,
                ProdBrandName = product2BCasted.ProdBrandName,
                Description = product2BCasted.Description,
            };
        }

        public InventoryLineItem cast2InventoryLineItem(InvLineItemCRVM inventoryLineItem2BCasted)
        {
            return new InventoryLineItem
            {
                InventoryId = inventoryLineItem2BCasted.InventoryId,
                ProductId = inventoryLineItem2BCasted.ProductId,
                Quantity = inventoryLineItem2BCasted.Quantity,
            };
        }

        public InvLineItemIndexVM cast2InventoryLineItemIndexVM(InventoryLineItem inventoryLineItem2BCasted, Product product)
        {
            return new InvLineItemIndexVM
            {
                InventoryId = inventoryLineItem2BCasted.InventoryId,
                ProductId = inventoryLineItem2BCasted.ProductId,
                ProdName = product.ProdName,
                ProdPrice = product.ProdPrice,
                ProdBrandName = product.ProdBrandName,
                Quantity = inventoryLineItem2BCasted.Quantity,
                Id = inventoryLineItem2BCasted.Id
            };
        }

        public InvLineItemCRVM cast2InventoryLineItemCRVM(InventoryLineItem inventoryLineItem)
        {
            return new InvLineItemCRVM
            {
                ProductId = inventoryLineItem.ProductId,
                Quantity = inventoryLineItem.Quantity,
            };
        }

        public InvLineItemEditVM cast2InventoryLineItemEditVM(InventoryLineItem inventoryLineItem, Product product)
        {
            return new InvLineItemEditVM
            {
                InventoryId = inventoryLineItem.InventoryId,
                ProductId = inventoryLineItem.ProductId,
                ProdName = product.ProdName,
                ProdPrice = product.ProdPrice,
                Quantity = inventoryLineItem.Quantity,
                Id = inventoryLineItem.Id
            };
        }

        public InventoryLineItem cast2InventoryLineItem(InvLineItemEditVM inventoryLineItem2BCasted)
        {
            return new InventoryLineItem
            {
                InventoryId = inventoryLineItem2BCasted.InventoryId,
                ProductId = inventoryLineItem2BCasted.ProductId,
                Quantity = inventoryLineItem2BCasted.Quantity,
                Id = inventoryLineItem2BCasted.Id
            };
        }
    }
}
