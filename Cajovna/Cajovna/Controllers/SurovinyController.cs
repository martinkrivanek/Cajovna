using Cajovna.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cajovna.Controllers
{
    public class SurovinyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        const int items_on_page = 2;

        public ActionResult Index(String sort, int page = 1)
        {
            List<Surovina> suroviny = db.Suroviny.ToList();
            ViewBag.totalItems = suroviny.Count;
            ViewBag.maxPage = (suroviny.Count % items_on_page == 0) ? suroviny.Count / items_on_page : suroviny.Count / items_on_page + 1;
            ViewBag.page = page;
            if (String.IsNullOrWhiteSpace(sort)) sort = "none";
            Dictionary<string, string> sortlist = new Dictionary<string, string>();
            sortlist.Add("a-z", "A -> Z");
            sortlist.Add("z-a", "Z -> A");
            sortlist.Add("price-0-9", "od nejlevnější");
            sortlist.Add("price-9-0", "od nejdražší");
            sortlist.Add("time-old-new", "od nejstaršího");
            sortlist.Add("time-new-old", "od nejnovějšího");
            ViewBag.sort = sort;
            ViewBag.sortList = sortlist;
            switch (sort)
            {
                case "a-z": suroviny = suroviny.OrderBy(a => a.name).ToList(); break;
                case "z-a": suroviny = suroviny.OrderByDescending(a => a.name).ToList(); break;
                case "price-9-0": suroviny = suroviny.OrderByDescending(a => a.price).ToList(); break;
                case "price-0-9": suroviny = suroviny.OrderBy(a => a.price).ToList(); break;
                case "time-old-new": suroviny = suroviny.OrderBy(a => a.date_added).ToList(); break;
                case "time-new-old": suroviny = suroviny.OrderByDescending(a => a.date_added).ToList(); break;
            }
            suroviny = suroviny.Skip((page - 1) * items_on_page).Take(items_on_page).ToList();
            return View(suroviny);
        }

        public ActionResult Detail(int id = 0)
        {
            Surovina surovina = db.Suroviny.Find(id);
            if (surovina == null) return HttpNotFound();
            return View(surovina);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Surovina surovina)
        {
            if (ModelState.IsValid)
            {
                db.Suroviny.Add(surovina);
                db.SaveChanges();
                return RedirectToAction("Index", "Suroviny");
            }
            return View(surovina);
        }

        public ActionResult Edit(int id = 0)
        {
            Surovina surovina = db.Suroviny.Find(id);
            if (surovina == null) return HttpNotFound();
            return View(surovina);
        }

        [HttpPost]
        public ActionResult Edit(Surovina surovina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surovina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Suroviny");
            }
            return View(surovina);
        }

        public ActionResult Delete(int id = 0)
        {
            Surovina surovina = db.Suroviny.Find(id);
            if (surovina == null) return HttpNotFound();
            return View(surovina);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Surovina surovina = db.Suroviny.Find(id);
            db.Suroviny.Remove(surovina);
            db.SaveChanges();
            return RedirectToAction("Index", "Suroviny");
        }
    }
}