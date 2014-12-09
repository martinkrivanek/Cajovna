using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cajovna.Models
{

    public enum Unit
    {
        g, l, ks
    }


    public class Surovina
    {
        public int surovinaID { get; set; }

        [Required]
        public String name { get; set; }

        public String desription { get; set; }

        [Required]
        public DateTime date_added { get; set; }

        [Required]
        public Unit unit { get; set; }

        [Required]
        public double number_of_units { get; set; }

        [Required]
        public double price { get; set; }


        // CONSTRUCTORs
        public Surovina()
        {
            date_added = DateTime.Now;
        }
    }
}