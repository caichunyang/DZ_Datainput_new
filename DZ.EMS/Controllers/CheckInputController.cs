using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DZ.EMS.Controllers
{
    public class CheckInputController : Controller
    {
        //
        // GET: /CheckInput/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult KeepQueue()
        {
   //          select i.imageid,p.values3,p.inputname, b.thedate||'/'||b.boxno||'/'||i.imageid||'.jpg' from dz_box_shjj b,dz_image_shjj i,dz_input_shjj p where lower(b.barcode)=i.barcode and p.imageid=i.imageid
   //order by p.imageid desc
            return View();
        }
    }
}
