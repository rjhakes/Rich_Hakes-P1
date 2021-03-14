using StoreModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class ProductIndexVM
    {
        [DisplayName("Product ID")]
        [Required]
        public int Id { get; set; }
        [DisplayName("Name")]
        [Required]
        public string ProdName { get; set; }
        [DisplayName("Price")]
        [Required]
        [DataType(DataType.Currency)]
        public double ProdPrice { get; set; }
        [DisplayName("Category")]
        [Required]
        public Category ProdCategory { get; set; }
        [DisplayName("Brand Name")]
        [Required]
        public string ProdBrandName { get; set; }
        [DisplayName("Description")]
        [Required]
        public string Description { get; set; }
    }
}
