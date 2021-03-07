using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using StoreModels;
using System.ComponentModel;

namespace StoreMVC.Models
{
    public class CustomerEditVM
    {
        [DisplayName("Name")]
        [Required]
        public string CustomerName { get; set; }
        [DisplayName("Email")]
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }
        /*[DisplayName("Password")]
        [Required]*/
        //[PasswordPropertyText]
        //public string CustomerPasswordHash { get; set; }

        [DisplayName("Phone")]
        [Required]
        [Phone]
        public string CustomerPhone { get; set; }
        [DisplayName("Address")]
        [Required]
        public string CustomerAddress { get; set; }

        public int CustomerId { get; set; }

    }
}
