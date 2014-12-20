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
    public class SurovinyControllerTest
    {
        static int id = 0;

        [TestMethod]
        public void SurovinyController_0_Pre()
        {
            SurovinyDAO dao = new SurovinyDAOImpl();

            foreach (Surovina s in dao.readAll())
            {
                dao.delete(s);
                Console.WriteLine("deleted one");
            }

            Surovina sur1 = new Surovina
            {
                name = "TEST",
                desription = "Test",
                unit = Unit.g,
                number_of_units = 100,
                price = 100
            };
            
            dao.create(sur1);
            List<Surovina> list = dao.readAll();
            Assert.AreEqual(1, list.Count);
            id = list.ToArray()[0].surovinaID;
            Assert.AreNotEqual(0, id);
        }

        [TestMethod]
        public void SurovinyController_Index()
        {
            ViewResult result = new SurovinyController().Index("none", 1) as ViewResult;
            List<Surovina> model = (List<Surovina>)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual("TEST", model.ToArray()[0].name);
        }

        [TestMethod]
        public void SurovinyController_Add()
        {
            //NO IDEA WHAT TO DO HERE...other testing must cover
        }

        [TestMethod]
        public void SurovinyController_Detail()
        {
            ViewResult result = new SurovinyController().Detail(id) as ViewResult;
            Surovina model = (Surovina)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual("TEST", model.name);
        }

        [TestMethod]
        public void SurovinyController_Detail_Fail()
        {
            ActionResult result = new SurovinyController().Detail(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }

        [TestMethod]
        public void SurovinyController_Edit()
        {
            ViewResult result = new SurovinyController().Edit(id) as ViewResult;
            Surovina model = (Surovina)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual("TEST", model.name);
        }

        [TestMethod]
        public void SurovinyController_Edit_Fail()
        {
            ActionResult result = new SurovinyController().Edit(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }

        [TestMethod]
        public void SurovinyController_Delete()
        {
            ViewResult result = new SurovinyController().Delete(id) as ViewResult;
            Surovina model = (Surovina)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.AreEqual("TEST", model.name);
        }

        [TestMethod]
        public void SurovinyController_Delete_Fail()
        {
            ActionResult result = new SurovinyController().Delete(-1);
            Assert.AreEqual(result.GetType(), new HttpNotFoundResult().GetType());
        }
    }
}
