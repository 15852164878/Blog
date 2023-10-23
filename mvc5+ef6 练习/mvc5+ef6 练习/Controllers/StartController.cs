using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc5_ef6_练习.Controllers
{
    public class StartController : Controller
    {
        // GET: Start
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Ulist()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View("Ulist");
        }
    }
}