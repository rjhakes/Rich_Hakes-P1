using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public interface IInventoryLineItemBL
    {
        List<InventoryLineItem> GetInventoryLineItems();
        InventoryLineItem AddInventoryLineItem(InventoryLineItem newInventoryLineItem);
        InventoryLineItem GetInventoryLineItemById(int invId, int prodId);
        InventoryLineItem GetInventoryLineItemById(int invId);
        InventoryLineItem DeleteInventoryLineItem(InventoryLineItem inventoryLineItem2BDeleted);
        InventoryLineItem UpdateInventoryLineItem(InventoryLineItem inventoryLineItem2BUpdated);
    }
}