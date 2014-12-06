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
        public int quantity { get; set; }

        [Required]
        public int surovinaID { get; set; }
        public virtual Surovina surovina { get; set; }


        // METHODs
        internal double price()
        {
            return quantity * surovina.price;
        }
    }
}