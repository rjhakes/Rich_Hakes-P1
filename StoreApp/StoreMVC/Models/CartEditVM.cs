using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class CartEditVM
    {
        [DisplayName("Customer ID")]
        [Required]
        public int CustId { get; set; }
        [DisplayName("Location ID")]
        [Required]
        public int LocId { get; set; }
        [DisplayName("Order ID")]
        [Required]
        public int CurrentItemsId { get; set; }
        public int Id { get; set; }
    }
}
