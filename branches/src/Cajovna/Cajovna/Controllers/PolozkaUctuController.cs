﻿using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cajovna.Controllers
{
    public class PolozkaUctuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Add(int id = 0) //id ucet
        {
            Ucet ucet = db.Ucty.Find(id);
            if (ucet == null) return HttpNotFound();
            ViewBag.ucetID = id;
            ViewBag.polozkyMenu = db.PolozkyMenu.Where(b => b.avalible).OrderBy(a => a.name).ToList();
            ViewBag.stulID = ucet.stulID;
            return View();
        }

        [HttpPost]
        public ActionResult Add(PolozkaUctu polozkaUctu) //id ucet
        {
            if (ModelState.IsValid)
            {
                db.PolozkyUctu.Add(polozkaUctu);
                db.SaveChanges();
                return RedirectToAction("Detail", "Ucet", new { id = polozkaUctu.ucetID });
            }
            ViewBag.errors = "error";
            ViewBag.ucetID = polozkaUctu.ucetID;
            ViewBag.stulID = db.Ucty.Find(polozkaUctu.ucetID).stulID;
            ViewBag.polozkyMenu = db.PolozkyMenu.Where(b => b.avalible).OrderBy(a => a.name).ToList();
            return View(polozkaUctu);
        }

        public ActionResult Delete(int id = 0) //id polozkaUctu
        {
            PolozkaUctu polozkaUctu = db.PolozkyUctu.Find(id);
            if (polozkaUctu == null) return HttpNotFound();
            ViewBag.stulID = polozkaUctu.ucet.stulID;
            return View(polozkaUctu);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PolozkaUctu polozkaUctu = db.PolozkyUctu.Find(id);
            db.PolozkyUctu.Remove(polozkaUctu);
            db.SaveChanges();
            return RedirectToAction("Detail", "Ucet", new { id = polozkaUctu.ucetID });
        }
    }
}