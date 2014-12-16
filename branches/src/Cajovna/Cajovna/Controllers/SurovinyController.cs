using Cajovna.DAO;
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
    /* Controller servicing actions of Surovina entity */
    public class SurovinyController : Controller
    {
        private SurovinyDAO surovinyDAO = new SurovinyDAOImpl();

        const int items_on_page = 3;

        /* Action which returns a view to show LIST of Surovina objects in order defined 
         * by the input parameter sort with proper paging defined by the input parameter page */
        public ActionResult Index(String sort, int page = 1)
        {
            ViewBag.totalItems = surovinyDAO.readAll().Count();
            ViewBag.maxPage = (ViewBag.totalItems % items_on_page == 0) ? ViewBag.totalItems / items_on_page : ViewBag.totalItems / items_on_page + 1;
            ViewBag.page = page;
            ViewBag.sort = (String.IsNullOrWhiteSpace(sort)) ? "none" : sort;
            ViewBag.sortList = getSurovinySortList();
            return View(getSuroviny(page, sort));
        }

        /* Action which returns a view to show DETAIL of Surovina object defined by the input parameter id */
        public ActionResult Detail(int id = 0)
        {
            Surovina surovina = surovinyDAO.read(id);
            if (surovina == null) return HttpNotFound();
            return View(surovina);
        }

        /* Get method action which returns a view to CREATE a Surovina*/
        public ActionResult Add()
        {
            return View();
        }

        /* Post method action which processes (CREATES) the Surovina object given back by the
         * submitted get method. In failure scenario it returns back the object and challenges
         * the user to make some necessary changes */
        [HttpPost]
        public ActionResult Add(Surovina surovina)
        {
            if (ModelState.IsValid)
            {
                surovinyDAO.create(surovina);
                return RedirectToAction("Index", "Suroviny");
            }
            return View(surovina);
        }

        /* Get method action which returns a view to EDIT a Surovina acordingly to the input id paremeter*/
        public ActionResult Edit(int id = 0)
        {
            Surovina surovina = surovinyDAO.read(id);
            if (surovina == null) return HttpNotFound();
            return View(surovina);
        }

        /* Post method action which processes (EDITS) the Surovina object given back by the
         * submitted get method. In failure scenario it returns back the object and challenges
         * the user to make some necessary changes */
        [HttpPost]
        public ActionResult Edit(Surovina surovina)
        {
            if (ModelState.IsValid)
            {
                surovinyDAO.update(surovina);
                return RedirectToAction("Index", "Suroviny");
            }
            return View(surovina);
        }

        /* Get method action which returns a view to DELETE a Surovina acordingly to the input id paremeter*/
        public ActionResult Delete(int id = 0)
        {
            Surovina surovina = surovinyDAO.read(id);
            if (surovina == null) return HttpNotFound();
            return View(surovina);
        }

        /* Post method action which processes (DELETES) the Surovina object which the input 
         * id parameter was given back by the submitted get method defines. In failure scenario it 
         * returns back the object and challenges the user to make some necessary changes */
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Surovina surovina = surovinyDAO.read(id);
            surovinyDAO.delete(surovina);
            return RedirectToAction("Index", "Suroviny");
        }


        // HELPERs
        /* creates a dictionary of values and text to define sort html select options*/
        private Dictionary<string, string> getSurovinySortList()
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
        private List<Surovina> getSuroviny(int page, String sort)
        {
            List<Surovina> suroviny = surovinyDAO.readAll();
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
            return suroviny;
        }
    }
}