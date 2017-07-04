using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DZ.BLL;

namespace DZ.EMS.Controllers
{
    public class RoleModuleController : Controller
    {
        //
        // GET: /RoleModule/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult RoleRight()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetRoleTreejson()
        {
            DZ_ROLE_BLL bll = new DZ_ROLE_BLL();
            var modelist = bll.GetEntities();
            var tree = from a in modelist select new { id = a.roleid, text = a.rolename, iconCls = "icon-man" };
            return Json(tree);
        }
        [AllowAnonymous]
        public ActionResult UpdateRoleRight(int roleid, int moduleid, bool check)
        {
            DZ_ROLEMODULE_BLL bll = new DZ_ROLEMODULE_BLL();
            bool result =false;
            if (check)//=="True")
            {
                result = bll.AddRoleModule(roleid, moduleid);
            }
            else
            {
              result=  bll.DeleteRoleModule(roleid, moduleid);
            }
            return Content(result.ToString());
        }
    }
}
