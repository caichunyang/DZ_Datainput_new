using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DZ.EMS.Controllers
{
    public class AnnouncementController : Controller
    {
        //
        // GET: /Announcement/

        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult update15_16output()
        {
            BLL.DZ_INPUT_EMS_BLL bllinput = new BLL.DZ_INPUT_EMS_BLL();
            BLL.DZ_ShowOutPut_BLL outbll = new BLL.DZ_ShowOutPut_BLL();
           var list= bllinput.LoadEntities();
           foreach (var item in list)
           {
               outbll.updateoutputems15_16(int.Parse(item[3].ToString()), item[0], item[1], item[2]);
           }
            return Content("1");
        }
    }
}
