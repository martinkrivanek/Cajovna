using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cajovna.DAO;
using Cajovna.Models;
using System.Collections.Generic;

namespace Cajovna.Tests.DAO
{
    [TestClass]
    public class PolozkaUctuDAOTest
    {
        PolozkaUctuDAO dao = new PolozkaUctuDAOImpl();


        static int id = 0;

        static PolozkaMenu polM = new PolozkaMenu
        {
            name = "TEST",
            price_sell = 100,
            avalible = true,
            description = "Test"
        };

        static Stul stul1 = new Stul { };

        static Ucet ucet1 = new Ucet
        {
            name = "Ucet1",
            stul = stul1
        };

        PolozkaUctu polozkaUctu1 = new PolozkaUctu
        {
            ucet = ucet1,
            polozkaMenu = polM
        };

        [TestMethod]
        public void PolozkaUctuDAO_0_Pre()
        {
            PolozkyMenuDAO polozkyMenuDAO = new PolozkyMenuDAOImpl();
            UcetDAO ucetDao = new UcetDAOImpl();
            StulDAO stulDao = new StulDAOImpl();
            

            foreach (PolozkaUctu pu in dao.readAll())
            {
                dao.delete(pu);
                Console.WriteLine("deleted one");
            }

            foreach (PolozkaMenu p in polozkyMenuDAO.readAll())
            {
                polozkyMenuDAO.delete(p);
            }
            polozkyMenuDAO.create(polM);
            polM = polozkyMenuDAO.readAll().ToArray()[0];

            
            foreach (Ucet u in ucetDao.readAll())
            {
                ucetDao.delete(u);
            }
            ucet1.stulID = stul1.stulID;
            ucetDao.create(ucet1);
            ucet1 = ucetDao.readAll().ToArray()[0];

            
            foreach(Stul s in stulDao.readAll())
            {
                stulDao.delete(s);
            }
            stulDao.create(stul1);
            stul1 = stulDao.readAll().ToArray()[0];

        }


        [TestMethod]
        public void PolozkaUctuDAO_1_DeleteAndReadAll()
        {
            List<PolozkaUctu> list = dao.readAll();
            foreach (PolozkaUctu pu in list)
            {
                dao.delete(pu);
                Console.WriteLine("deleted one");
            }
            Assert.AreEqual(0, dao.readAll().Count);
        }

        [TestMethod]
        public void PolozkaUctuDAO_2_Create()
        {
            dao.create(polozkaUctu1);
            List<PolozkaUctu> list = dao.readAll();
            Assert.AreEqual(1, list.Count);

            id = list.ToArray()[0].polozkaUctuID;
            Assert.AreNotEqual(id, 0);
        }

        [TestMethod]
        public void PolozkaUctuDAO_3_ReadOneAndUpdate()
        {
            PolozkaUctu pu = dao.read(id);
            Assert.IsNotNull(pu);

            DateTime dateTime = DateTime.Now;
            pu.date_ordered = dateTime;
            dao.update(pu);

            pu = dao.read(id);
            Assert.AreEqual(dateTime, pu.date_ordered);
        }

    }
}