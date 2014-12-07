using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cajovna.Models
{
    public class Ucet
    {
        public int ucetID { get; set; }

        public String name { get; set; }

        [Required]
        public DateTime date_added { get; set; }

        public DateTime? date_closed { get; set; }

        [Required]
        public int stulID { get; set; }
        public virtual Stul stul { get; set; }

        public virtual List<PolozkaUctu> polozkyUctu { get; set; }


        // CONTSTRUCTORs
        public Ucet()
        {
            date_added = DateTime.Now;
            polozkyUctu = new List<PolozkaUctu>();
        }


        // METHODs
        public double price_total(){
            double total = 0;
            foreach (PolozkaUctu item in polozkyUctu)
            {
                total += item.price();
            }
            return total;
        }
    }
}