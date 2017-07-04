using DZ.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DZ.EMS.Controllers
{
    /// <summary>
    /// 差错查询页
    /// </summary>
    public class SelectInputController : Controller
    {
        //
        // GET: /SelectInput/
        [AllowAnonymous]
        public ActionResult Index()
        {

            MODEL.SessionModel session = Session["user"] as SessionModel;
            if (session != null)
            {
                var list = session.ModuleList.Where(a => a.Controler == "selectinput" && a.Isview == 1 && a.Disablestatus == 0);//Disablestatus!=0 表示顶部菜单
                ViewBag.tabs = list;
                return View();
            }
            else
            {
                return RedirectToAction("login", "home");
            }
        }
        #region 韵达面单查询 超管查询在管理页 差错查询


        /// <summary>
        /// 查询韵达图片 本用户
        /// </summary>
        /// <returns></returns>
        public ActionResult mySelect_yd()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetmyInputAndImage_yd(string imageid, string filename,string values1, string startdate,string enddate, int page, int rows)
        {
            string userid = "";
            string inputstate = "5";
          // string values1="<link>1";
            SessionModel model = Session["user"] as SessionModel;
            if (model != null)
            {
                userid = model.User.Userid;
            }
            values1 = System.Web.HttpUtility.UrlDecode(values1);
            BLL.DZ_IMAGE_YD_BLL bll = new BLL.DZ_IMAGE_YD_BLL();
            string where = "";
            if (!string.IsNullOrEmpty(imageid))
            {
                where += " and i.imageid=" + imageid;
            }
            if (!string.IsNullOrEmpty(filename))
            {
                where += " and i.filename = '" + filename + "'";
            }
            if (!string.IsNullOrEmpty(userid))
            {
                where += " and t.userid1 like '%" + userid + "%'";
            }
            if (!string.IsNullOrEmpty(values1))
            {
                where += " and t.values1 like '%" + values1 + "%'";
            }
            if (!string.IsNullOrEmpty(inputstate))
            {
                where += " and t.inputstate = " + inputstate;
            }
            //where += " and t.values1 <>  t.values3";
            
            if (!string.IsNullOrEmpty(startdate))
            {
                where += " and t.thedate1 >= to_date('" + startdate + " 00:00:00','yyyy-mm-dd HH24:MI:SS')";
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                where += " and t.thedate1 <=to_date('" + enddate + " 23:59:59','yyyy-mm-dd HH24:MI:SS')";
            }
            int count = 0;
            List<string[]> list = bll.SelectImage_YD(where, rows, page, out count);

            var obj = from a in list
                      select new
                      {
                          imageid = a[0],
                          inputname = a[1],
                          values1 = a[2],
                          values2 = a[3],
                          values3 = a[4],
                          inputstate = a[5],
                          userid1 = a[6],
                          userid2 = a[7],
                          userid3 = a[8],
                          thedate1 = a[9],
                          thedate2 = a[10],
                          thedate3 = a[11],

                          boxno = a[12],
                          imagedate = a[13],
                          imagestate = a[14],
                          imagetype = a[15],
                          filename = a[16],
                          outstate = a[17]
                      };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + count + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            string result = sb.ToString().Replace("<link>", "||");
            return Content(result);
        }

        /// <summary>
        /// 查询韵达图片 本中心
        /// </summary>
        /// <returns></returns>
        public ActionResult mycenterSelect_yd()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetCenterInputImage_yd(string imageid, string filename, string userid, string values1, string startdate,string enddate, int page, int rows)//string enddate,
        {
            string inputstate = "5";
            values1 = System.Web.HttpUtility.UrlDecode(values1);
            BLL.DZ_IMAGE_YD_BLL bll = new BLL.DZ_IMAGE_YD_BLL();
            SessionModel model = Session["user"] as SessionModel;
            int objid = -1;
            if (model != null)
            {
                objid = model.User.Objectid;
            }
            string where = "";
            if (objid>=0)
            {
                  where += " and u.objectid=" + objid;
            }
            if (!string.IsNullOrEmpty(imageid))
            {
                where += " and i.imageid=" + imageid;
            }
            if (!string.IsNullOrEmpty(filename))
            {
                where += " and i.filename = '" + filename + "'";
            }
            if (!string.IsNullOrEmpty(userid))
            {
                where += " and t.userid1 like '%" + userid + "%'";
            }
            if (!string.IsNullOrEmpty(values1))
            {
                where += " and t.values1 like '%" + values1 + "%'";
            }
            if (!string.IsNullOrEmpty(inputstate))
            {
                where += " and t.inputstate = " + inputstate;
            }
           // where += " and t.values1 <>  t.values3";
            if (!string.IsNullOrEmpty(startdate))
            {
               
                where += " and t.thedate1 >= to_date('" + startdate + " 00:00:00','yyyy-mm-dd HH24:MI:SS')";
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                where += " and t.thedate1 <=to_date('" + enddate + " 23:59:59','yyyy-mm-dd HH24:MI:SS')";
            }
            int count = 0;
            List<string[]> list = bll.SelectCenterImage_YD(where, rows, page, out count);

            var obj = from a in list
                      select new
                      {
                          imageid = a[0],
                          inputname = a[1],
                          values1 = a[2],
                          values2 = a[3],
                          values3 = a[4],
                          inputstate = a[5],
                          userid1 = a[6],
                          userid2 = a[7],
                          userid3 = a[8],
                          thedate1 = a[9],
                          thedate2 = a[10],
                          thedate3 = a[11],

                          boxno = a[12],
                          imagedate = a[13],
                          imagestate = a[14],
                          imagetype = a[15],
                          filename = a[16],
                          outstate = a[17]
                      };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + count + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            string result = sb.ToString().Replace("<link>", "||");
            return Content(result);
        }

        
        #endregion

        #region 邮政面单查询


        /// <summary>
        /// 查询邮政图片 本用户
        /// </summary>
        /// <returns></returns>
        public ActionResult mySelect_ems()
        {
            return View();
        }
        /// <summary>
        /// 查询邮政图片 本中心
        /// </summary>
        /// <returns></returns>
        public ActionResult mycenterSelect_ems()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetmyInputImage_ems(string imageid, string guid, int filedtype, string values1, string startdate, string enddate, int page, int rows)//, string userid , string enddate
        {
            string userid = "";
           
            string inputstate = "5";//被修改过的
            // string values1="<link>1";
            SessionModel model = Session["user"] as SessionModel;
            if (model != null)
            {
                userid = model.User.Userid;
            }
            values1 = System.Web.HttpUtility.UrlDecode(values1);
            BLL.DZ_IMAGE_EMS_BLL bll = new BLL.DZ_IMAGE_EMS_BLL();
            string where = "";
            if (!string.IsNullOrEmpty(imageid))
            {
                where += " and i.imageid=" + imageid;
            }
            if (!string.IsNullOrEmpty(guid))
            {
                where += " and i.guid = '" + guid + "'";
            }
            if (filedtype > 0)
            {
                where += " and i.filedtype =" + filedtype;
            }
            if (!string.IsNullOrEmpty(userid))
            {
                where += " and t.userid1 like '%" + userid + "%'";
            }
            if (!string.IsNullOrEmpty(values1))
            {
                where += " and t.values1 like '%" + values1 + "%'";
            }
           // where += " and t.values1 <>  t.values3";
            if (!string.IsNullOrEmpty(inputstate))
            {
                where += " and t.inputstate = " + inputstate;
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                where += " and t.thedate1 >= to_date('" + startdate + " 00:00:00','yyyy-mm-dd HH24:MI:SS')";
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                where += " and t.thedate1 <=to_date('" + enddate + " 23:59:59','yyyy-mm-dd HH24:MI:SS')";
            }
            int count = 0;
            List<string[]> list = bll.SelectImage_EMS(where, rows, page, out count);

            var obj = from a in list
                      select new
                      {
                          imageid = a[0],
                          inputname = a[1],
                          values1 = a[2],
                          values2 = a[3],
                          values3 = a[4],
                          inputstate = a[5],
                          userid1 = a[6],
                          userid2 = a[7],
                          userid3 = a[8],
                          thedate1 = a[9],
                          thedate2 = a[10],
                          thedate3 = a[11],

                          boxno = a[12],
                          imagedate = a[13],
                          imagestate = a[14],
                          imagetype = a[15],
                          guid = a[16],
                          outstate = a[17]
                      };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + count + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            string result = sb.ToString().Replace("<link>", "||");
            return Content(result);
        }

        [AllowAnonymous]
        public ActionResult GetCenterInputImage_ems(string imageid, string guid, int filedtype, string userid, string values1, string startdate, string enddate,int page, int rows)// string enddate,
        {
           
            string inputstate = "5";
            values1 = System.Web.HttpUtility.UrlDecode(values1);
            BLL.DZ_IMAGE_EMS_BLL bll = new BLL.DZ_IMAGE_EMS_BLL();
            SessionModel model = Session["user"] as SessionModel;
            int objid = -1;
            if (model != null)
            {
                objid = model.User.Objectid;
            }
            string where = "";
            if (objid>=0)
            {
                where += " and u.objectid=" + objid;
            }
            if (!string.IsNullOrEmpty(imageid))
            {
                where += " and i.imageid=" + imageid;
            }
            if (!string.IsNullOrEmpty(guid))
            {
                where += " and i.guid = '" + guid + "'";
            }
            if (filedtype > 0)
            {
                where += " and i.filedtype =" + filedtype;
            }
            if (!string.IsNullOrEmpty(userid))
            {
                where += " and t.userid1 like '%" + userid + "%'";
            }
            if (!string.IsNullOrEmpty(values1))
            {
                where += " and t.values1 like '%" + values1 + "%'";
            }
            if (!string.IsNullOrEmpty(inputstate))
            {
                where += " and t.inputstate = " + inputstate;
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                where += " and t.thedate1 >= to_date('" + startdate + " 00:00:00','yyyy-mm-dd HH24:MI:SS')";
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                where += " and t.thedate1 <=to_date('" + enddate + " 23:59:59','yyyy-mm-dd HH24:MI:SS')";
            }
            int count = 0;
            List<string[]> list = bll.SelectCenterImage_EMS(where, rows, page, out count);

            var obj = from a in list
                      select new
                      {
                          imageid = a[0],
                          inputname = a[1],
                          values1 = a[2],
                          values2 = a[3],
                          values3 = a[4],
                          inputstate = a[5],
                          userid1 = a[6],
                          userid2 = a[7],
                          userid3 = a[8],
                          thedate1 = a[9],
                          thedate2 = a[10],
                          thedate3 = a[11],

                          boxno = a[12],
                          imagedate = a[13],
                          imagestate = a[14],
                          imagetype = a[15],
                          guid = a[16],
                          outstate = a[17]
                      };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + count + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            string result = sb.ToString().Replace("<link>", "||");
            return Content(result);
        }

        #endregion
    }
}
