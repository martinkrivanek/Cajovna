using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cajovna.Controllers
{
    /* Controller servicing actions of Stul entity */
    public class StulController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /* Action which returns a view to show LIST of Stul objects */
        public ActionResult Index()
        {
            return View(db.Stoly.ToList());
        }

        /* Action which returns a view to show DETAIL of PolozkaMenu object defined by the input parameter id */
        public ActionResult Detail(int id = 0)
        {
            Stul stul = db.Stoly.Find(id);
            if (stul == null) return HttpNotFound();
            return View(stul);
        }

        /* Get method action which returns a view to CREATE a Stul*/
        public ActionResult Add()
        {
            return View();
        }

        /* Post method action which processes (CREATES) the Stul object given back by the
         * submitted get method. In failure scenario it returns back the object and challenges
         * the user to make some necessary changes */
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

        /* Get method action which returns a view to DELETE a Stul acordingly to the input id paremeter*/
        public ActionResult Delete(int id = 0)
        {
            Stul stul = db.Stoly.Find(id);
            if (stul == null) return HttpNotFound();
            return View(stul);
        }

        /* Post method action which processes (DELETES) the Stul object which the input 
         * id parameter was given back by the submitted get method defines. In failure scenario it 
         * returns back the object and challenges the user to make some necessary changes */
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

        /* Get method action which returns a view to EDIT a Stul acordingly to the input id paremeter*/
        public ActionResult Edit(int id = 0)
        {
            Stul stul = db.Stoly.Find(id);
            if (stul == null) return HttpNotFound();
            return View(stul);
        }

        /* Post method action which processes (EDITS) the Stul object given back by the
         * submitted get method. In failure scenario it returns back the object and challenges
         * the user to make some necessary changes */
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
        /* checks if all accounts asociated with Stul are closed */
        private bool allUctyClosed(Stul stul)
        {
            return stul.ucty.Where(a => a.date_closed == null).Count() == 0;
        }
    }
}