using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cajovna.Models;
using Cajovna.DAO;
using System.Collections.Generic;

namespace Cajovna.Tests.DAO
{
    [TestClass]
    public class PolozkaMenuDAOTest
    {
        PolozkyMenuDAO dao = new PolozkyMenuDAOImpl();

        static int id = 0; 

        PolozkaMenu polM = new PolozkaMenu
        {
            name = "TEST",
            price_sell = 100,
            avalible = true,
            description = "Test"
        };

        [TestMethod]
        public void PolozkyMenuDAO_1_DeleteAndReadAll()
        {
            List<PolozkaMenu> list = dao.readAll();
            foreach (PolozkaMenu s in list)
            {
                dao.delete(s);
                Console.WriteLine("deleted one");
            }
            Assert.AreEqual(0, dao.readAll().Count);
        }

        [TestMethod]
        public void PolozkyMenuDAO_2_Create()
        {
            dao.create(polM);
            List<PolozkaMenu> list = dao.readAll();
            Assert.AreEqual(1, list.Count);

            id = list.ToArray()[0].polozkaMenuID;
            Assert.AreNotEqual(id, 0);
        }

        [TestMethod]
        public void PolozkyMenuDAO_3_ReadOneAndUpdate()
        {
            PolozkaMenu s = dao.read(id);
            Assert.IsNotNull(s);

            String old = s.name;
            s.name = old + "_EDITED";
            dao.update(s);

            s = dao.read(id);
            Assert.AreEqual(id, s.polozkaMenuID);

            Assert.AreNotEqual(old, s.name);
        }
    }
}
