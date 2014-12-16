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
    public class UcetDAOImpl : UcetDAO
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void create(Ucet ucet)
        {
            db.Ucty.Add(ucet);
            db.SaveChanges();
        }

        public Ucet read(int id)
        {
            Ucet ucet = db.Ucty.Find(id);
            return ucet;
        }

        public void update(Ucet ucet)
        {
            db.Entry(ucet).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void delete(Ucet ucet)
        {
            db.Ucty.Remove(ucet);
            db.SaveChanges();
        }

        public List<Ucet> readAll()
        {
            return db.Ucty.ToList();
        }

    }
}