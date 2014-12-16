using Cajovna.DAO;
using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cajovna.Controllers
{
    /* Controller servicing actions of PolozkaMenu entity */
    public class PolozkyMenuController : Controller
    {
        private PolozkyMenuDAO polMenuDAO = new PolozkyMenuDAOImpl();

        const int items_on_page = 2;

        /* Action which returns a view to show LIST of PolozkaMenu objects in order defined 
         * by the input parameter sort with proper paging defined by the input parameter page */
        public ActionResult Index(String sort, int page = 1)
        {
            ViewBag.totalItems = polMenuDAO.readAll().Count();
            ViewBag.maxPage = (ViewBag.totalItems % items_on_page == 0) ? ViewBag.totalItems / items_on_page : ViewBag.totalItems / items_on_page + 1;
            ViewBag.page = page;
            ViewBag.sort = (String.IsNullOrWhiteSpace(sort)) ? "none" : sort;
            ViewBag.sortList = getPolozkyMenuSortList();
            return View(getPolozkyMenu(page, sort));
        }

        /* Action which returns a view to show DETAIL of PolozkaMenu object defined by the input parameter id */
        public ActionResult Detail(int id = 0)
        {
            PolozkaMenu polozkaMenu = polMenuDAO.read(id);
            if (polozkaMenu == null) return HttpNotFound();
            return View(polozkaMenu);
        }

        /* Get method action which returns a view to CREATE a PolozkaMenu*/
        public ActionResult Add()
        {
            return View();
        }

        /* Post method action which processes (CREATES) the PolozkaMenu object given back by the
         * submitted get method. In failure scenario it returns back the object and challenges
         * the user to make some necessary changes */
        [HttpPost]
        public ActionResult Add(PolozkaMenu polozkaMenu)
        {
            if (ModelState.IsValid)
            {
                polMenuDAO.create(polozkaMenu);
                return RedirectToAction("Index", "PolozkyMenu");
            }
            return View(polozkaMenu);
        }

        /* Get method action which returns a view to EDIT a PolozkaMenu acordingly to the input id paremeter*/
        public ActionResult Edit(int id = 0)
        {
            PolozkaMenu polozkaMenu = polMenuDAO.read(id);
            if (polozkaMenu == null) return HttpNotFound();
            ViewBag.price_buy = polozkaMenu.price_buy();
            return View(polozkaMenu);
        }

        /* Post method action which processes (EDITS) the PolozkaMenu object given back by the
         * submitted get method. In failure scenario it returns back the object and challenges
         * the user to make some necessary changes */
        [HttpPost]
        public ActionResult Edit(PolozkaMenu polozkaMenu, double price_buy = 0)
        {
            if (ModelState.IsValid && myPolozkaMenuValidation(polozkaMenu, price_buy))
            {
                polMenuDAO.update(polozkaMenu);
                return RedirectToAction("Index", "PolozkyMenu");
            }
            ViewBag.price_buy = price_buy;
            ViewBag.errors = "Prodejní cena musí být vyšší než nákupní. Prodejní cena musí také být definována aby mohl být stav \"k prodeji\". Položka musí mít definovanou alespoň nějaké složení (nákupní cena se pak nerovná 0)";
            return View(polozkaMenu);
        }

        /* Get method action which returns a view to DELETE a PolozkaMenu acordingly to the input id paremeter*/
        public ActionResult Delete(int id = 0)
        {
            PolozkaMenu polozkaMenu = polMenuDAO.read(id);
            if (polozkaMenu == null) return HttpNotFound();
            return View(polozkaMenu);
        }

        /* Post method action which processes (DELETES) the PolozkaMenu object which the input 
         * id parameter was given back by the submitted get method defines. In failure scenario it 
         * returns back the object and challenges the user to make some necessary changes */
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PolozkaMenu polozkaMenu = polMenuDAO.read(id);
            polMenuDAO.delete(polozkaMenu);
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
            List<PolozkaMenu> polozkyMenu = polMenuDAO.readAll();
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