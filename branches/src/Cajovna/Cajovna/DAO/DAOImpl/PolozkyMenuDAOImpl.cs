using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using Cajovna.Models;
using System.Text;


namespace Cajovna.DAO
{
    public class PolozkyMenuDAOImpl : PolozkyMenuDAO
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void create(PolozkaMenu polozkaMenu)
        {
            db.PolozkyMenu.Add(polozkaMenu);
            db.SaveChanges();
        }

        public PolozkaMenu read(int id)
        {
            PolozkaMenu polozkaMenu = db.PolozkyMenu.Find(id);
            return polozkaMenu;
        }

        public void update(PolozkaMenu polozkaMenu)
        {
            db.Entry(polozkaMenu).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void delete(PolozkaMenu polozkaMenu)
        {
            db.PolozkyMenu.Remove(polozkaMenu);
            db.SaveChanges();
        }

        public List<PolozkaMenu> readAll()
        {
            return db.PolozkyMenu.ToList();
        }

    }
}