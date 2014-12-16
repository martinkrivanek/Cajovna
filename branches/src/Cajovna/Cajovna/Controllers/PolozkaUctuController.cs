using Cajovna.DAO;
using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cajovna.Controllers
{
    /* Controller servicing actions of PolozkaUctu entity */
    public class PolozkaUctuController : Controller
    {
        private PolozkaUctuDAO polUctuDAO = new PolozkaUctuDAOImpl();
        private UcetDAO ucetDAO = new UcetDAOImpl();
        private PolozkyMenuDAO polMenuDAO = new PolozkyMenuDAOImpl();

        /* Get method action which returns a view to CREATE a PolozkaUctu within an Ucet 
         * defined by input id parameter */
        public ActionResult Add(int id = 0) //id ucet
        {
            Ucet ucet = ucetDAO.read(id);
            if (ucet == null) return HttpNotFound();
            ViewBag.ucetID = id;
            ViewBag.polozkyMenu = polMenuDAO.readAll().Where(b => b.avalible).OrderBy(a => a.name).ToList();
            ViewBag.stulID = ucet.stulID;
            return View();
        }

        /* Post method action which processes (CREATES) the PolozakaUctu object given back by the
         * submitted get method. In failure scenario it returns back the object and challenges
         * the user to make some necessary changes*/
        [HttpPost]
        public ActionResult Add(PolozkaUctu polozkaUctu) //id ucet
        {
            if (ModelState.IsValid)
            {
                polUctuDAO.create(polozkaUctu);
                return RedirectToAction("Detail", "Ucet", new { id = polozkaUctu.ucetID });
            }
            ViewBag.errors = "error";
            ViewBag.ucetID = polozkaUctu.ucetID;
            ViewBag.stulID = ucetDAO.read(polozkaUctu.ucetID).stulID;
            ViewBag.polozkyMenu = polMenuDAO.readAll().Where(b => b.avalible).OrderBy(a => a.name).ToList();
            return View(polozkaUctu);
        }

        /* Get method action which returns a view to DELETE a PolozkaUctu within an Ucet
         * defined by input id parameter */
        public ActionResult Delete(int id = 0) //id polozkaUctu
        {
            PolozkaUctu polozkaUctu = polUctuDAO.read(id);
            if (polozkaUctu == null) return HttpNotFound();
            ViewBag.stulID = polozkaUctu.ucet.stulID;
            return View(polozkaUctu);
        }

        /* Post method action which processes (DELETES) the polozkaUctu object which the input 
         * id parameter was given back by the submitted get method defines. In failure scenario it 
         * returns back the object and challenges the user to make some necessary changes */
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PolozkaUctu polozkaUctu = polUctuDAO.read(id);
            polUctuDAO.delete(polozkaUctu);
            return RedirectToAction("Detail", "Ucet", new { id = polozkaUctu.ucetID });
        }
    }
}