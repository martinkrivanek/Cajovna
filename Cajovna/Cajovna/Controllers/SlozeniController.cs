using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cajovna.Controllers
{
    /* Controller servicing actions of Slozeni entity */
    public class SlozeniController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        /* Get method action which returns a view to CREATE a Slozeni of an PolozkaMenu 
         * object defined by the input ID parameter */
        public ActionResult Add(int id = 0) // = ID polozkyMenu
        {
            PolozkaMenu polozkaMenu = db.PolozkyMenu.Find(id);
            if (polozkaMenu == null) return HttpNotFound();
            ViewBag.polozkaMenuID = id;
            ViewBag.suroviny = getListAddableMaterials(id);
            return View();
        }

        /* Post method action which processes (CREATES) the Slozeni object given back by the
         * submitted get method. In failure scenario it returns back the object and challenges
         * the user to make some necessary changes */
        [HttpPost]
        public ActionResult Add(Slozeni slozeni)
        {
            if (ModelState.IsValid && slozeni.quantity > 0)
            {
                db.Slozeni.Add(slozeni);
                PolozkaMenu polozkaMenu = db.PolozkyMenu.Find(slozeni.polozkaMenuID);
                polozkaMenu.avalible = false;
                db.Entry(polozkaMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Detail", "PolozkyMenu", new { id = slozeni.polozkaMenuID });
            }
            ViewBag.polozkaMenuID = slozeni.polozkaMenuID;
            ViewBag.suroviny = getListAddableMaterials(slozeni.polozkaMenuID);
            ViewBag.errors = "Množství musí být větší než 0";
            return View(slozeni);
        }

        /* Get method action which returns a view to EDIT a Slozeni acordingly to the input id paremeter*/
        public ActionResult Edit(int id = 0)
        {
            Slozeni slozeni = db.Slozeni.Find(id);
            if (slozeni == null) return HttpNotFound();
            ViewBag.suroviny = getListEditableMaterials(slozeni.polozkaMenuID, slozeni.surovinaID);
            return View(slozeni);
        }

        /* Post method action which processes (EDITS) the Slozeni object given back by the
         * submitted get method. In failure scenario it returns back the object and challenges
         * the user to make some necessary changes */
        [HttpPost]
        public ActionResult Edit(Slozeni slozeni)
        {
            if (ModelState.IsValid & slozeni.quantity > 0)
            {
                db.Entry(slozeni).State = EntityState.Modified;
                PolozkaMenu polozkaMenu = db.PolozkyMenu.Find(slozeni.polozkaMenuID);
                polozkaMenu.avalible = false;
                db.Entry(polozkaMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Detail", "PolozkyMenu", new { id = slozeni.polozkaMenuID });
            }
            ViewBag.errors = "Množství musí být větší než 0";
            ViewBag.suroviny = getListEditableMaterials(slozeni.polozkaMenuID, slozeni.surovinaID);
            return View(slozeni);
        }

        /* Get method action which returns a view to DELETE a Slozeni acordingly to the input id paremeter*/
        public ActionResult Delete(int id = 0)
        {
            Slozeni slozeni = db.Slozeni.Find(id);
            if (slozeni == null) return HttpNotFound();
            return View(slozeni);
        }

        /* Post method action which processes (DELETES) the Slozeni object which the input id parameter was given back by the
         * submitted get method defines. In failure scenario it returns back the object and challenges
         * the user to make some necessary changes */
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Slozeni slozeni = db.Slozeni.Find(id);
            db.Slozeni.Remove(slozeni);
            db.SaveChanges();
            return RedirectToAction("Detail", "PolozkyMenu", new { id = slozeni.polozkaMenuID });
        }


        // HELPERs
        /* creates a list of Surovina, which are still addable to the PolozkaMenu 
         * entity because the duplicities there are forbidden */
        private List<Surovina> getListAddableMaterials(int pmID)
        {
            List<Surovina> result = db.Suroviny.OrderBy(a => a.name).ToList();
            foreach (Slozeni sl in db.PolozkyMenu.Find(pmID).recipe)
            {
                result.Remove(sl.surovina);
            }
            return result;
        }

        /* creates a list of Surovina, which are still editable to the PolozkaMenu
         * entity because the duplicities there are forbidden */
        private List<Surovina> getListEditableMaterials(int pmID, int surID)
        {
            List<Surovina> result = getListAddableMaterials(pmID);
            result.Add(db.Suroviny.Find(surID));
            return result;
        }
    }
}