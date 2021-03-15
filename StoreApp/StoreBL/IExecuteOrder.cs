using StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBL
{
    public interface IExecuteOrder
    {
        void AddItemToCart(Customer customer, InventoryLineItem item,
            int quantity);
    }
}
