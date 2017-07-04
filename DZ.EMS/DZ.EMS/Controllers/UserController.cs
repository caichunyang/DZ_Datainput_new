using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DZ.EMS.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Users()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult loginout()
        {
            //Session["user"] = null;
            Session.Remove("user");
            return RedirectToAction("login", "home");
        }
    }
}
