using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cajovna.Models
{
    public class PolozkaMenu
    {
        public int polozkaMenuID { get; set; }

        public double price_sell { get; set; }

        [Required]
        public DateTime date_added { get; set; }

        [Required]
        public bool avalible { get; set; }

        [Required]
        public String name { get; set; }

        public String description { get; set; }

        public virtual List<Slozeni> recipe { get; set; }


        // CONTSTRUCTORs
        public PolozkaMenu()
        {
            recipe = new List<Slozeni>();
            avalible = false;
            date_added = DateTime.Now;
        }

        // METHODs
        public double price_buy()
        {
            double total = 0;
            foreach (Slozeni item in recipe)
            {
                total += item.price();
            }
            return total;
        }
    }
}