using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cajovna.DAO;
using Cajovna.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Cajovna.Controllers;

namespace Cajovna.Tests.Controllers
{
    [TestClass]
    public class PolozkaMenuControllerTest
    {
        static int id = 0;

        [TestMethod]
        public void PolozkaMenuController_0_Pre()
        {
            PolozkyMenuDAO dao = new PolozkyMenuDAOImpl();

            foreach (PolozkaMenu s in dao.readAll())
            {
                dao.delete(s);
                Console.WriteLine("deleted one");
            }

            PolozkaMenu polM = new PolozkaMenu
            {
                name = "TEST",
                price_sell = 100,
                avalible = true,
                description = "Test"
            };

            dao.create(polM);
            List<PolozkaMenu> list = dao.readAll();
            Assert.AreEqual(1, list.Count);
            id = list.ToArray()[0].polozkaMenuID;
            Assert.AreNotEqual(0, id);
        }

        [TestMethod]
        public void PolozkaMenuController_Index()
        {
            ViewResult result = new PolozkyMenuController().Index("none", 1) as ViewResult;
            List<PolozkaMenu> model = (List<PolozkaMenu>)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual("TEST", model.ToArray()[0].name);
        }

        [TestMethod]
        public void PolozkaMenuController_Detail()
        {
            ViewResult result = new PolozkyMenuController().Detail(id) as ViewResult;
            PolozkaMenu model = (PolozkaMenu)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual("TEST", model.name);
        }

        [TestMethod]
        public void PolozkaMenuController_Detail_Fail()
        {
            ActionResult result = new PolozkyMenuController().Detail(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }

        [TestMethod]
        public void PolozkaMenuController_Edit()
        {
            ViewResult result = new PolozkyMenuController().Edit(id) as ViewResult;
            PolozkaMenu model = (PolozkaMenu)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual("TEST", model.name);
        }

        [TestMethod]
        public void PolozkaMenuController_Edit_Fail()
        {
            ActionResult result = new PolozkyMenuController().Edit(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }

        [TestMethod]
        public void PolozkaMenuController_Delete()
        {
            ViewResult result = new PolozkyMenuController().Delete(id) as ViewResult;
            PolozkaMenu model = (PolozkaMenu)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual("TEST", model.name);
        }

        [TestMethod]
        public void PolozkaMenuController_Delete_Fail()
        {
            ActionResult result = new PolozkyMenuController().Delete(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }
    }
}
