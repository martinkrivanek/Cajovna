using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cajovna.Controllers
{
    public class StulController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Stoly.ToList());
        }

        public ActionResult Detail(int id = 0)
        {
            Stul stul = db.Stoly.Find(id);
            if (stul == null) return HttpNotFound();
            return View(stul);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Stul stul)
        {
            if (ModelState.IsValid)
            {
                db.Stoly.Add(stul);
                db.SaveChanges();
                return RedirectToAction("Index", "Stul");
            }
            return View(stul);
        }

        public ActionResult Delete(int id = 0)
        {
            Stul stul = db.Stoly.Find(id);
            if (stul == null) return HttpNotFound();
            return View(stul);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Stul stul = db.Stoly.Find(id);
            if (allUctyClosed(stul))
            {
                db.Stoly.Remove(stul);
                db.SaveChanges();
                return RedirectToAction("Index", "Stul");
            }
            ViewBag.errors = "Nemůžete smazat stůl s otevřenými účty";
            return View(stul);
        }

        public ActionResult Edit(int id = 0)
        {
            Stul stul = db.Stoly.Find(id);
            if (stul == null) return HttpNotFound();
            return View(stul);
        }

        [HttpPost]
        public ActionResult Edit(Stul stul)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stul).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Stul");
            }
            return View(stul);
        }

        // HELPERs
        // checks if all accounts asociated with Stul are closed
        private bool allUctyClosed(Stul stul)
        {
            return stul.ucty.Where(a => a.date_closed == null).Count() == 0;
        }
    }
}