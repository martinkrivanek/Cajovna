using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cajovna.Controllers
{
    public class SlozeniController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Add(int id = 0) // = ID polozkyMenu
        {
            PolozkaMenu polozkaMenu = db.PolozkyMenu.Find(id);
            if (polozkaMenu == null) return HttpNotFound();
            ViewBag.polozkaMenuID = id;
            ViewBag.suroviny = getListAddableMaterials(id);
            return View();
        }

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

        public ActionResult Edit(int id = 0)
        {
            Slozeni slozeni = db.Slozeni.Find(id);
            if (slozeni == null) return HttpNotFound();
            ViewBag.suroviny = getListEditableMaterials(slozeni.polozkaMenuID, slozeni.surovinaID);
            return View(slozeni);
        }

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

        public ActionResult Delete(int id = 0)
        {
            Slozeni slozeni = db.Slozeni.Find(id);
            if (slozeni == null) return HttpNotFound();
            return View(slozeni);
        }

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