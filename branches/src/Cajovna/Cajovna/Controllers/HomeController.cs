using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cajovna.Controllers
{
    public class HomeController : Controller
    {
        /* url: localhost */
        public ActionResult Index()
        {
            return RedirectToAction("Admin");
        }

        /* url: localhost/Admin */
        public ActionResult Admin()
        {
            return View();
        }
    }
}