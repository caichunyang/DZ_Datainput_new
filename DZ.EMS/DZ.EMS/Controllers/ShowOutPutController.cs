using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DZ.Common;
using DZ.MODEL;
using System.Text;

namespace DZ.EMS.Controllers
{
    public class ShowOutPutController : Controller
    {
        public ActionResult Production_YD()
        {
            SessionModel model = Session["user"] as SessionModel;
            ViewBag.userid = model == null ? "" : model.User.Userid;
            return View();
        }
        public ActionResult Production_EMS()
        {
            SessionModel model = Session["user"] as SessionModel;
            ViewBag.userid = model == null ? "" : model.User.Userid;
            return View();
        }
        public ActionResult UserProduction()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetOutPutTJ(string startdate, string enddate, string casekey, string wherestr, string sort, string order)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            order = order ?? "asc";
            string orderstr = !string.IsNullOrEmpty(sort) ? " order by t." + sort + " " + order : "";
            var obj = from a in bll.SelectOutPut(startdate, enddate, casekey, wherestr, orderstr)
                      select new
                      {
                          userid = a[0],
                          thedate = a[1],
                          encode = a[2],
                          thenum = a[3],
                          lv = a[4],
                          integral = a[5],
                          checknum = a[6],
                          checkerrnum = a[7],
                          errnum = a[8],
                          backnum = a[9],
                      };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + obj.Count() + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        /// <summary>
        /// 用户本人产量查询
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="casekey"></param>
        /// <param name="wherestr"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetUserProduction(string startdate, string enddate, string sort, string order)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            order = order ?? "asc";
            string orderstr = !string.IsNullOrEmpty(sort) ? " order by t." + sort + " " + order : "";
            SessionModel model = Session["user"] as SessionModel;
            //ViewBag.userid = model == null ? "" : model.User.Userid;
            string wherestr = "t.userid='" + model.User.Userid+"'";
            var obj_yd = bll.SelectOutPut(startdate, enddate, "yd", wherestr, orderstr);
            var obj_ems = bll.SelectOutPut(startdate, enddate, "ems", wherestr, orderstr);
            List<string> datearry_yd = (from a in obj_yd select a[1]).ToList();
            List<string> datearry_ems = (from a in obj_ems select a[1]).ToList();


            List<int> collection1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            List<int> collection2 = new List<int>() { 1, 4, 5, 6, 7, 9 };
            var ExecptResult = collection1.Except(collection2);//差集
            var IntersectResult = collection1.Intersect(collection2);//交集
            var UnionResult = collection1.Union(collection2);//并集

            List<string> alldateArray = datearry_yd.Union(datearry_yd).ToList() ;
            //alldateArray.AddRange(datearry_yd);
            //alldateArray.AddRange(datearry_ems);
            List<string[]> resultlist = new List<string[]>();
            string[] sss = new string[5];
            var ss = sss[0] + sss[1];
            foreach (string dt in alldateArray)
            {
                string[] templist = new   string[20] ;
                var tempobj_ems = obj_ems.Where(a => a[1] == dt).FirstOrDefault();
                if (tempobj_ems!=null)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        templist[i] = tempobj_ems[i];
                    }
                }
                var tempobj_yd = obj_yd.Where(a => a[1] == dt).FirstOrDefault();
                if (tempobj_yd != null)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        templist[i + 10] = tempobj_yd[i];
                    }
                }
                resultlist.Add(templist);
            }
            var obj = from a in resultlist select new {
                userid = a[0]??"",
                thedate = a[1] ?? "",
                encode = a[2] ?? "",
                thenum = a[3] ?? "",
                lv = a[4] ?? "",
                integral = a[5] ?? "",
                checknum = a[6] ?? "",
                checkerrnum = a[7] ?? "",
                errnum = a[8] ?? "",
                backnum = a[9] ?? "",

                userid1 = a[10] ?? "",
                thedate1 = a[11] ?? "",
                encode1 = a[12] ?? "",
                thenum1 = a[13] ?? "",
                lv1 = a[14] ?? "",
                integral1 = a[15] ?? "",
                checknum1 = a[16] ?? "",
                checkerrnum1 = a[17] ?? "",
                errnum1 = a[18] ?? "",
                backnum1 = a[19] ?? ""
            };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + obj.Count() + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        /// <summary>
        /// 用户本人产量统计 未用
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="casekey"></param>
        /// <param name="wherestr"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetUserProductionTJ(string startdate, string enddate, string casekey, string wherestr, string sort, string order)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            order = order ?? "asc";
            string orderstr = !string.IsNullOrEmpty(sort) ? " order by t." + sort + " " + order : "";
            var obj = from a in bll.SelectOutPut(startdate, enddate, casekey, wherestr, orderstr)
                      select new
                      {
                          userid = a[0],
                          thedate = a[1],
                          encode = a[2],
                          thenum = a[3],
                          lv = a[4],
                          integral = a[5],
                          checknum = a[6],
                          checkerrnum = a[7],
                          errnum = a[8],
                          backnum = a[9],
                      };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + obj.Count() + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 各用户录入量统计页
        /// </summary>
        /// <returns></returns>
        public ActionResult useroutput()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetUseroutputTJ(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            var obj = from a in bll.UserOutPutTj(startdate, enddate) select new { count = a[0], name = a[1] };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + obj.Count() + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");


            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        /// <summary>
        /// 各中心录入量统计
        /// </summary>
        /// <returns></returns>
        public ActionResult centeroutput()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetCenteroutputTJ(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            var obj = from a in bll.CenterOutPutTJ(startdate, enddate) select new { id = a[0], centername = a[1], count = a[2] };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + obj.Count() + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        public ActionResult imagetongji_yd()
        {
            return View();
        }
        public ActionResult imagetongji_ems()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetImage_rc_up(string startdate, string enddate, string casekey)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            //array[0]表示接收image量 按天分组 array[1]表示上传image量 outstate==1 按天分组
            List<string[]>[] array = bll.Image_rc_upTJ(startdate, enddate, casekey);//string[] 长2
            List<string[]> newlist = new List<string[]>();//string[] 长3
            if (array[1].Count() == 0)
            {
                array[1] = new List<string[]>();
            }
            foreach (var item in array[0])
            {
                string[] arr = new string[3];
                string[] temp = array[1].Where(a => a[1] == item[1]).SingleOrDefault();
                arr[0] = item[0];
                arr[1] = temp == null ? "" : temp[0];
                arr[2] = item[1];
                newlist.Add(arr);
                // arr[1]=from a in array[1] where a[1]==item[1] sele
            }
            var obj = from a in newlist select new { count1 = a[0], count2 = a[1], datestr = a[2] };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + obj.Count() + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }

        [AllowAnonymous]
        public ActionResult ShowCenter()
        {
            MODEL.SessionModel session = Session["user"] as SessionModel;
            if (session != null)
            {
                var list = session.ModuleList.Where(a => a.Controler == "showoutput" && a.Isview == 1);
                ViewBag.tabs = list;
                return View();
            }
            else
            {
                return RedirectToAction("login", "home");
            }
        }
        [AllowAnonymous]
        public ActionResult GetData(string data)
        {

            Common.HttpHelper http = new HttpHelper();
            var result = http.HttpPost("http://119.97.244.109:9666/YunDaInput.aspx", data);
            return Content(result);
        }
    }
}
