using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using Cajovna.Models;
using System.Text;
using Cajovna.DAO;

namespace Cajovna.Controllers
{
    /* Controller servicing actions of Ucet entity */
    public class UcetController : Controller
    {
        private UcetDAO ucetDAO = new UcetDAOImpl();
        private StulDAO stulDAO = new StulDAOImpl();
        private PolozkaUctuDAO polUctuDAO = new PolozkaUctuDAOImpl();

        const int items_on_page = 10;

        /* Action which returns a view to show LIST of Ucet objects in order defined 
         * by the input parameter sort with proper paging defined by the input parameter page */
        public ActionResult Index(String sort, int page = 1)
        {
            ViewBag.totalItems = ucetDAO.readAll().Count();
            ViewBag.maxPage = (ViewBag.totalItems % items_on_page == 0) ? ViewBag.totalItems / items_on_page : ViewBag.totalItems / items_on_page + 1;
            ViewBag.page = page;
            ViewBag.sort = (String.IsNullOrWhiteSpace(sort)) ? "none" : sort;
            ViewBag.sortList = getUctySortList();
            return View(getUcty(page, sort));            
        }

        /* Action which returns a view to show DETAIL of Ucet object defined by the input parameter id */
        public ActionResult Detail(int id = 0)
        {
            Ucet ucet = ucetDAO.read(id);
            if (ucet == null) return HttpNotFound();
            return View(ucet);
        }

        /* Get method action which returns a view to CREATE a Ucet to the Stul defined by the input id parameter*/
        public ActionResult Add(int id = 0)
        {
            if (stulDAO.read(id) == null)
            {
                return HttpNotFound();
            }
            ViewBag.stulID = id;
            return View();
        }

        /* Post method action which processes (CREATES) the PolozkaMenu object given back by the
         * submitted get method. In failure scenario it returns back the object and challenges
         * the user to make some necessary changes */
        [HttpPost]
        public ActionResult Add(Ucet ucet)
        {
            if (ModelState.IsValid)
            {
                ucetDAO.create(ucet);
                return RedirectToAction("Detail", "Stul", new { id = ucet.stulID });
            }
            ViewBag.stulID = ucet.stulID;
            return View(ucet);
        }

        /* Get method action which returns a view to EDIT a Ucet acordingly to the input id paremeter*/
        public ActionResult Edit(int id = 0)
        {
            Ucet ucet = ucetDAO.read(id);
            if (ucet == null) return HttpNotFound();
            return View(ucet);
        }

        /* Post method action which processes (EDITS) the Ucet object given back by the
         * submitted get method. In failure scenario it returns back the object and challenges
         * the user to make some necessary changes */
        [HttpPost]
        public ActionResult Edit(Ucet ucet)
        {
            if (ModelState.IsValid)
            {
                ucetDAO.update(ucet);
                return RedirectToAction("Detail", "Stul", new { id = ucet.stulID });
            }
            return View(ucet);
        }

        /* Get method action which returns a view to DELETE a Ucet acordingly to the input id paremeter*/
        public ActionResult Delete(int id = 0)
        {
            Ucet ucet = ucetDAO.read(id);
            if (ucet == null) return HttpNotFound();
            return View(ucet);
        }

        /* Post method action which processes (DELETES) the Ucet object which the input 
         * id parameter was given back by the submitted get method defines. In failure scenario it 
         * returns back the object and challenges the user to make some necessary changes */
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ucet ucet = ucetDAO.read(id);
            if (ucet.polozkyUctu.Where(a => a.date_paid == null).Count() != 0)
            {
                ViewBag.errors = "Neleze smazat účet, na kterém jsou nezaplacené položky.";
                return View(ucet);
            }
            ucet.date_closed = DateTime.Now;
            ucetDAO.update(ucet);
            return RedirectToAction("Detail", "Stul", new { id = ucet.stulID });
        }

        /* Get method action which returns a view to MOVE a Ucet defined by the input id parameter to a different Stul */
        public ActionResult MoveUcet(int id = 0) // ucetID
        {
            Ucet ucet = ucetDAO.read(id);
            if (ucet == null) return HttpNotFound();
            ViewBag.stoly = stulDAO.readAll().OrderBy(a => a.name).ToList();
            return View(ucet);
        }

        /* Post method action which processes (EDITES) the Ucet object which the input 
         * id parameter was given back by the submitted get method defines. In failure scenario it 
         * returns back the object and challenges the user to make some necessary changes */
        [HttpPost]
        public ActionResult MoveUcet(Ucet ucet)
        {
            if (ModelState.IsValid)
            {
                ucetDAO.update(ucet);
                return RedirectToAction("Detail", "Stul", new { id = ucet.stulID });
            }
            return View(ucet);
        }

        /* Get method action which returns a view to PAY some PolozkaUcet objects of an Ucet defined by the input id parameter*/
        public ActionResult Pay(int id = 0)
        {
            Ucet ucet = ucetDAO.read(id);
            if (ucet == null) return HttpNotFound();
            return View(ucet);
        }
        
        /* Post method action which processes (PAYS) the PolozkaUctu objects which the input 
         * ids parameters were given back by the submitted get method defines. In failure scenario it 
         * returns back the object and challenges the user to make some necessary changes */
        [HttpPost]
        public ActionResult Pay(int[] polozkyUctuIDs, int ucetID)
        {
            Ucet ucet = ucetDAO.read(ucetID);
            if (ucet == null) return HttpNotFound();
            foreach (int id in polozkyUctuIDs)
            {
                PolozkaUctu pu = polUctuDAO.read(id);
                pu.pay();
                polUctuDAO.update(pu);
            }
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
            List<Ucet> ucty = ucetDAO.readAll();
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