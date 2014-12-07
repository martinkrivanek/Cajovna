using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cajovna.Models
{
    public class Slozeni
    {
        public int slozeniID { get; set; }

        [Required]
        public double quantity { get; set; }

        [Required]
        public int surovinaID { get; set; }
        public virtual Surovina surovina { get; set; }

        [Required]
        public int polozkaMenuID { get; set; }
        public virtual PolozkaMenu polozkaMenu { get; set; }


        // METHODs
        internal double price()
        {
            return quantity * surovina.price;
        }
    }
}