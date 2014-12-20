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
    public class StulControllerTest
    {
        static int id = 0;

        [TestMethod]
        public void StulController_0_Pre()
        {
            StulDAO dao = new StulDAOImpl();

            foreach (Stul s in dao.readAll())
            {
                dao.delete(s);
                Console.WriteLine("deleted one");
            }

            Stul stul1 = new Stul
            {
                name = "Stul1",
            };

            dao.create(stul1);
            List<Stul> list = dao.readAll();
            Assert.AreEqual(1, list.Count);
            id = list.ToArray()[0].stulID;
            Assert.AreNotEqual(0, id);
        }

        [TestMethod]
        public void StulController_Add()
        {
            //NO IDEA WHAT TO DO HERE...other testing must cover
        }

        [TestMethod]
        public void StulController_Index()
        {
            ViewResult result = new StulController().Index() as ViewResult;
            List<Stul> model = (List<Stul>)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual("Stul1", model.ToArray()[0].name);
        }

        [TestMethod]
        public void StulController_Detail()
        {
            ViewResult result = new StulController().Detail(id) as ViewResult;
            Stul model = (Stul)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual("Stul1", model.name);
        }

        [TestMethod]
        public void StulController_Detail_Fail()
        {
            ActionResult result = new StulController().Detail(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }

        [TestMethod]
        public void StulController_Edit()
        {
            ViewResult result = new StulController().Edit(id) as ViewResult;
            Stul model = (Stul)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual("Stul1", model.name);
        }

        [TestMethod]
        public void StulController_Edit_Fail()
        {
            ActionResult result = new StulController().Edit(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }

        [TestMethod]
        public void StulController_Delete()
        {
            ViewResult result = new StulController().Delete(id) as ViewResult;
            Stul model = (Stul)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual("Stul1", model.name);
        }

        [TestMethod]
        public void StulController_Delete_Fail()
        {
            ActionResult result = new StulController().Delete(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }
    }
}