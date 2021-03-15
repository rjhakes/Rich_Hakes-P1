using System.Collections.Generic;
using Model = StoreModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreModels;
namespace StoreDL
{
    public class InventoryLineItemRepoDB : IInventoryLineItemRepository
    {
        private readonly StoreDBContext _context;
        public InventoryLineItemRepoDB(StoreDBContext context)
        {
            _context = context;
        }
        public InventoryLineItem AddInventoryLineItem(InventoryLineItem newInventoryLineItem)
        {
            _context.InventoryLineItems.Add(newInventoryLineItem);
            _context.SaveChanges();
            return newInventoryLineItem;
        }

        public InventoryLineItem DeleteInventoryLineItem(InventoryLineItem inventoryLineItem2BDeleted)
        {
            _context.InventoryLineItems.Remove(inventoryLineItem2BDeleted);
            _context.SaveChanges();
            return inventoryLineItem2BDeleted;
        }

        public InventoryLineItem GetInventoryLineItemById(int invId, int prodId)
        {
            return _context.InventoryLineItems
                .FirstOrDefault(x => x.InventoryId == invId && x.ProductId == prodId);
        }

        public InventoryLineItem GetInventoryLineItemById(int invId)
        {
            return _context.InventoryLineItems
                .FirstOrDefault(x => x.Id == invId);
        }

        public List<InventoryLineItem> GetInventoryLineItems()
        {
            return _context.InventoryLineItems
                .Select(x => x)
                .ToList();
        }

        public InventoryLineItem UpdateInventoryLineItem(InventoryLineItem inventoryLineItem2BUpdated)
        {
            InventoryLineItem oldInventoryLineItem = _context.InventoryLineItems.Find(inventoryLineItem2BUpdated.Id);


            _context.Entry(oldInventoryLineItem).CurrentValues.SetValues(inventoryLineItem2BUpdated);

            _context.SaveChanges();

            //This method clears the change tracker to drop all tracked entities
            _context.ChangeTracker.Clear();
            return inventoryLineItem2BUpdated;

        }
    }
}