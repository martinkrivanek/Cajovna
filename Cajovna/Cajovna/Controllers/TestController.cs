using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Cajovna.Controllers
{
    public class TestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Test
        public String Index(int id = 1)
        {
            StringBuilder sb = new StringBuilder();
            // id uctu
            List<PolozkaUctu> pus = db.Ucty.Find(id).polozkyUctu.Where(a => a.date_paid == null).ToList();
            foreach (PolozkaUctu pu in pus)
            {
                sb.Append(pu.polozkaMenu.name + "<br>");
            }

            //foreach (Stul stul in db.Stoly.ToList())
            //{
            //    sb.Append("STUL - id: #" + stul.stulID + ".............");
            //    sb.Append("name: " + stul.name);
            //    sb.Append("<br>");
            //    foreach (Ucet ucet in stul.ucty)
            //    {
            //        sb.Append(".............");
            //        sb.Append("UCET - id: #" + ucet.ucetID + ".............");
            //        sb.Append("total price = " + ucet.price_total());
            //        sb.Append("<br>");
            //        foreach (PolozkaUctu polU in ucet.polozkyUctu)
            //        {
            //            sb.Append(".............");
            //            sb.Append(".............");
            //            sb.Append("POL-UCT - id: #" + polU.polozkaUctuID + ".............");
            //            sb.Append("product: " + polU.polozkaMenu.name + ".............");
            //            sb.Append("price = " + polU.price());
            //            sb.Append("<br>");
            //            foreach (Slozeni slo in polU.polozkaMenu.recipe)
            //            {
            //                sb.Append(".............");
            //                sb.Append(".............");
            //                sb.Append(".............");
            //                sb.Append("SUR - " + slo.quantity + "x .............");
            //                sb.Append("product #: " + slo.surovinaID + " - " + slo.surovina.name);
            //                sb.Append("<br>");
            //            }
            //        }
            //    }

            //}



            return sb.ToString();
        }
    }
}