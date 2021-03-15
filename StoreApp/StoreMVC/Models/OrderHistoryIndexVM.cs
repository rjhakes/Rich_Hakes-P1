using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class OrderHistoryIndexVM
    {
        public int Id { get; set; }
        [DisplayName("Location ID")]
        [Required]
        public int LocId { get; set; }
        [DisplayName("Customer ID")]
        [Required]
        public int CustId { get; set; }
        [DisplayName("Date")]
        [Required]
        public DateTime OrderDate { get; set; }
        [DisplayName("Order ID")]
        [Required]
        public int OrderId { get; set; }
        [DisplayName("Total")]
        [Required]
        [DataType(DataType.Currency)]
        public double Total { get; set; }
    }
}
