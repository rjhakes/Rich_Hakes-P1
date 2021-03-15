using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class OrderItemVM
    {
        [DisplayName("Location Id")]
        [Required]
        public int InventoryId { get; set; }
        [DisplayName("Product Id")]
        [Required]
        public int ProductId { get; set; }
        [DisplayName("Name")]
        [Required]
        public string ProdName { get; set; }
        [DisplayName("Price")]
        [Required]
        [DataType(DataType.Currency)]
        public double ProdPrice { get; set; }
        [DisplayName("Brand Name")]
        [Required]
        public string ProdBrandName { get; set; }
        [DisplayName("Stock")]
        [Required]
        public int Quantity { get; set; }

        [DisplayName("Quantity")]
        [Required]
        public int Amount { get; set; }
        public int Id { get; set; }
    }
}
