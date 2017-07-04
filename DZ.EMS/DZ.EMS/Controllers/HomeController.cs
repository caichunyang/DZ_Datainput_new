using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DZ.BLL;
using DZ.MODEL;


namespace DZ.EMS.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymousAttribute]
        public ActionResult Login(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.tips = "没有权限";
            }
            return View();
        }
        public ActionResult Index()
        {
            // ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            //DZ.DLL.Class1 sfs = new DLL.Class1();
            //sfs.sdfsd();
            //  DZ_IMAGE_EMS_BLL bll = new DZ_IMAGE_EMS_BLL();
            //int a=  bll.GetAllList().Count;
            //a = a + 1;
       
            if (Session["user"]!=null)
            {
                SessionModel session = Session["user"] as SessionModel;
                ViewBag.username = session.User.Username;
               List< DZ_MODULE> newlist = session.ModuleList.Where(a => a.Controler == "check").ToList<MODEL.DZ_MODULE>();
               if (newlist != null&&newlist.Count>0)
                {
                  string  str=  Common.JSON.SetJson(newlist);
                    //ViewBag.checkenable = "/" + bb.Controler + "/" + bb.Action;
                  ViewBag.checkenable = str;
                }
                else
                {
                    ViewBag.checkenable = "";
                }

            }
            else
            {
                return RedirectToAction("login");
            }
            return View();
        }
        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult loginckeck(string username, string password)
        {
            BLL.DZ_USER_BLL userbl = new DZ_USER_BLL();
            DZ_USER user = userbl.CheckUserAndPwd(username, password);
            if (user != null)
            { 
                List<DZ_MODULE> modulelist = userbl.GetMuduleByUserId(user.Userid);
                MODEL.SessionModel tempmodel = new SessionModel() { User = user };
                tempmodel.ModuleList = modulelist;
                Session["user"] = tempmodel;
               // return RedirectToAction("index");
                return Content("/home/index");
                //return new RedirectResult("/hone/index");
            }
            return Content("/home/login");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
