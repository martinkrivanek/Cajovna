using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cajovna.Models
{
    public class PolozkaUctu
    {
        public int polozkaUctuID { get; set; }

        [Required]
        public DateTime date_ordered { get; set; }

        public DateTime? date_paid { get; set; }

        [Required]
        public int polozkaMenuID { get; set; }
        public virtual PolozkaMenu polozkaMenu { get; set; }

        [Required]
        public int ucetID { get; set; }
        public virtual Ucet ucet { get; set; }


        // CONSTRUCTORs
        public PolozkaUctu()
        {
            date_ordered = DateTime.Now;
        }


        // METHODs
        /* calculates the price of this object */ 
        public double price(){
            return polozkaMenu.price_sell;
        }
    }
}