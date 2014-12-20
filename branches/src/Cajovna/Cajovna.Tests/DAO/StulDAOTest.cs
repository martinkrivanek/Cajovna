using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cajovna.DAO;
using Cajovna.Models;
using System.Collections.Generic;

namespace Cajovna.Tests.DAO
{
    [TestClass]
    public class StulDAOTest
    {
        StulDAO dao = new StulDAOImpl();


        static int id = 0;


        Stul stul1 = new Stul
        {
            name = "Stul1",
        };


        [TestMethod]
        public void StulDAO_1_DeleteAndReadAll()
        {
            List<Stul> list = dao.readAll();
            foreach (Stul s in list)
            {
                dao.delete(s);
                Console.WriteLine("deleted one");
            }
            Assert.AreEqual(0, dao.readAll().Count);
        }

        [TestMethod]
        public void StulDAO_2_Create()
        {
            dao.create(stul1);
            List<Stul> list = dao.readAll();
            Assert.AreEqual(1, list.Count);

            id = list.ToArray()[0].stulID;
            Assert.AreNotEqual(id, 0);
        }

        [TestMethod]
        public void StulDAO_3_ReadOneAndUpdate()
        {
            Stul s = dao.read(id);
            Assert.IsNotNull(s);

            String old = s.name;
            s.name = old + "_EDITED";
            dao.update(s);

            s = dao.read(id);
            Assert.AreEqual(id, s.stulID);

            Assert.AreNotEqual(old, s.name);
        }

    }
}