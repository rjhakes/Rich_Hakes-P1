using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class CustomerCRVM
    {
        [DisplayName("Name")]
        [Required]
        public string CustomerName { get; set; }
        [DisplayName("Email")]
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }
        [DisplayName("Password")]
        [Required]
        [PasswordPropertyText]
        public string CustomerPasswordHash { get; set; }
        
        [DisplayName("Phone")]
        [Required]
        [Phone]
        public string CustomerPhone { get; set; }
        [DisplayName("Address")]
        [Required]
        public string CustomerAddress { get; set; }

    }
}
