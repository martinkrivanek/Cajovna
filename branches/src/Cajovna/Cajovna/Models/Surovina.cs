using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cajovna.Models
{
    public class Surovina
    {
        public int surovinaID { get; set; }
        public String name { get; set; }
        public String desription { get; set; }
        public DateTime date_added { get; set; }
        public Unit unit { get; set; }
        public double number_of_units { get; set; }
        public double price { get; set; }

        public Surovina()
        {
            date_added = DateTime.Now;
        }
    }

    public enum Unit
    {
        g, 
        l,
        ks
    }
}