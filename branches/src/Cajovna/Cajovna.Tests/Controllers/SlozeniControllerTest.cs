using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cajovna.Models;
using Cajovna.DAO;
using System.Collections.Generic;
using System.Web.Mvc;
using Cajovna.Controllers;

namespace Cajovna.Tests.Controllers
{
    [TestClass]
    public class SlozeniControllerTest
    {
        static int id = 0;
        static int id_polMenu = 0;

        [TestMethod]
        public void SlozeniController_0_Pre()
        {
            Surovina sur1 = new Surovina
            {
                name = "TEST",
                desription = "Test",
                unit = Unit.g,
                number_of_units = 100,
                price = 100
            };

            PolozkaMenu polM = new PolozkaMenu 
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

            PolozkyMenuDAO pDAO = new PolozkyMenuDAOImpl();
            foreach (PolozkaMenu p in pDAO.readAll())
            {
                pDAO.delete(p);
            }
            pDAO.create(polM);
            polM = pDAO.readAll().ToArray()[0];
            id_polMenu = polM.polozkaMenuID;
            Assert.AreNotEqual(0, id_polMenu);

            SurovinyDAO sDAO = new SurovinyDAOImpl();
            foreach (Surovina p in sDAO.readAll())
            {
                sDAO.delete(p);
            }
            sDAO.create(sur1);
            sur1 = sDAO.readAll().ToArray()[0];


            SlozeniDAO dao = new SlozeniDAOImpl();
            dao.create(slozeni);
            List<Slozeni> list = dao.readAll();
            Assert.AreEqual(1, list.Count);
            id = list.ToArray()[0].slozeniID;
            Assert.AreNotEqual(0, id);
        }

        [TestMethod]
        public void SlozeniController_Add()
        {
            ViewResult result = new SlozeniController().Add(id_polMenu) as ViewResult;
            int idPolMenu = (int)result.ViewBag.polozkaMenuID;
            Assert.AreNotEqual(0, idPolMenu);
            Assert.AreEqual((int)idPolMenu, (int)id_polMenu);

        }

        [TestMethod]
        public void SlozeniController_Add_Fail()
        {
            ActionResult result = new SlozeniController().Add(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }

        [TestMethod]
        public void SlozeniController_Edit()
        {
            ViewResult result = new SlozeniController().Edit(id) as ViewResult;
            Slozeni model = (Slozeni)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.quantity);
        }

        [TestMethod]
        public void SlozeniController_Edit_Fail()
        {
            ActionResult result = new PolozkyMenuController().Edit(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }

        [TestMethod]
        public void SlozeniController_Delete()
        {
            ViewResult result = new SlozeniController().Delete(id) as ViewResult;
            Slozeni model = (Slozeni)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.quantity);
        }

        [TestMethod]
        public void SlozeniController_Delete_Fail()
        {
            ActionResult result = new PolozkyMenuController().Delete(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }
    }
}
