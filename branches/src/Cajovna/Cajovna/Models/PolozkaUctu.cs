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

        public DateTime date_paid { get; set; }

        [Required]
        public int polozkaMenuID { get; set; }
        public virtual PolozkaMenu polozkaMenu { get; set; }

        // CONTSTRUCTORs
        public PolozkaUctu()
        {
            date_ordered = DateTime.Now;
        }
    }
}