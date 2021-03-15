using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class LocIndexVM
    {
        [DisplayName("Name")]
        [Required]
        public string LocName { get; set; }
        [DisplayName("Address")]
        [Required]
        public string LocAddress { get; set; }
        [DisplayName("Phone")]
        [Required]
        [Phone]
        public string LocPhone { get; set; }

        public int LocId { get; set; }
    }
}
