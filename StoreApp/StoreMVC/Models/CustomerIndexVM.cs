using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class CustomerIndexVM
    {
        [DisplayName("Name")]
        public string CustomerName { get; set; }
        [DisplayName("Email")]
        public string CustomerEmail { get; set; }
        [DisplayName("Phone")]
        public string CustomerPhone { get; set; }
        [DisplayName("Address")]
        public string CustomerAddress { get; set; }

    }
}
