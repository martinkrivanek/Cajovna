using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using Cajovna.Models;
using System.Text;

namespace Cajovna.Controllers
{
    public class UcetController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        const int items_on_page = 10;        

        public ActionResult Index(String sort, int page = 1)
        {
            ViewBag.totalItems = db.Ucty.Count();
            ViewBag.maxPage = (ViewBag.totalItems % items_on_page == 0) ? ViewBag.totalItems / items_on_page : ViewBag.totalItems / items_on_page + 1;
            ViewBag.page = page;
            ViewBag.sort = (String.IsNullOrWhiteSpace(sort)) ? "none" : sort;
            ViewBag.sortList = getUctySortList();
            return View(getUcty(page, sort));            
        }

        public ActionResult Detail(int id = 0)
        {
            Ucet ucet = db.Ucty.Find(id);
            if (ucet == null) return HttpNotFound();
            return View(ucet);
        }

        public ActionResult Add(int id = 0)
        {
            if (db.Stoly.Find(id) == null)
            {
                return HttpNotFound();
            }
            ViewBag.stulID = id;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Ucet ucet)
        {
            if (ModelState.IsValid)
            {
                db.Ucty.Add(ucet);
                db.SaveChanges();
                return RedirectToAction("Detail", "Stul", new { id = ucet.stulID });
            }
            ViewBag.stulID = ucet.stulID;
            return View(ucet);
        }

        public ActionResult Edit(int id = 0)
        {
            Ucet ucet = db.Ucty.Find(id);
            if (ucet == null) return HttpNotFound();
            return View(ucet);
        }

        [HttpPost]
        public ActionResult Edit(Ucet ucet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ucet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Detail", "Stul", new { id = ucet.stulID });
            }
            return View(ucet);
        }

        public ActionResult Delete(int id = 0)
        {
            Ucet ucet = db.Ucty.Find(id);
            if (ucet == null) return HttpNotFound();
            return View(ucet);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ucet ucet = db.Ucty.Find(id);
            if (ucet.polozkyUctu.Where(a => a.date_paid == null).Count() != 0)
            {
                ViewBag.errors = "Neleze smazat účet, na kterém jsou nezaplacené položky.";
                return View(ucet);
            }

            db.SaveChanges();
            return RedirectToAction("Detail", "Stul", new { id = ucet.stulID });
        }

        public ActionResult MoveUcet(int id = 0) // ucetID
        {
            Ucet ucet = db.Ucty.Find(id);
            if (ucet == null) return HttpNotFound();
            ViewBag.stoly = db.Stoly.OrderBy(a => a.name).ToList();
            return View(ucet);
        }

        [HttpPost]
        public ActionResult MoveUcet(Ucet ucet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ucet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Detail", "Stul", new { id = ucet.stulID });
            }
            return View(ucet);
        }

        public ActionResult Pay(int id = 0)
        {
            Ucet ucet = db.Ucty.Find(id);
            if (ucet == null) return HttpNotFound();
            return View(ucet);
        }

        [HttpPost]
        public ActionResult Pay(int[] polozkyUctuIDs, int ucetID)
        {
            Ucet ucet = db.Ucty.Find(ucetID);
            if (ucet == null) return HttpNotFound();

            foreach (int id in polozkyUctuIDs)
            {
                PolozkaUctu pu = db.PolozkyUctu.Find(id);
                pu.pay();
                db.Entry(pu).State = EntityState.Modified;
            }            
            db.SaveChanges();

            return RedirectToAction("Detail", "Ucet", new { id = ucetID });

        }

        // HELPERs
        /* creates a dictionary of values and text to define sort html select options*/
        private Dictionary<string, string> getUctySortList()
        {
            Dictionary<string, string> sortlist = new Dictionary<string, string>();
            sortlist.Add("a-z", "A -> Z");
            sortlist.Add("z-a", "Z -> A");
            sortlist.Add("time-old-new", "od nejstaršího");
            sortlist.Add("time-new-old", "od nejnovějšího");
            return sortlist;
        }

        /* returns list of PolozkaMenu objects with paging and sorted accordingly */
        private List<Ucet> getUcty(int page, String sort)
        {
            List<Ucet> ucty = db.Ucty.ToList();
            switch (sort)
            {
                case "a-z": ucty = ucty.OrderBy(a => a.name).ToList(); break;
                case "z-a": ucty = ucty.OrderByDescending(a => a.name).ToList(); break;
                case "time-old-new": ucty = ucty.OrderBy(a => a.date_added).ToList(); break;
                case "time-new-old": ucty = ucty.OrderByDescending(a => a.date_added).ToList(); break;
            }
            ucty = ucty.Skip((page - 1) * items_on_page).Take(items_on_page).ToList();
            return ucty;
        }

	}
}