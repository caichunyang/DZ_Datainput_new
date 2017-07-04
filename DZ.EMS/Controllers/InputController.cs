using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DZ.BLL;
using DZ.MODEL;

namespace DZ.EMS.Controllers
{
    public class InputController : Controller
    {
        //
        // GET: /Input/
        /// <summary>
        /// 识图录入url据权限跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult url()
        {

            if (Session["user"]!=null)
            {
                SessionModel session = Session["user"] as SessionModel;
                List<DZ_MODULE> newlist = session.ModuleList.Where(a => a.Controler == "input").ToList<DZ_MODULE>();

                if (newlist != null)
                {

                    string str = Common.JSON.SetJson(newlist);
                    return Content(str);
                     //Response.Redirect("/input/"+single.Action);
                  // return  RedirectToAction(single.Action, single.Controler);
                   //return Content("/input/"+single.Action);
                }
                else
                {
                    return Content("");
                }
            }
            else
            {
               // return Content("/home/login");
                return RedirectToAction("login", "home");
            }
        }
        [AllowAnonymous]
        public ActionResult YD()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult YZ()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult inputimage()
        {
            MODEL.SessionModel session = Session["user"] as SessionModel;
            if (session!=null)
            {
                var list = session.ModuleList.Where(a => a.Controler == "input" &&a.Isview==1);
                ViewBag.tabs = list;
                return View();
            }
            else
            {
                return RedirectToAction("login", "home");
            }
        }
    }
}
