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
    public sealed class StulDAOImpl : StulDAO, IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void create(Stul stul)
        {
            db.Stoly.Add(stul);
            db.SaveChanges();
        }

        public Stul read(int id)
        {
            Stul stul = db.Stoly.Find(id);
            return stul;
        }

        public void update(Stul stul)
        {
            db.Entry(stul).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void delete(Stul stul)
        {
            db.Stoly.Remove(stul);
            db.SaveChanges();
        }

        public List<Stul> readAll()
        {
            return db.Stoly.ToList();
        }
        public void Dispose()
        {
            db.Dispose();
        }

    }
}