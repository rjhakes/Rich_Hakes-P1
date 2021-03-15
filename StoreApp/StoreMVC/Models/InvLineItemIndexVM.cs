using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class InvLineItemIndexVM
    {
        /*[DisplayName("Location")]
        [Required]
        public string LocName { get; set; }*/
        [DisplayName("Location Id")]
        [Required]
        public int InventoryId { get; set; }
        [DisplayName("Product Id")]
        [Required]
        public int ProductId { get; set; }
        [DisplayName("Name")]
        [Required]
        public string ProdName{ get; set; }
        [DisplayName("Price")]
        [Required]
        [DataType(DataType.Currency)]
        public double ProdPrice{ get; set; }
        [DisplayName("Brand")]
        [Required]
        public string ProdBrandName{ get; set; }
        [DisplayName("Quantity")]
        [Required]
        public int Quantity { get; set; }

        public int Id { get; set; }
    }
}
