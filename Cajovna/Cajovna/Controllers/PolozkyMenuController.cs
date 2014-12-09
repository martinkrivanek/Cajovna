using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cajovna.Controllers
{
    public class PolozkyMenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        const int items_on_page = 2;

        public ActionResult Index(String sort, int page = 1)
        {
            ViewBag.totalItems = db.PolozkyMenu.Count();
            ViewBag.maxPage = (ViewBag.totalItems % items_on_page == 0) ? ViewBag.totalItems / items_on_page : ViewBag.totalItems / items_on_page + 1;
            ViewBag.page = page;
            ViewBag.sort = (String.IsNullOrWhiteSpace(sort)) ? "none" : sort;
            ViewBag.sortList = getPolozkyMenuSortList();
            return View(getPolozkyMenu(page, sort));
        }

        public ActionResult Detail(int id = 0)
        {
            PolozkaMenu polozkaMenu = db.PolozkyMenu.Find(id);
            if (polozkaMenu == null) return HttpNotFound();
            return View(polozkaMenu);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(PolozkaMenu polozkaMenu)
        {
            if (ModelState.IsValid)
            {
                db.PolozkyMenu.Add(polozkaMenu);
                db.SaveChanges();
                return RedirectToAction("Index", "PolozkyMenu");
            }
            return View(polozkaMenu);
        }

        public ActionResult Edit(int id = 0)
        {
            PolozkaMenu polozkaMenu = db.PolozkyMenu.Find(id);
            if (polozkaMenu == null) return HttpNotFound();
            ViewBag.price_buy = polozkaMenu.price_buy();
            return View(polozkaMenu);
        }

        [HttpPost]
        public ActionResult Edit(PolozkaMenu polozkaMenu, double price_buy = 0)
        {
            if (ModelState.IsValid && myPolozkaMenuValidation(polozkaMenu, price_buy))
            {
                db.Entry(polozkaMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "PolozkyMenu");
            }
            ViewBag.price_buy = price_buy;
            ViewBag.errors = "Prodejní cena musí být vyšší než nákupní. Prodejní cena musí také být definována aby mohl být stav \"k prodeji\". Položka musí mít definovanou alespoň nějaké složení (nákupní cena se pak nerovná 0)";
            return View(polozkaMenu);
        }

        public ActionResult Delete(int id = 0)
        {
            PolozkaMenu polozkaMenu = db.PolozkyMenu.Find(id);
            if (polozkaMenu == null) return HttpNotFound();
            return View(polozkaMenu);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PolozkaMenu polozkaMenu = db.PolozkyMenu.Find(id);
            db.PolozkyMenu.Remove(polozkaMenu);
            db.SaveChanges();
            return RedirectToAction("Index", "PolozkyMenu");
        }


        // HELPERs
        /* creates a dictionary of values and text to define sort html select options*/
        private Dictionary<string, string> getPolozkyMenuSortList()
        {
            Dictionary<string, string> sortlist = new Dictionary<string, string>();
            sortlist.Add("a-z", "A -> Z");
            sortlist.Add("z-a", "Z -> A");
            sortlist.Add("price-0-9", "od nejlevnější");
            sortlist.Add("price-9-0", "od nejdražší");
            sortlist.Add("time-old-new", "od nejstaršího");
            sortlist.Add("time-new-old", "od nejnovějšího");
            return sortlist;
        }

        /* returns list of PolozkaMenu objects with paging and sorted accordingly */
        private List<PolozkaMenu> getPolozkyMenu(int page, String sort)
        {
            List<PolozkaMenu> polozkyMenu = db.PolozkyMenu.ToList();
            switch (sort)
            {
                case "a-z": polozkyMenu = polozkyMenu.OrderBy(a => a.name).ToList(); break;
                case "z-a": polozkyMenu = polozkyMenu.OrderByDescending(a => a.name).ToList(); break;
                case "price-9-0": polozkyMenu = polozkyMenu.OrderByDescending(a => a.price_sell).ToList(); break;
                case "price-0-9": polozkyMenu = polozkyMenu.OrderBy(a => a.price_sell).ToList(); break;
                case "time-old-new": polozkyMenu = polozkyMenu.OrderBy(a => a.date_added).ToList(); break;
                case "time-new-old": polozkyMenu = polozkyMenu.OrderByDescending(a => a.date_added).ToList(); break;
            }
            polozkyMenu = polozkyMenu.Skip((page - 1) * items_on_page).Take(items_on_page).ToList();
            return polozkyMenu;
        }

        /* method to define additional validation next to the one defined by anotations at the model */
        private bool myPolozkaMenuValidation(PolozkaMenu polozkaMenu, double price_buy)
        {
            if (polozkaMenu.price_sell < price_buy) return false;
            if (price_buy == 0 && polozkaMenu.avalible) return false;
            //another check for example if the materials are in stock
            return true;
        }
    }
}