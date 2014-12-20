using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cajovna.Models;
using Cajovna.DAO;
using System.Web.Mvc;
using Cajovna.Controllers;

namespace Cajovna.Tests.Controllers
{
    [TestClass]
    public class PolozkaUctuControllerTest
    {
        static int id = 0;
        static int idUcet = 0;
        static int idStul = 0;

        [TestMethod]
        public void PolozkaUctuController_0_Pre()
        {
            //DAOs init
            PolozkaUctuDAO dao = new PolozkaUctuDAOImpl();
            PolozkyMenuDAO polozkyMenuDAO = new PolozkyMenuDAOImpl();
            UcetDAO ucetDao = new UcetDAOImpl();
            StulDAO stulDao = new StulDAOImpl();

            //Delete all
            foreach (PolozkaUctu pu in dao.readAll())
            {
                dao.delete(pu);
            }
            Assert.AreEqual(0, dao.readAll().Count);

            foreach (PolozkaMenu p in polozkyMenuDAO.readAll())
            {
                polozkyMenuDAO.delete(p);
            }
            Assert.AreEqual(0, polozkyMenuDAO.readAll().Count);

            foreach (Ucet u in ucetDao.readAll())
            {
                ucetDao.delete(u);
            }
            Assert.AreEqual(0, ucetDao.readAll().Count);

            foreach (Stul s in stulDao.readAll())
            {
                stulDao.delete(s);
            }
            Assert.AreEqual(0, stulDao.readAll().Count);


            // polozkaMenu creation
            PolozkaMenu polM = new PolozkaMenu
            {
                name = "TEST",
                price_sell = 100,
                avalible = true,
                description = "Test"
            };

            polozkyMenuDAO.create(polM);
            polM = polozkyMenuDAO.readAll().ToArray()[0];

            Assert.IsNotNull(polM);
            Assert.AreNotEqual(0, polM.polozkaMenuID);



            // stul creation
            Stul stul1 = new Stul { };

            stulDao.create(stul1);
            stul1 = stulDao.readAll().ToArray()[0];

            idStul = stul1.stulID;

            Assert.IsNotNull(stul1);
            Assert.AreNotEqual(0, idStul);



            // ucet na stul creation
            Ucet ucet1 = new Ucet
            {
                name = "Ucet1",
                stul = stul1
            };

            ucetDao.create(ucet1);
            ucet1 = ucetDao.readAll().ToArray()[0];
            idUcet = ucet1.ucetID;
            Assert.IsNotNull(ucet1);
            Assert.AreNotEqual(0, idUcet);



            // polozka uctu creation
            PolozkaUctu polozkaUctu1 = new PolozkaUctu
            {
                ucet = ucet1,
                polozkaMenu = polM
            };

            dao.create(polozkaUctu1);
            polozkaUctu1 = dao.readAll().ToArray()[0];
            id = polozkaUctu1.polozkaUctuID;
            Assert.IsNotNull(polozkaUctu1);
            Assert.AreNotEqual(0, id);
            
        }

        [TestMethod]
        public void PolozkaUctuController_Add()
        {
            ViewResult result = new PolozkaUctuController().Add(idUcet) as ViewResult;
            int tmp_ucetID = result.ViewBag.ucetID;
            Assert.AreEqual(tmp_ucetID, idUcet);
        }

        [TestMethod]
        public void PolozkaUctuController_Add_Fail()
        {
            ActionResult result = new PolozkaUctuController().Add(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }

        [TestMethod]
        public void PolozkaUctuController_Delete()
        {
            ViewResult result = new PolozkaUctuController().Delete(id) as ViewResult;
            PolozkaUctu model = (PolozkaUctu)result.ViewData.Model;
            Assert.IsNotNull(model);          
        }

        [TestMethod]
        public void PolozkaUctuController_Delete_Fail()
        {
            ActionResult result = new PolozkaUctuController().Delete(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }
    }
}
