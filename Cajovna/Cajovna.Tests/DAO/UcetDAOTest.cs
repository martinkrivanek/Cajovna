using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cajovna.DAO;
using Cajovna.Models;
using System.Collections.Generic;

namespace Cajovna.Tests.DAO
{
    [TestClass]
    public class UcetDAOTest
    {
        UcetDAO dao = new UcetDAOImpl();
        

        static int id = 0;
        static int stulID = 0;

        Ucet ucet1 = new Ucet
        {
            name = "Ucet1",
            date_added = DateTime.Now,
            stulID = 1
        };

        Stul stul1 = new Stul { };

        [TestMethod]
        public void UcetDAO_0_Pre()
        {
            StulDAO stulDao = new StulDAOImpl();
            stulDao.create(stul1);
        }

        [TestMethod]
        public void UcetDAO_1_DeleteAndReadAll()
        {
            List<Ucet> list = dao.readAll();
            foreach (Ucet u in list)
            {
                dao.delete(u);
                Console.WriteLine("deleted one");
            }
            Assert.AreEqual(0, dao.readAll().Count);
        }

        [TestMethod]
        public void UcetDAO_2_Create()
        {
            List<Stul> stulList = new StulDAOImpl().readAll();
            stulID = stulList.ToArray()[0].stulID;

            ucet1.stulID = stulID;
            dao.create(ucet1);
            List<Ucet> list = dao.readAll();
            Assert.AreEqual(1, list.Count);

            id = list.ToArray()[0].ucetID;
            Assert.AreNotEqual(id, 0);
        }

        [TestMethod]
        public void UcetDAO_3_ReadOneAndUpdate()
        {
            Ucet u = dao.read(id);
            Assert.IsNotNull(u);

            String old = u.name;
            u.name = old + "_EDITED";
            dao.update(u);

            u = dao.read(id);
            Assert.AreEqual(id, u.ucetID);

            Assert.AreNotEqual(old, u.name);
        }

    }
}