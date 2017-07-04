using DZ.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DZ.EMS.Controllers
{
    public class ManagementController : Controller
    {
        //
        // GET: /Management/

        public ActionResult Index()
        {
            MODEL.SessionModel session = Session["user"] as SessionModel;
            if (session != null)
            {
                var list = session.ModuleList.Where(a => a.Controler == "management" && a.Isview == 1 && a.Disablestatus == 0);//Disablestatus!=0 表示顶部菜单
                ViewBag.tabs = list;
                return View();
            }
            else
            {
                return RedirectToAction("login", "home");
            }
        }
        public ActionResult MyCenterUserManagement()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult my_LoadUserGridview(string userid, int page, int rows)
        {
            BLL.DZ_USER_BLL bll = new BLL.DZ_USER_BLL();
            SessionModel model = Session["user"] as SessionModel;//
            if (model != null)
            {
                string where = " t.userid not in (select userid from dz_userrole where roleid=1)";
                List<DZ_USER> userlist = bll.LoadEntities(model.User.Objectid.ToString(), userid, rows, page, where);
                int count = bll.selectpagecount(model.User.Objectid.ToString(), userid);

                var obj = from a in userlist
                          select new
                          {
                              a.Userid,
                              a.Username,
                              a.Userpwd,
                              a.Groupname,
                              a.Objectid,
                              a.Roleid,
                              state = a.state == 0 ? "√" : "×"
                          };
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"total\":" + count + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
                //return Content(Common.JSON.SetJson(obj));
                return Content(sb.ToString());
            }
            else
            {
                return Content("");
            }
        }
        /// <summary>
        /// 中心员工管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CenterUserManagement()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult LoadUserGridview(string objid, string userid, int page, int rows)
        {
            BLL.DZ_USER_BLL bll = new BLL.DZ_USER_BLL();
            if (objid == "*")
            {
                objid = "";
            }
            List<DZ_USER> userlist = bll.LoadEntities(objid, userid, rows, page, "");
            int count = bll.selectpagecount(objid, userid);

            var obj = from a in userlist
                      select new
                      {
                          a.Userid,
                          a.Username,
                          a.Userpwd,
                          a.Groupname,
                          a.Objectid,
                          a.Roleid,
                          state = a.state == 0 ? "√" : "×"
                      };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + count + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        [AllowAnonymous]
        public ActionResult UpdateUser(DZ_USER model)
        {
            BLL.DZ_USER_BLL bll = new BLL.DZ_USER_BLL();
            bool result = bll.UpdateUserInfo(model);
            return Content(result.ToString());
        }
        #region 韵达面单查询


        /// <summary>
        /// 查询韵达图片
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectImage_yd()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetInputAndImage_yd(string imageid, string filename, string userid, string values1, string startdate, string inputstate, string enddate, int page, int rows)
        {
            values1 = System.Web.HttpUtility.UrlDecode(values1);
            BLL.DZ_IMAGE_YD_BLL bll = new BLL.DZ_IMAGE_YD_BLL();
            string where = "";
            if (!string.IsNullOrEmpty(imageid))
            {
                where += " and i.imageid=" + imageid;
            }
            if (!string.IsNullOrEmpty(filename))
            {
                where += " and i.filename like '" + filename + "%'";
            }
            if (!string.IsNullOrEmpty(userid))
            {
                where += " and t.userid1 like '%" + userid + "%'";
            }
            if (!string.IsNullOrEmpty(values1))
            {
                where += " and t.values1 like '%" + values1 + "%'";
            }
            if (!string.IsNullOrEmpty(inputstate) && inputstate != "-1")
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

        [AllowAnonymous]
        public ActionResult GetImage_yd(string date, string boxno, string imageid, string filetype)
        {
            string path = "";
            try
            {
                // if (DateTime.Parse(date) >= DateTime.Parse("2016-11-20 00:00:00"))
                if (long.Parse(boxno.Substring(0, 11)) >= 20161120204)
                {
                    //path = "http://114.215.16.58:81/image_/ImageYDLR/" + date + "/" + boxno + "/" + imageid + "." + filetype;
                    path = " http://dzimage.oss-cn-hangzhou.aliyuncs.com/ImageYDLR/" + date + "/" + boxno + "/" + imageid + "." + filetype;

                }
                else
                {
                    path = "/image_/ImageYDLR/" + date + "/" + boxno + "/" + imageid + "." + filetype;
                }


                //byte[] bytearray = Common.DealFile.getImageByte(path);
                //if (bytearray!=null)
                //{
                //    Response.ContentType = "image/jpeg";
                //    Response.BinaryWrite(bytearray);
                //}
                //else
                //{
                //    path = Server.MapPath("/Images/no_img_.jpg");
                //     bytearray = Common.DealFile.getImageByte(path);
                //    Response.BinaryWrite(bytearray);
                //}
                return Content(path);

            }
            catch (Exception ex)
            {
                string filepath = Server.MapPath("/Content") + "/dz_datainput.log";
                Common.WriteLog.Write2Log(ex, path, filepath);
                return null;
                // throw;
            }
        }
        #endregion
        #region 邮政面单查询


        /// <summary>
        /// 查询邮政图片
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectImage_ems()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetInputAndImage_ems(string imageid, string guid, int filedtype, string userid, string values1, string startdate, string inputstate, string enddate, int page, int rows)
        {
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
            if (!string.IsNullOrEmpty(inputstate) && inputstate != "-1")
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
        public ActionResult GetImage_ems(string date, string boxno, string guid, string imagetype)
        {
            string path = "";
            try
            {
                if (DateTime.Parse(date) >= DateTime.Parse("2016-11-21 00:00:00"))
                {
                    path = " http://dzimage.oss-cn-hangzhou.aliyuncs.com/ImageYZLR/" + date + "/" + boxno + "/" + guid + "." + imagetype;
                }
                else
                {
                    path = "/image_/ImageYZLR/" + date + "/" + boxno + "/" + guid + "." + imagetype;
                }

                //path = Server.MapPath(path);
                //// path = "d:\\h2.png";
                ////if (date.Split('-')[2] == "30")
                ////{
                ////    path = "d:\\QQ截图20161031141149.png";
                ////}
                //string filepath = Server.MapPath("/Content") + "/dz_datainput.log";
                //Common.WriteLog.Write2Log(new Exception("myerror"), path, filepath);
                //byte[] bytearray = Common.DealFile.getImageByte(path);
                //if (bytearray != null)
                //{
                //    Response.ContentType = "image/jpeg";
                //    Response.BinaryWrite(bytearray);
                //}
                //else
                //{
                //    path = Server.MapPath("/Images/no_img_.jpg");
                //    bytearray = Common.DealFile.getImageByte(path);
                //    Response.BinaryWrite(bytearray);
                //}
                return Content(path);

            }
            catch (Exception ex)
            {
                string filepath = Server.MapPath("/Content") + "/dz_datainput.log";
                Common.WriteLog.Write2Log(ex, path, filepath);
                return null;
                // throw;
            }
        }
        #endregion
        #region 上海捷记
        public ActionResult recheck_shjj()
        {
            return View();
        }
        /// <summary>
        /// 启动shjj 该批次再次质检
        /// </summary>
        /// <returns></returns>

        public JsonResult StartReCheck(string boxno)
        {
            SessionModel model = Session["user"] as SessionModel;
            if (model != null)
            {
                BLL.DZ_INPUT_SHJJ_BLL bll = new BLL.DZ_INPUT_SHJJ_BLL();
                string result = bll.RestartCheck2_shjj(boxno).ToString();
                return Json(new { statu = 1, msg = "请登录", result = result });

            }
            else
            {
                return Json(new { statu = 0, msg = "请登录", result = 0 });
            }

        }
        #endregion
        
        [AllowAnonymous ]
        [HttpGet]
        public JsonResult RecvMonitor(string id)
        {
            return Json(new { statu = 1, msg = "测试成功！", result = id },JsonRequestBehavior.AllowGet);
        }

    }
}
