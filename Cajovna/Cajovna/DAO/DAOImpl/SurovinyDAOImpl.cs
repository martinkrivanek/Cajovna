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
    public sealed class SurovinyDAOImpl : SurovinyDAO, IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void create(Surovina surovina)
        {
            db.Suroviny.Add(surovina);
            db.SaveChanges();
        }

        public Surovina read(int id)
        {
            Surovina surovina = db.Suroviny.Find(id);
            return surovina;
        }

        public void update(Surovina surovina)
        {
            db.Entry(surovina).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void delete(Surovina surovina)
        {
            db.Suroviny.Remove(surovina);
            db.SaveChanges();
        }

        public List<Surovina> readAll()
        {
            return db.Suroviny.ToList();
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}