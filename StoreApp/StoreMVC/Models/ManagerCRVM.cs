using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class ManagerCRVM
    {
        [DisplayName("Name")]
        [Required]
        public string ManagerName { get; set; }
        [DisplayName("Email")]
        [Required]
        [EmailAddress]
        public string ManagerEmail { get; set; }
        [Category("Security")]
        [DisplayName("Password")]
        [Required]
        //[PasswordPropertyText(true)]
        [DataType(DataType.Password)]
        public string ManagerPasswordHash { get; set; }
        /*[DisplayName("Confirm Password")]
        [Required]
        //[PasswordPropertyText(true)]
        [DataType(DataType.Password)]
        [Compare("CustomerPasswordHash")]
        public string ConfirmPassword { get; set; }*/
        [DisplayName("Phone")]
        [Required]
        [Phone]
        public string ManagerPhone { get; set; }
        [DisplayName("Location ID")]
        [Required]
        public int ManagerLocId { get; set; }
    }
}
