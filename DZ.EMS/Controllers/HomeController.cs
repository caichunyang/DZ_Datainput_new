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
        public ActionResult Login_(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.tips = "没有权限";
            }
            return View();
        }
        [AllowAnonymousAttribute]
        public ActionResult Login(string id)
        {
            //if (!string.IsNullOrEmpty(id))
            //{
            //    ViewBag.tips = "没有权限";
            //}
            return View();
        }
        [AllowAnonymous]
        public ActionResult Error()
        {
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

            if (Session["user"] != null)
            {
                SessionModel session = Session["user"] as SessionModel;
                ViewBag.usfgdfd = session.User.Userid;
                ViewBag.dsfsdg = session.User.Userpwd;
                //List<DZ_MODULE> newlist = session.ModuleList.Where(a => a.Controler == "check").ToList<MODEL.DZ_MODULE>();
                /////数据质检图标显示与否
                //if (newlist != null && newlist.Count > 0)
                //{
                //    string str = Common.JSON.SetJson(newlist);
                //    //ViewBag.checkenable = "/" + bb.Controler + "/" + bb.Action;
                //    ViewBag.checkenable = str;
                //}
                //else
                //{
                //    ViewBag.checkenable = "";
                //}
                /////数据导出图标显示与否
                //List<DZ_MODULE> dataoutlist = session.ModuleList.Where(a => a.Controler == "exceloutcenter" && a.Action == "exceloutcenter").ToList<MODEL.DZ_MODULE>();
                //if (dataoutlist != null && dataoutlist.Count > 0)
                //{
                //    // string str = Common.JSON.SetJson(dataoutlist);
                //    //ViewBag.checkenable = "/" + bb.Controler + "/" + bb.Action;
                //    ViewBag.dataoutenable = "/exceloutcenter/exceloutcenter";
                //}
                //else
                //{
                //    ViewBag.dataoutenable = "";
                //}
                /////查错查询图标显示与否
                //List<DZ_MODULE> usererrorlist = session.ModuleList.Where(a => a.Controler == "selectinput" && a.Action == "index").ToList<MODEL.DZ_MODULE>();
                //if (usererrorlist != null && usererrorlist.Count > 0)
                //{
                //    // string str = Common.JSON.SetJson(dataoutlist);
                //    //ViewBag.checkenable = "/" + bb.Controler + "/" + bb.Action;
                //    ViewBag.SelectErrEnable = "/selectinput/index";
                //}
                //else
                //{
                //    ViewBag.SelectErrEnable = "";
                //}
                return View();
            }
            else
            {
                //return RedirectToAction("login");
                //return RedirectToAction("login", "home");
                // return View("login");
                return RedirectToAction("login", "home");
                //return View();
            }

        }

        [AllowAnonymous]
        public ActionResult CheckuserAndPwd(string userid, string pwd)
        {
            BLL.DZ_USER_BLL userbl = new DZ_USER_BLL();
            DZ_USER user = userbl.CheckUserAndPwd(userid, pwd);
            if (user != null)
            {
                if (user.state==0)
                {
                    var obj = new { userid = user.Userid, username = user.Username, state = user.state, group = user.Groupname };
                    //Request.Cookies("DataInput");
                    HttpCookie cookie = new HttpCookie("DataInput");
                    cookie.Values.Add("username", user.Username);
                    cookie.Values.Add("logintime", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    cookie.Values.Add("checknum", "0");
                    cookie.Values.Add("inputnum", "0");
                    cookie.Values.Add("clientkey", user.Username);
                    Response.Cookies.Add(cookie); 
                    return Content("{\"statu\":1,\"msg\":\"验证通过\",\"data\":" + Common.JSON.SetJson(obj) + "}");
                }
                else
                {
                    return Content("{\"statu\":0,\"msg\":\"帐号被禁用\"}");
                }
                
            }
            else
            {
                return Content("{\"statu\":0,\"msg\":\"用户名或密码错误\"}");
            }
        }
        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult loginckeck(string username, string password, string code)
        {
            //if (!string.IsNullOrEmpty(code))
            //{
            //if (code.Equals(Session["ServerCode"].ToString()))
            //{
            //  int b = int.Parse("");
            BLL.DZ_USER_BLL userbl = new DZ_USER_BLL();
            BLL.DZ_Object_BLL objbll = new DZ_Object_BLL();
            DZ_USER user = userbl.CheckUserAndPwd(username, password);
            if (user != null)
            {
                if (user.state == 0)
                {
                    List<DZ_MODULE> modulelist = userbl.GetMuduleByUserId(user.Userid);
                    string objname = objbll.GetAllList().Where(a => a[0] == user.Objectid.ToString()).SingleOrDefault()[1];
                    MODEL.SessionModel tempmodel = new SessionModel() { User = user, objname = objname };
                    tempmodel.ModuleList = modulelist;
                    Session["user"] = tempmodel;
                    // return RedirectToAction("index");
                    //return Content("/home/index");
                    return Content("{\"statu\":" + 1 + ",\"msg\":\"/homenew/index\"}");

                }
                else
                {
                    return Content("{\"statu\":" + 2 + ",\"msg\":\"该用户已被禁用!\"}");
                }
            }
            else
            {
                return Content("{\"statu\":" + 2 + ",\"msg\":\"用户名或密码错误!\"}");
            }
            //}
            //else
            //{
            //    return Content("{\"statu\":"+0+",\"msg\":\"验证码错误!\"}");
            //}
            //}
            //return Content("{\"statu\":" + 0 + ",\"msg\":\"请录入验证码!\"}");

            // return Content("/home/login");
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
