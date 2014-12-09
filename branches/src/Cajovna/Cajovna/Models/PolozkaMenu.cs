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


        // CONSTRUCTORs
        public PolozkaMenu()
        {
            recipe = new List<Slozeni>();
            avalible = false;
            date_added = DateTime.Now;
        }


        // METHODs
        /* calculates and returns the total buying price of the materials */
        public double price_buy()
        {
            double total = 0;
            foreach (Slozeni item in recipe)
            {
                total += item.price();
            }
            return total;
        }

        /* creates and returns the string of the specific accesibility */
        public String getAccesibility()
        {
            // in the future, there will be check if the materials are in stock
            return (avalible) ? "v prodeji" : "neprodává se";
        }
    }
}