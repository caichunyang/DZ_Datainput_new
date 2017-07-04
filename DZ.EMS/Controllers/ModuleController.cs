using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DZ.MODEL;
using DZ.Common;
using DZ.BLL;

namespace DZ.EMS.Controllers
{
    public class ModuleController : Controller
    {
        //
        // GET: /Module/
      
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取模块展示json
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetModuleGriedview()
        {
            BLL.DZ_MODULE_BLL bll = new BLL.DZ_MODULE_BLL();
            var list = bll.GetEntites();
            var newlist =from  a in list select new {a.iconCls,status=a.Isview>0?"P":"",a.Moduleaction,a.Moduleid,a.Modulename,a.Parentid,a.Title,a.Controler,a.Action,a.Disablestatus};
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + list.Count).Append(",\"rows\":" + Common.JSON.SetJson(newlist)).Append("}");

            return Content(sb.ToString());
        }
        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult UpdateModule(string json)
        {
            List<DZ_MODULE> list = Common.JSON.DeserializeObject<List<DZ_MODULE>>(json);
            BLL.DZ_MODULE_BLL bll = new BLL.DZ_MODULE_BLL();
            int i = 0;
            foreach (var item in list)
            {
                item.Action = item.Action.ToLower();
                item.Controler = item.Controler.ToLower();
                if (bll.Update(item))
                {
                    i++;
                }
                else
                {
                    bll.Add(item);
                    i++;
                }
            }
            return Content(i.ToString());
        }
        [AllowAnonymous]
        public ActionResult LoadSingleModule(int id)
        {
            BLL.DZ_MODULE_BLL bll = new BLL.DZ_MODULE_BLL();
           DZ_MODULE model= bll.LoadEntites("moduleid=" + id).SingleOrDefault();
           return Json(model);
        }
        [AllowAnonymous]
        public ActionResult GetModuleTreeGridjson(int id)
        {
            BLL.DZ_MODULE_BLL bll = new BLL.DZ_MODULE_BLL();
            var moduleidlist = from b in bll.GetRoleRightByRoleid(id) select b.Moduleid;
            var allmodule = bll.GetEntites();
            foreach (var item in allmodule)
            {
                item.checkedinfo = false;
                if (moduleidlist.Contains(item.Moduleid))
                {

                    item.checkedinfo = true;
                    item.iconCls = "icon-man";
                }
            }

            //  var tree = from a in allmodule select new { a.Moduleid, a.Modulename, checkedinfo = true, iconCls = "icon-man", a.Moduleaction, a.Controler, a.Action, a.Isview, a.Title };
            string resultjson = Common.JSON.SetJson(allmodule).Replace("checkedinfo", "checked");
            // return Json(resultjson);
            return Content(resultjson);
        }
        [AllowAnonymous]
        public ActionResult GetModuleTreejson(int id)
        {
            BLL.DZ_MODULE_BLL bll = new BLL.DZ_MODULE_BLL();
            var moduleidlist =( from b in bll.GetRoleRightByRoleid(id) select b.Moduleid).ToList();
            var allmodule = bll.GetEntites();
            foreach (var item in allmodule)
            {
                item.checkedinfo = false;
                if (moduleidlist.Contains(item.Moduleid))
                {
                    item.checkedinfo = true;
                    item.iconCls = "icon-man";
                }
            }
            var tree = from a in allmodule select new { id = a.Moduleid, text = a.Modulename, a.checkedinfo, iconCls = "icon-tip" };//, a.Moduleaction, a.Controler, a.Action, a.Isview, a.Title
              string resultjson = Common.JSON.SetJson(tree).Replace("checkedinfo", "checked");
            // return Json(resultjson);
            return Content(resultjson);
        }
    }
}
