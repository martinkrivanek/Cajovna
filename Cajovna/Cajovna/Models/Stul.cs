using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cajovna.Models
{
    public class Stul
    {
        public int stulID { get; set; }

        public String name { get; set; }

        public virtual List<Ucet> ucty { get; set; }


        // CONSTRUCTORs
        public Stul()
        {
            ucty = new List<Ucet>();
        }
    }
}