using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PosCookents.Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public BoughtItemAdmin BoughtItemAdmin { get; set; }
        public int BoughtItemAdminId { get; set; }
    }
}