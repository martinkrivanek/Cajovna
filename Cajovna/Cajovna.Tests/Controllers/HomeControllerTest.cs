using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cajovna;
using Cajovna.Controllers;
using Cajovna.Models;
using Cajovna.DAO;

namespace Cajovna.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        [TestMethod]
        public void HomeController_Index()
        {
            var controller = new HomeController();
            var result = (RedirectToRouteResult)controller.Index();
            Assert.AreEqual("Admin", result.RouteValues["action"]);
        }

        [TestMethod]
        public void HomeController_Admin()
        {
            //NO IDEA WHAT TO DO HERE...other testing must cover
        }


        
    }
}
