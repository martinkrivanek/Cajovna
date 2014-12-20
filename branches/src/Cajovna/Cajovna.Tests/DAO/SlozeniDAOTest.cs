using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cajovna.Models;
using System.Collections.Generic;
using Cajovna.DAO;

namespace Cajovna.Tests.DAO
{
    [TestClass]
    public class SlozeniDAOTest
    {
        SlozeniDAO dao = new SlozeniDAOImpl();

        static int id = 0;     

        static Surovina sur1 = new Surovina
        {
            name = "TEST",
            desription = "Test",
            unit = Unit.g,
            number_of_units = 100,
            price = 100
        };

        static PolozkaMenu polM = new PolozkaMenu 
        {
            name = "TEST",
            price_sell = 100,
            avalible = true,
            description = "Test"
        };

        Slozeni slozeni = new Slozeni
        {
            surovina = sur1,
            quantity = 1,
            polozkaMenu = polM
        };

        

        [TestMethod]
        public void SlozeniDAO_0_Pre()
        {
            PolozkyMenuDAO pDAO = new PolozkyMenuDAOImpl();
            foreach (PolozkaMenu p in pDAO.readAll())
            {
                pDAO.delete(p);
            }
            pDAO.create(polM);
            polM = pDAO.readAll().ToArray()[0];

            SurovinyDAO sDAO = new SurovinyDAOImpl();
            foreach (Surovina p in sDAO.readAll())
            {
                sDAO.delete(p);
            }
            sDAO.create(sur1);
            sur1 = sDAO.readAll().ToArray()[0];
        }


        [TestMethod]
        public void SlozeniDAO_1_DeleteAndReadAll()
        {
            List<Slozeni> list = dao.readAll();
            foreach (Slozeni s in list)
            {
                dao.delete(s);
                Console.WriteLine("deleted one");
            }
            Assert.AreEqual(0, dao.readAll().Count);
        }

        [TestMethod]
        public void SlozeniDAO_2_Create()
        {
            dao.create(slozeni);
            List<Slozeni> list = dao.readAll();
            Assert.AreEqual(1, list.Count);

            id = list.ToArray()[0].slozeniID;
            Assert.AreNotEqual(id, 0);
        }

        [TestMethod]
        public void SlozeniDAO_3_ReadOneAndUpdate()
        {
            Slozeni s = dao.read(id);
            Assert.IsNotNull(s);

            double old = s.quantity;
            s.quantity = old + 2;
            dao.update(s);

            s = dao.read(id);
            Assert.AreEqual(id, s.slozeniID);

            Assert.AreNotEqual(old, s.quantity);
        }
    }
}
