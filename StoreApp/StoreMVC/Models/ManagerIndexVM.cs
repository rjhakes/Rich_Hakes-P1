using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class ManagerIndexVM
    {
        [DisplayName("Name")]
        [Required]
        public string ManagerName { get; set; }
        [DisplayName("Email")]
        [Required]
        [EmailAddress]
        public string ManagerEmail { get; set; }
        [DisplayName("Phone")]
        [Required]
        [Phone]
        public string ManagerPhone { get; set; }
        [DisplayName("Location ID")]
        [Required]
        public int ManagerLocId { get; set; }
    }
}
