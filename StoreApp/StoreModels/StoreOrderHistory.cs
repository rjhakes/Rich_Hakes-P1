using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreModels
{
    public class StoreOrderHistory
    {
        public int Id { get; set; }
        public int LocId { get; set; }
        public int ManagerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderId { get; set; }
        public double Total { get; set; }

    }
}
