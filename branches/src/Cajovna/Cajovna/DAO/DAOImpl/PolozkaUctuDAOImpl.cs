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
    public sealed class PolozkaUctuDAOImpl : PolozkaUctuDAO, IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void create(PolozkaUctu polozkaUctu)
        {
            db.PolozkyUctu.Add(polozkaUctu);
            db.SaveChanges();
        }

        public PolozkaUctu read(int id)
        {
            PolozkaUctu polozkaUctu = db.PolozkyUctu.Find(id);
            return polozkaUctu;
        }

        public void update(PolozkaUctu polozkaUctu)
        {
            db.Entry(polozkaUctu).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void delete(PolozkaUctu polozkaUctu)
        {
            db.PolozkyUctu.Remove(polozkaUctu);
            db.SaveChanges();
        }

        public List<PolozkaUctu> readAll()
        {
            return db.PolozkyUctu.ToList();
        }

        public void Dispose()
        {
            db.Dispose();            
        }

    }
}