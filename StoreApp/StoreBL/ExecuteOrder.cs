using StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBL
{
    public class ExecuteOrder : IExecuteOrder
    {
        // Add to CustomerOrderLineItem
        public void AddItemToCart(Customer customer, InventoryLineItem item, 
            int quantity)
        {
            CustomerOrderLineItem _coli = new CustomerOrderLineItem();
           
        }

        // 
    }
}
