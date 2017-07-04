using DZ.BLL;
using DZ.MODEL;
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
          
            SessionModel session = Session["user"] as SessionModel;
            ViewBag.model = session;
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
        [AllowAnonymous]
        public ActionResult ChangePsw(string userid, string oldpwd, string newpwd)
        {
            BLL.DZ_USER_BLL bll = new DZ_USER_BLL();
            if (string.IsNullOrEmpty(userid))
            {
                SessionModel session = Session["user"] as SessionModel;
                if (session != null)
                {
                    userid = session.User.Userid;
                }
                else
                {
                    return Content("{\"statu\":-1,\"msg\":\"登录已过期,请重新登录!\"}");
                }
            }

            if (bll.CheckUserAndPwd(userid, oldpwd)!=null)
            {
                if (bll.ChangePwd(userid,oldpwd, newpwd))
                {
                    Session["user"] = null;
                    //Session.Abandon();
                    return Content("{\"statu\":1,\"msg\":\"修改成功!\"}");
                }
                else
                {
                    return Content("{\"statu\":-1,\"msg\":\"修改出错 通知管理员!\"}");
                }
            }
            else
            {
                return Content("{\"statu\":-1,\"msg\":\"旧密码错误!\"}");
            }
        }
    }
}
