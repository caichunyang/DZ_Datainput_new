using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DZ.EMS.Controllers
{
    public class HomenewController : Controller
    {
        //
        // GET: /Homenew/
        [AllowAnonymous]
        public ActionResult Index()
        {
            
            return View();
        }

    }
}
