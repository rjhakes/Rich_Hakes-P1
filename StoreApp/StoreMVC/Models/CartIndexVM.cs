using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class CartIndexVM
    {
        [DisplayName("Item")]
        [Required]
        public string ProdName { get; set; }
        [DisplayName("Quantity")]
        [Required]
        public int Quantity { get; set; }
        [DisplayName("Price per Unit")]
        [Required]
        [DataType(DataType.Currency)]
        public double ProdPrice { get; set; }
        [DisplayName("Total")]
        [Required]
        [DataType(DataType.Currency)]
        public double Total { get; set; }
        public int Id { get; set; }
    }
}
