using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cajovna.DAO;
using Cajovna.Models;
using System.Collections.Generic;

namespace Cajovna.Tests.DAO
{
    [TestClass]
    public class SurovinyDAOTest
    {
        SurovinyDAO dao = new SurovinyDAOImpl();

        static int id = 0;

        Surovina sur1 = new Surovina
        {
            name = "TEST",
            desription = "Test",
            unit = Unit.g,
            number_of_units = 100,
            price = 100
        };


        [TestMethod]
        public void SurovinyDAO_1_DeleteAndReadAll()
        {
            List<Surovina> list = dao.readAll();
            foreach (Surovina s in list)
            {
                dao.delete(s);
                Console.WriteLine("deleted one");
            }
            Assert.AreEqual(0, dao.readAll().Count);
        }

        [TestMethod]
        public void SurovinyDAO_2_Create()
        {
            dao.create(sur1);
            List<Surovina> list = dao.readAll();
            Assert.AreEqual(1, list.Count);

            id = list.ToArray()[0].surovinaID;
            Assert.AreNotEqual(id, 0);            
        }

        [TestMethod]
        public void SurovinyDAO_3_ReadOneAndUpdate()
        {
            Surovina s = dao.read(id);
            Assert.IsNotNull(s);

            String old = s.name;
            s.name = old + "_EDITED";
            dao.update(s);

            s = dao.read(id);
            Assert.AreEqual(id, s.surovinaID);

            Assert.AreNotEqual(old, s.name);
        }

    }
}
