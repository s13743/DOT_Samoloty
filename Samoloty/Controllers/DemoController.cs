using Samoloty.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samoloty.Controllers
{
    public class DemoController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize(Roles = "superadmin")]
        public ActionResult Work1()
        {
            return View("Work1");
        }

        [CustomAuthorize(Roles = "superadmin,admin")]
        public ActionResult Work2()
        {
            return View("Work2");
        }

        [CustomAuthorize(Roles = "superadmin,admin,pilot")]
        public ActionResult Work3()
        {
            return View("Work3");
        }
    }
}