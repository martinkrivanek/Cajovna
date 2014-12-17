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
    public sealed class SlozeniDAOImpl : SlozeniDAO, IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void create(Slozeni slozeni)
        {
            db.Slozeni.Add(slozeni);
            db.SaveChanges();
        }

        public Slozeni read(int id)
        {
            Slozeni slozeni = db.Slozeni.Find(id);
            return slozeni;
        }

        public void update(Slozeni slozeni)
        {
            db.Entry(slozeni).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void delete(Slozeni slozeni)
        {
            db.Slozeni.Remove(slozeni);
            db.SaveChanges();
        }

        public List<Slozeni> readAll()
        {
            return db.Slozeni.ToList();
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}