using System;
using System.Collections.Generic;
using StoreDL;
using StoreModels;

namespace StoreBL
{
    public class InventoryLineItemBL : IInventoryLineItemBL
    {
        private IInventoryLineItemRepository _repo;

        public InventoryLineItemBL(IInventoryLineItemRepository repo) {
            _repo = repo;
        }

        public InventoryLineItem AddInventoryLineItem(InventoryLineItem newInventoryLineItem)
        {
            //TODO: Add BL
            return _repo.AddInventoryLineItem(newInventoryLineItem);
        }
        public InventoryLineItem DeleteInventoryLineItem(InventoryLineItem inventoryLineItem2BDeleted)
        {
            return _repo.DeleteInventoryLineItem(inventoryLineItem2BDeleted);
        }
        public InventoryLineItem GetInventoryLineItemById(int invId, int prodId) {
            //todo validate
            return _repo.GetInventoryLineItemById(invId, prodId);
        }
        public InventoryLineItem GetInventoryLineItemById(int invId)
        {
            //todo validate
            return _repo.GetInventoryLineItemById(invId);
        }
        public List<InventoryLineItem> GetInventoryLineItems()
        {
            //TODO Add BL
            return _repo.GetInventoryLineItems();
        }
        public InventoryLineItem UpdateInventoryLineItem(InventoryLineItem inventoryLineItem2BUpdated)
        {
            return _repo.UpdateInventoryLineItem(inventoryLineItem2BUpdated);
        }

        
    }
}