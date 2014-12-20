using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cajovna.Controllers;
using Cajovna.Models;
using System.Web.Mvc;
using Cajovna.DAO;
using System.Collections.Generic;

namespace Cajovna.Tests.Controllers
{
    [TestClass]
    public class UcetControllerTest
    {
        static int id = 0;
        static int idStul;

        [TestMethod]
        public void UcetController_0_Pre()
        {
            UcetDAO dao = new UcetDAOImpl();
            StulDAO daoStul = new StulDAOImpl();

            Stul stul1 = new Stul { };
     
            foreach (Ucet u in dao.readAll())
            {
                dao.delete(u);
                Console.WriteLine("deleted one");
            }

            foreach (Stul s in daoStul.readAll())
            {
                daoStul.delete(s);
                Console.WriteLine("deleted one");
            }

            daoStul.create(stul1);
            List<Stul> listStul = daoStul.readAll();
            stul1 = listStul.ToArray()[0];
            idStul = stul1.stulID;

            Ucet ucet1 = new Ucet
            {
                name = "Ucet1",
                stul = stul1
            };

            dao.create(ucet1);
            List<Ucet> list = dao.readAll();
            Assert.AreEqual(1, list.Count);
            id = list.ToArray()[0].ucetID;
            
            Assert.AreNotEqual(0, id);
        }

        [TestMethod]
        public void UcetController_Add()
        {
            ViewResult result = new UcetController().Add(idStul) as ViewResult;
            int stulID = result.ViewBag.stulID;
            Assert.AreEqual(stulID, idStul);
        }

        [TestMethod]
        public void UcetController_Add_Fail()
        {
            ActionResult result = new UcetController().Add(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }

        [TestMethod]
        public void UcetController_Index()
        {
            ViewResult result = new UcetController().Index("a-z", 1) as ViewResult;
            List<Ucet> model = (List<Ucet>)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual("Ucet1", model.ToArray()[0].name);
        }

        [TestMethod]
        public void UcetController_Detail()
        {
            ViewResult result = new UcetController().Detail(id) as ViewResult;
            Ucet model = (Ucet)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual("Ucet1", model.name);
        }

        [TestMethod]
        public void UcetController_Detail_Fail()
        {
            ActionResult result = new UcetController().Detail(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }

        [TestMethod]
        public void UcetController_Edit()
        {
            ViewResult result = new UcetController().Edit(id) as ViewResult;
            Ucet model = (Ucet)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual("Ucet1", model.name);
        }

        [TestMethod]
        public void UcetController_Edit_Fail()
        {
            ActionResult result = new UcetController().Edit(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }

        [TestMethod]
        public void UcetController_Delete()
        {
            ViewResult result = new UcetController().Delete(id) as ViewResult;
            Ucet model = (Ucet)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual("Ucet1", model.name);
        }

        [TestMethod]
        public void UcetController_Delete_Fail()
        {
            ActionResult result = new UcetController().Delete(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }
    }
}