using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DZ.MODEL;

namespace DZ.EMS.Controllers
{
    public class NavRightController : Controller
    {
        [AllowAnonymous]
        public ActionResult GetLeftNavJson1()
        {
            var count = HttpContext.Application["onLine"];
            ViewBag.count = count;
            MODEL.SessionModel session = Session["user"] as SessionModel;
            if (session != null)
            {
                string[] proname = new string[] { "A组","邮政", "平安", "安能","捷记","EMS" };
                List<LeftNav> navlist = new List<LeftNav>();
                List<MODEL.DZ_MODULE> list = new List<DZ_MODULE>();
                var list1 = session.ModuleList.Where(a => a.Controler == "showoutput" && a.Isview == 1).ToList();
                var list2 = session.ModuleList.Where(a => a.Controler == "exceloutcenter" && a.Isview == 1).ToList();
                var list3 = session.ModuleList.Where(a => a.Controler == "selectinput" && a.Isview == 1).ToList();
                list.AddRange(list1);
                list.AddRange(list2);
                list.AddRange(list3);
                foreach (string name in proname)
                {
                    LeftNav modle = new LeftNav();
                  //  modle.menuid = name;
                    modle.menuname = name;
                 //   modle.icon = "icon-nav";
                    var listnav = list.Where(a => a.Title.Contains(name));
                    //if (name=="邮政捷记")
                    //{
                    //    listnav = list.Where(a => a.Title.Contains("邮政") || a.Title.Contains("捷记"));
                    //}
                   
                    List<LeftNav> navlist2 = new List<LeftNav>();
                    foreach (var item in listnav)
                    {
                        LeftNav modle2 = new LeftNav();
                     //   modle2.menuid = item.Moduleid.ToString();
                        modle2.url = "/" + item.Controler + "/" + item.Action;
                        modle2.menuname = item.Title;
                      //  modle2.icon = "icon-nav";
                        navlist2.Add(modle2);
                    }
                    modle.menus = navlist2;
                    if (listnav.Count()>0)
                    {
                        navlist.Add(modle);
                    }
                   
                }
                return Content(Common.JSON.SetJson(navlist));

            }
            return null;
        }

        [AllowAnonymous]
        public ActionResult GetLeftNavJson_manager()
        {
            //var count = HttpContext.Application["onLine"];
            //ViewBag.count = count;
            MODEL.SessionModel session = Session["user"] as SessionModel;
            if (session != null)
            {
                // string[] proname = new string[] { "A组", "邮政","平安", "安能" };
                List<LeftNav> navlist = new List<LeftNav>();
               var list = session.ModuleList.Where(a => a.Controler == "management" && a.Disablestatus == 0 && a.Isview == 1).ToList();
               // var list = session.ModuleList.Where(a => a.Controler == "management").ToList();
                var main_navobj = session.ModuleList.Where(a => a.Controler == "management" && a.Disablestatus != 0 && a.Isview == 1).SingleOrDefault();
                LeftNav modle = new LeftNav();
                if (main_navobj != null)
                {
                    modle.menuid = main_navobj.Moduleid.ToString();
                    modle.menuname = main_navobj.Title;
                    modle.icon = "icon-nav";
                }
                List<LeftNav> navlist2 = new List<LeftNav>();
                foreach (var item in list)
                {
                    LeftNav modle2 = new LeftNav();
                    modle2.menuid = item.Moduleid.ToString();
                    modle2.url = "/" + item.Controler + "/" + item.Action;
                    modle2.menuname = item.Title;
                    modle2.icon = "icon-nav";
                    navlist2.Add(modle2);
                }
                modle.menus = navlist2;
                navlist.Add(modle);
                var list2 = session.ModuleList.Where(a => a.Disablestatus > 0 && a.Isview == 1&&a.Modulename.Contains("system"));
              List<LeftNav> scondlist= new List<LeftNav>();
                foreach (var item in list2)
                {
                    scondlist.Add(new LeftNav() { menuname = item.Title, icon = "icon-nav", url = "/" + item.Controler + "/" + item.Action});
                }
                if (scondlist.Count>0)
                {
                    navlist.Add(new LeftNav()
                    {
                        menuid = "",
                        menuname = "系统管理",
                        icon = "icon-nav",
                        menus = scondlist
                    });
                }
              
                return Content(Common.JSON.SetJson(navlist));

            }
            return null;
        }
    }
}
