using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DZ.Common;
using DZ.MODEL;
using System.Text;
using System.Threading;

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

        /// <summary>
        /// 员工邮政产量
        /// </summary>
        /// <returns></returns>
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
            string wherestr = "t.userid='" + model.User.Userid + "'";
            var obj_yd = bll.SelectOutPut(startdate, enddate, "yd", wherestr, orderstr);
            var obj_ems = bll.SelectOutPut(startdate, enddate, "ems", wherestr, orderstr);
            List<string> datearry_yd = (from a in obj_yd select a[1]).ToList();
            List<string> datearry_ems = (from a in obj_ems select a[1]).ToList();


            //List<int> collection1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            //List<int> collection2 = new List<int>() { 1, 4, 5, 6, 7, 9 };
            //var ExecptResult = collection1.Except(collection2);//差集
            //var IntersectResult = collection1.Intersect(collection2);//交集
            //var UnionResult = collection1.Union(collection2);//并集

            List<string> alldateArray = datearry_yd.Union(datearry_yd).ToList();
            //alldateArray.AddRange(datearry_yd);
            //alldateArray.AddRange(datearry_ems);
            List<string[]> resultlist = new List<string[]>();
            string[] sss = new string[5];
            var ss = sss[0] + sss[1];
            foreach (string dt in alldateArray)
            {
                string[] templist = new string[20];
                var tempobj_ems = obj_ems.Where(a => a[1] == dt);
                templist[0] = model.User.Userid;
                templist[1] = dt;
                if (tempobj_ems != null)
                {
                    int[] lenth_5 = new int[5];

                    foreach (var array in tempobj_ems)
                    {
                        lenth_5[0] += int.Parse(array[3]);
                        lenth_5[1] += int.Parse(array[6]);
                        lenth_5[2] += int.Parse(array[7]);
                        lenth_5[3] += int.Parse(array[8]);
                        lenth_5[4] += int.Parse(array[9]);
                    }

                    for (int i = 0; i < lenth_5.Length; i++)
                    {
                        templist[i + 2] = lenth_5[i].ToString();
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
            var obj = from a in resultlist
                      select new
                      {
                          userid = a[0] ?? "",
                          thedate = a[1] ?? "",
                          // encode = a[2] ?? "",
                          thenum = a[2] ?? "",
                          //lv = a[4] ?? "",
                          // integral = a[5] ?? "",
                          checknum = a[3] ?? "",
                          checkerrnum = a[4] ?? "",
                          errnum = a[5] ?? "",
                          backnum = a[6] ?? "",

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
        /// <summary>
        /// 韵达员工产量 查询退单接口
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetUserPro_back(string startdate, string enddate, string casekey, string wherestr)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            List<List<string>> entities = bll.SelectOutPut_back(startdate, enddate, casekey, wherestr);
            var obj = entities.Select(a => new { userid = a[0], date = a[1], value = a[2] });
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + obj.Count() + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
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
        [AllowAnonymous]
        public ActionResult ReCheckSHJJ()
        {
            string startdate = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            string endate = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            BLL.DZ_INPUT_SHJJ_BLL bll = new BLL.DZ_INPUT_SHJJ_BLL();
            var result = bll.RestartCheck(startdate, endate);
            return Content(result.ToString());
        }
        /// <summary>
        /// 韵达各中心录入量统计
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
        #region 韵达 图片接收上传统计

        /// <summary>
        /// 韵达 图片接收上传统计
        /// </summary>
        /// <returns></returns>
        public ActionResult imagetongji_yd()
        {
            return View();
        }
        public ActionResult imagetongji_ems()
        {
            return View();
        }
        /// <summary>
        /// 分时间段统计上传接收
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetImage_rc_up_Three(string start, string end)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            if (start != null && end != null)
            {
                string dtnowstr = DateTime.Now.ToString("yyyy-MM-dd");
                List<string> resultlist = new List<string>();
                if (DateTime.Now.CompareTo(DateTime.Parse(start + " 00:00:00")) > 0 && dtnowstr != start)
                {
                    List<string[]> histroylist = bll.GetRecordUpRecv_yd(start, end);
                    foreach (var item in histroylist)
                    {
                        resultlist.Add(item[0] + "_" + item[1]);
                    }
                    //查询表记录
                }
                if (DateTime.Now.CompareTo(DateTime.Parse(end + " 23:59:59")) < 0)
                {
                    string datestr = DateTime.Now.ToString("yyyy-MM-dd");
                    //查当日
                    var list = GethoursUpReclist(datestr);
                    // bll.GetInsertUpRecv_yd(datestr,string.Join("_", list));

                    list.Insert(0, dtnowstr);
                    resultlist.Add(string.Join("_", list));
                    // return Content(string.Join("_", list));
                }
                resultlist.Reverse();
                return Content(Common.JSON.SetJson(resultlist));
            }
            return Content("");

        }

        private List<string> GethoursUpReclist(string start)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            string[] result = new string[16];
            List<string> onlineuserid = bll.GetInputUserCount_yd();
            string online = onlineuserid == null ? "0" : onlineuserid.Count.ToString();
            DateTime dtnow = DateTime.Parse(start);
            string endate = ""; string startdate = "";
            string yestodaystr = dtnow.AddDays(-1).ToString("yyyy-MM-dd");
            startdate = yestodaystr + " 23:00:00";
            endate = yestodaystr + " 23:59:59";
            string[] temparray = bll.Image_rc_upTJ2(startdate, endate);
            result[0] = temparray[0];
            result[1] = temparray[1];
            startdate = start + " 00:00:00";
            endate = start + " 08:59:59";
            temparray = bll.Image_rc_upTJ2(startdate, endate);
            result[2] = temparray[0];
            result[3] = temparray[1];
            //startdate = start + " 08:00:00";
            //endate = start + " 08:59:59";
            //temparray = bll.Image_rc_upTJ2(startdate, endate);
            //result[4] = temparray[0];
            //result[5] = temparray[1];
            startdate = start + " 09:00:00";
            endate = start + " 16:59:59";
            temparray = bll.Image_rc_upTJ2(startdate, endate);
            result[4] = temparray[0];
            result[5] = temparray[1];

            startdate = start + " 17:00:00";
            endate = start + " 18:59:59";
            temparray = bll.Image_rc_upTJ2(startdate, endate);
            result[6] = temparray[0];
            result[7] = temparray[1];
            startdate = start + " 19:00:00";
            endate = start + " 19:59:59";
            temparray = bll.Image_rc_upTJ2(startdate, endate);
            result[8] = temparray[0];
            result[9] = temparray[1];
            startdate = start + " 20:00:00";
            endate = start + " 20:59:59";
            temparray = bll.Image_rc_upTJ2(startdate, endate);
            result[10] = temparray[0];
            result[11] = temparray[1];
            startdate = start + " 21:00:00";
            endate = start + " 21:59:59";
            temparray = bll.Image_rc_upTJ2(startdate, endate);
            result[12] = temparray[0];
            result[13] = temparray[1];
            startdate = start + " 22:00:00";
            endate = start + " 22:59:59";
            temparray = bll.Image_rc_upTJ2(startdate, endate);
            result[14] = temparray[0];
            result[15] = temparray[1];
            int recsum = 0;
            int upsum = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if (i % 2 == 0)
                {
                    recsum += int.Parse(result[i]);
                }
                else
                {
                    upsum += int.Parse(result[i]);
                }
            }
            string[] dailu_daichuan = bll.Image_dailu_daichuan(start + " 00:00:00", start + " 23:59:59");
            var list = result.ToList();
            list.Add(recsum.ToString());
            list.Add(upsum.ToString());
            list.Add(dailu_daichuan[0]);
            list.Add(dailu_daichuan[1]);
            list.Add(dailu_daichuan[2]);
            list.Add(online);
            return list;
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
            obj = obj.OrderBy(a => a.datestr);
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + obj.Count() + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        [AllowAnonymous]
        public ActionResult GetImage_rc_up23(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();

            if (startdate == enddate)
            {
                string datestr = DateTime.Parse(startdate).ToString("yyyy-MM-dd");
                BLL.DZ_Recive_BLL Recbll = new BLL.DZ_Recive_BLL();
                var model = Recbll.GetModel(datestr, "yd");
                if (model != null)
                {
                    return Content("{ \"statu\": 1,\"rows\": [{\"count1\": \"" + model[1] + "\",\"count2\": \"" + model[2] + "\",\"datestr\": \"" + model[3] + "\"}]}");
                }
                else
                {
                    string startstr = DateTime.Parse(startdate).AddDays(-1).ToString("yyyyMMdd") + "230000";
                    string endstr = enddate.Replace("-", "") + "230000";
                    string[] arrayxx = bll.EveryMonthYD_TJ(startstr, endstr);//string[] 长3
                    bool result = Recbll.Insert(new string[] { datestr, arrayxx[0], arrayxx[1], arrayxx[2], "", "yd", "" });
                    return Content("{ \"total\": 1,\"rows\": [{\"count1\": \"" + arrayxx[0] + "\",\"count2\": \"" + arrayxx[1] + "\",\"datestr\": \"" + arrayxx[2] + "\"}]}");
                }
            }

            startdate = DateTime.Parse(startdate).AddDays(-1).ToString("yyyyMMdd") + "230000";
            enddate = enddate.Replace("-", "") + "230000";
            //array[0]表示接收image量 按天分组 array[1]表示上传image量 outstate==1 按天分组
            string[] array = bll.EveryMonthYD_TJ(startdate, enddate);//string[] 长3

            //var obj = new { count1 = array[0], count2 = array[1], count3 = array[2] };
            //StringBuilder sb = new StringBuilder();
            //sb.Append("{\"total\":" + 1 + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content("{ \"total\": 1,\"rows\": [{\"count1\": \"" + array[0] + "\",\"count2\": \"" + array[1] + "\",\"datestr\": \"" + array[2] + "\"}]}");
            // return Content(sb.ToString());
        }
        /// <summary>
        /// 查询接口  韵达23:00为接点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetYD_rc_up23(string id)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            BLL.DZ_Recive_BLL Recbll = new BLL.DZ_Recive_BLL();
            try
            {
                DateTime dtstart = DateTime.Parse(id);

                string datestr = dtstart.ToString("yyyy-MM-dd");
                if (datestr == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    return Content("{ \"statu\": 0,\"msg\":\"" + "只能查今天之前数据" + "\"}");
                }
                var model = Recbll.GetModel(datestr, "yd");
                //if (model != null)
                //{

                //    return Content("{ \"statu\": 1,\"rows\": [{\"count1\": \"" + model[1] + "\",\"count2\": \"" + model[2] + "\",\"count3\": \"" + model[3] + "\"}]}");
                //}
                //else
                //{
                string startdate = dtstart.AddDays(-1).ToString("yyyyMMdd") + "230000"; ;
                //   startdate = DateTime.Parse(startdate).AddDays(-1).ToString("yyyyMMdd") + "230000";
                string enddate = id.Replace("-", "") + "230000";
                //array[0]表示接收image量 按天分组 array[1]表示上传image量 outstate==1 按天分组
                string[] array = bll.EveryMonthYD_TJ(startdate, enddate);//string[] 长3
                //  bool result = Recbll.Insert(new string[] { datestr, array[0], array[1], array[2], "", "yd", "" });
                return Content("{ \"statu\": 1,\"rows\": [{\"count1\": \"" + array[0] + "\",\"count2\": \"" + array[1] + "\",\"count3\": \"" + array[2] + "\"}]}");
                // }
            }
            catch (Exception)
            {
                return Content("{ \"statu\": 0,\"msg\":\"" + "参数错误" + "\"}");
            }
            //   string startdate = dtstart.AddDays(-1).ToString("yyyyMMdd") + "230000"; ;
            ////   startdate = DateTime.Parse(startdate).AddDays(-1).ToString("yyyyMMdd") + "230000";
            //   string enddate = id.Replace("-", "") + "230000";
            //   //array[0]表示接收image量 按天分组 array[1]表示上传image量 outstate==1 按天分组
            //   string[] array = bll.EveryMonthYD_TJ(startdate, enddate);//string[] 长3

            //   var obj = new { count1 = array[0], count2 = array[1], count3 = array[2] };
            //   StringBuilder sb = new StringBuilder();
            //   sb.Append("{\"total\":" + 1 + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));

            // return Content(sb.ToString());
        }
        /// <summary>
        /// 查询接口  韵达23:00为接点  整月整理
        /// </summary>
        /// <param name="id">该月第一天</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetYD_rc_up23_1(string id)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            BLL.DZ_Recive_BLL Recbll = new BLL.DZ_Recive_BLL();
            try
            {
                DateTime dtstart = DateTime.Parse(id);

                int startday = dtstart.Day;
              
                int days = DateTime.DaysInMonth(dtstart.Year, dtstart.Month);

                for (int i = startday; i <= days; i++)
                {
                    string datestr = dtstart.ToString("yyyy-MM-") + i.ToString("00");
                    string d1 = DateTime.Parse(datestr).AddDays(-1).ToString("yyyyMMdd") + "230000";
                    string d2 = datestr.Replace("-", "") + "230000";
                    string[] array = bll.EveryMonthYD_TJ(d1, d2);//string[] 长3
                   // string[] array = new string[] {"2","2","4" };
                    var model = Recbll.GetModel(datestr, "yd");
                    if (model != null)
                    {
                        if (int.Parse(array[0]) > int.Parse(model[1]))
                        {
                            Recbll.Update(new string[] { datestr, array[0], array[1], array[2], "", "yd", "" });
                        }
                     
                    }
                    else
                    {
                        // string startdate = dtstart.AddDays(-1).ToString("yyyyMMdd") + "230000";
                        // //   startdate = DateTime.Parse(startdate).AddDays(-1).ToString("yyyyMMdd") + "230000";
                        // string enddate = id.Replace("-", "") + "230000";
                        // //array[0]表示接收image量 按天分组 array[1]表示上传image量 outstate==1 按天分组
                        //array = bll.EveryMonthYD_TJ(startdate, enddate);//string[] 长3
                        bool result = Recbll.Insert(new string[] { datestr, array[0], array[1], array[2], "", "yd", "" });
                      //  return Content("{ \"statu\": 1,\"rows\": [{\"count1\": \"" + array[0] + "\",\"count2\": \"" + array[1] + "\",\"count3\": \"" + array[2] + "\"}]}");
                    }
                }
                 return Content("{ \"statu\": 1,\"msg\":\"" + "完成" + "\"}");
            }
            catch (Exception)
            {
                return Content("{ \"statu\": 0,\"msg\":\"" + "参数错误" + "\"}");
            }

        }

        #endregion
        #region 邮政 A组 逾期统计


        /// <summary>
        /// 韵达 图片接收上传统计 逾期统计
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        public ActionResult inputoverdue_yd()
        {
            return View();//yd 逾期2
        }
        public ActionResult inputoverdue_ems()
        {
            return View();//xx
        }
        [AllowAnonymous]
        public ActionResult inputoverdue()
        {
            return View();
        }
        /// <summary>
        /// A组综合查询
        /// </summary>
        /// <returns></returns>

        public ActionResult InputHours_record_yd()
        {
            return View();
        }

        public ActionResult InputHoursOverdue_yd()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetHoursOverdue(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            StringBuilder sb = new StringBuilder();
            List<string[]> resultarry = bll.SelectOverdue("yd", startdate, enddate);
            var obj = from a in resultarry select new { a0 = a[0], a1 = a[1], a2 = a[2], a3 = a[3], a4 = a[4] };
            sb.Append("{\"total\":" + 1 + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            return Content(sb.ToString());
        }
        [AllowAnonymous]
        public ActionResult GetHours_record(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            StringBuilder sb = new StringBuilder();

            List<string[]> resultarry = bll.SelectZhongHe_2table_yd(startdate, enddate);

            if (resultarry != null && resultarry.Count > 0)
            {
                sb.Append("{\"total\":" + resultarry.Count + ",\"rows\":[");
                foreach (var a in resultarry)
                {
                    sb.Append("{\"inputcount\":\"" + a[1] + "\",\"overdue\":\"" + a[2] + "\",\"recivnum\":\"" + a[3] + "\",\"uploadnum\":\"" + a[4] + "\",\"backnum\":\"" + a[5] + "\",\"checkerrnum\":\"" + a[6] + "\",\"datestr\":\"" + a[7] + "\",\"hours\":\"" + a[8] + "\",\"waitnum\":\"" + a[9] + "\"},");
                }
                sb.Remove(sb.Length - 1, 1).Append("]}");
            }
            // var obj = from a in resultarry select new {  inputcount = a[1], overdue = a[2], recivnum= a[3], uploadnum = a[4],backnum = a[5],checkerrnum = a[6],datestr = a[7],hours = a[8],waitnum = a[9]};
            //  var obj = from a in resultarry select new {  a1 = a[1], a2 = a[2], a3= a[3], a4 = a[4], a5 = a[5]};

            //  sb.Append("{\"total\":" + 1 + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            return Content(sb.ToString());
        }
        [AllowAnonymous]
        public ActionResult Getinputoverdue_yd(string startdate)//, string enddate
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            BLL.DZ_OVERDUE_BLL overbll = new BLL.DZ_OVERDUE_BLL();
            StringBuilder sb = new StringBuilder();
            string datenum = startdate.Replace("-", "");
            string[] resultarry = overbll.loadentity("yd", datenum);

            if (resultarry != null)
            {
                var obj = new { a0 = resultarry[1], a1 = resultarry[2], a2 = resultarry[3], a3 = resultarry[4], a4 = resultarry[5], a5 = resultarry[6], a6 = resultarry[7], a7 = resultarry[8], a8 = resultarry[11], a9 = resultarry[12] };
                sb.Append("{\"total\":" + 1 + ",\"rows\":[").Append(Common.JSON.SetJson(obj)).Append("]}");
                return Content(sb.ToString());
            }
            else
            {
                string[] array = bll.InputOverdueTJ_YD(startdate);
                overbll.myinsert("yd", datenum, array);
                var obj = new { a0 = array[0], a1 = array[1], a2 = array[2], a3 = array[3], a4 = array[4], a5 = array[5], a6 = array[6], a7 = array[7], a8 = array[8], a9 = array[9] };

                sb.Append("{\"total\":" + 1 + ",\"rows\":[").Append(Common.JSON.SetJson(obj)).Append("]}");
                return Content(sb.ToString());
            }
        }

        [AllowAnonymous]
        public ActionResult Getinputoverdue_yd2(string startdate)//, string enddate
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            BLL.DZ_OVERDUE_BLL overbll = new BLL.DZ_OVERDUE_BLL();
            StringBuilder sb = new StringBuilder();
            string datenum = startdate.Replace("-", "");
            string[] resultarry = overbll.loadentity2("yd", datenum);

            if (resultarry != null)
            {
                var obj = new
                {
                    a1 = resultarry[1],
                    a2 = resultarry[2],
                    a3 = resultarry[3],
                    a4 = resultarry[4],
                    a5 = resultarry[5],
                    a6 = resultarry[6],
                    a7 = resultarry[7],
                    a8 = resultarry[8],
                    a9 = resultarry[9],
                    a10 = resultarry[10],
                    a11 = resultarry[11],
                    a12 = resultarry[12],
                    a13 = resultarry[13],
                    a14 = resultarry[14],
                    a15 = resultarry[15],
                    a16 = resultarry[16],
                    a17 = resultarry[17]//,
                    //a18 = resultarry[18]
                };
                sb.Append("{\"total\":" + 1 + ",\"rows\":[").Append(Common.JSON.SetJson(obj)).Append("]}");
                return Content(sb.ToString());
            }
            else
            {
                Thread thr = new Thread(() =>
                {
                    string[] array = bll.InputOverdueTJ_YD2(startdate);
                    overbll.insert2("yd", datenum, array);
                });
                thr.IsBackground = true;
                thr.Start();

                //var obj = new { a0 = array[0], a1 = array[1], a2 = array[2], a3 = array[3], a4 = array[4], a5 = array[5], a6 = array[6], a7 = array[7], a8 = array[8], a9 = array[9] };
                //sb.Append("{\"total\":" + 1 + ",\"rows\":[").Append(Common.JSON.SetJson(obj)).Append("]}");
                //return Content(sb.ToString());
                return Content("{\"total\":" + 0 + "}");
            }
        }
        /// <summary>
        /// 逾期产量
        /// </summary>
        /// <param name="startdate"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Getinputoverdue_ems(string startdate)//, string enddate
        {
            //BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            ////array[0]表示接收image量 按天分组 array[1]表示上传image量 outstate==1 按天分组
            //string[] array = bll.InputOverdueTJ_EMS(startdate);//string[] 长2

            //var obj = new { a0 = array[0], a1 = array[1], a2 = array[2], a3 = array[3], a4 = array[4], a5 = array[5], a6 = array[6], a7 = array[7] };
            //StringBuilder sb = new StringBuilder();
            //sb.Append("{\"total\":" + 1 + ",\"rows\":[").Append(Common.JSON.SetJson(obj)).Append("]}");
            ////return Content(Common.JSON.SetJson(obj));
            //return Content(sb.ToString());


            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            BLL.DZ_OVERDUE_BLL overbll = new BLL.DZ_OVERDUE_BLL();
            StringBuilder sb = new StringBuilder();
            string datenum = startdate.Replace("-", "");
            string[] resultarry = overbll.loadentity("ems", datenum);

            if (resultarry != null)
            {
                var obj = new { a0 = resultarry[1], a1 = resultarry[2], a2 = resultarry[3], a3 = resultarry[4], a4 = resultarry[5], a5 = resultarry[6], a6 = resultarry[7], a7 = resultarry[8], a8 = resultarry[11], a9 = resultarry[2] };
                sb.Append("{\"total\":" + 1 + ",\"rows\":[").Append(Common.JSON.SetJson(obj)).Append("]}");
                return Content(sb.ToString());
            }
            else
            {
                string[] array = bll.InputOverdueTJ_EMS(startdate);
                overbll.myinsert("ems", datenum, array);
                var obj = new { a0 = array[0], a1 = array[1], a2 = array[2], a3 = array[3], a4 = array[4], a5 = array[5], a6 = array[6], a7 = array[7], a8 = array[8], a9 = array[9] };

                sb.Append("{\"total\":" + 1 + ",\"rows\":[").Append(Common.JSON.SetJson(obj)).Append("]}");
                return Content(sb.ToString());
            }
        }
        #endregion
        /// <summary>
        /// 相当于母版页
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ShowCenter()
        {
            var count = HttpContext.Application["onLine"];
            ViewBag.count = count;
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

        /// <summary>
        /// 邮政各中心(各类型图片)接收量统计
        /// </summary>
        /// <returns></returns>
        public ActionResult CenterAnalysis_ems()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult CenterAnalysis_emsTJ(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            List<string[]> listarray = bll.center_recv_ems(startdate, enddate);
            List<string[]> newlistarray = new List<string[]>();
            int i = 0;
            string objctid = ""; string datestr = ""; int add_center = 0; int add_date = 0;
            foreach (var item in listarray)
            {
                string[] temparray = new string[7];
                if (i == 0)
                {
                    objctid = item[0];
                    datestr = item[3];//日期
                }
                for (int j = 0; j < item.Length; j++)
                {
                    temparray[j] = item[j];
                }
                if (item[0] == objctid)
                {
                    add_center += int.Parse(item[2]);
                }
                else
                {
                    objctid = item[0];
                    temparray[5] = add_center.ToString();
                    add_center = int.Parse(item[2]);
                }
                if (item[3] == datestr)
                {
                    add_date += int.Parse(item[2]);

                }
                else
                {
                    datestr = item[3];
                    temparray[6] = add_date.ToString();
                    add_date = int.Parse(item[2]);
                }
                if (i == listarray.Count - 1)
                {
                    temparray[5] = add_center.ToString();
                    temparray[6] = add_date.ToString();
                }
                newlistarray.Add(temparray);
                i++;
            }

            var obj = from a in newlistarray select new { id = a[0], centername = a[1], count = a[2], datestr = a[3], filetype = a[4], sum = a[5], sum2 = a[6] };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + obj.Count() + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        #region 员工产量查询分析
        /// <summary>
        /// 员工邮政产量分析  new
        /// </summary>
        /// <returns></returns>
        public ActionResult OutPutAnalysis_ems()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult OutPutAnalysis_emsTJ(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            SessionModel model = Session["user"] as SessionModel;
            string userid = model == null ? "" : model.User.Userid;
            List<string[]> listarray = bll.OutPutAnalysis_emsTJ(startdate, enddate, userid);
            //string[] filenametemplet = new string[] { 
            //    "寄件人姓名","寄件人电话","收件人姓名","收件人电话","收件人地址","混合字段","物品重量"};


            var datelist = listarray.Select(a => a[1]).Distinct();
            List<string[]> resularry = new List<string[]>();
            foreach (string date in datelist)
            {
                string[] rows = new string[14];
                rows[0] = userid;
                rows[1] = date;

                string[] temparry = new string[7] { "0", "0", "0", "0", "0", "0", "0" };
                int checknum = 0;
                int checkerrnum = 0;
                int errnum = 0;
                int backnum = 0;
                foreach (var item in listarray.Where(a => a[1] == date))
                {
                    if (item[2] == "寄件人姓名")
                    {
                        temparry[0] = item[3];
                    }
                    else if (item[2] == "寄件人电话")
                    {
                        temparry[1] = item[3];
                    }
                    else if (item[2] == "收件人姓名")
                    {
                        temparry[2] = item[3];
                    }
                    else if (item[2] == "收件人电话")
                    {
                        temparry[3] = item[3];
                    }
                    else if (item[2] == "收件人地址")
                    {
                        temparry[4] = item[3];
                    }
                    else if (item[2] == "混合字段")
                    {
                        temparry[5] = item[3];
                    }
                    else if (item[2] == "物品重量")
                    {
                        temparry[6] = item[3];
                    }

                    checknum += int.Parse(item[4]);
                    checkerrnum += int.Parse(item[5]);
                    errnum += int.Parse(item[6]);
                    backnum += int.Parse(item[7]);

                }

                int sum = 0;
                for (int i = 0; i < temparry.Length; i++)
                {
                    sum += int.Parse(temparry[i] ?? "0");
                    rows[2 + i] = temparry[i];
                }
                rows[9] = checknum.ToString();
                rows[10] = checkerrnum.ToString();
                rows[11] = errnum.ToString();
                rows[12] = backnum.ToString();
                rows[13] = sum.ToString();
                resularry.Add(rows);
            }

            var obj = from a in resularry
                      select new
                      {
                          id = a[0],
                          datestr = a[1],
                          type1 = a[2],
                          type2 = a[3],
                          type3 = a[4],
                          type4 = a[5],
                          type5 = a[6],
                          type6 = a[7],
                          type7 = a[8],
                          checknum = a[9],
                          checkerrnum = a[10],
                          errnum = a[11],
                          backnum = a[12],
                          sum = a[13],
                      };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + 1 + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }



        /// <summary>
        /// 员工韵达产量分析  new
        /// </summary>
        /// <returns></returns>
        public ActionResult OutPutAnalysis_yd()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult OutPutAnalysis_ydTJ(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            SessionModel model = Session["user"] as SessionModel;
            string userid = model == null ? "" : model.User.Userid;
            List<string[]> listarray = bll.OutPutAnalysis_ydTJ(startdate, enddate, userid);
            //string[] filenametemplet = new string[] { 
            //    "寄件人姓名","寄件人电话","收件人姓名","收件人电话","收件人地址","混合字段","物品重量"};

            var datelist = listarray.Select(a => a[1]).Distinct();
            List<string[]> resularry = new List<string[]>();
            foreach (string date in datelist)
            {
                string[] rows = new string[14];
                rows[0] = userid;
                rows[1] = date;

                string[] temparry = new string[7] { "0", "0", "0", "0", "0", "0", "0" };
                int checknum = 0;
                int checkerrnum = 0;
                int errnum = 0;
                int backnum = 0;
                foreach (var item in listarray.Where(a => a[1] == date))
                {
                    //if (item[2] == "寄件人姓名")
                    //{
                    //    temparry[0] = item[3];
                    //}
                    //else if (item[2] == "寄件人电话")
                    //{
                    //    temparry[1] = item[3];
                    //}
                    //else if (item[2] == "收件人姓名")
                    //{
                    //    temparry[2] = item[3];
                    //}
                    //else if (item[2] == "收件人电话")
                    //{
                    //    temparry[3] = item[3];
                    //}
                    //else if (item[2] == "收件人地址")
                    //{
                    //    temparry[4] = item[3];
                    //}
                    //else if (item[2] == "混合字段")
                    //{
                    //    temparry[5] = item[3];
                    //}
                    //else if (item[2] == "物品重量")
                    //{
                    //    temparry[6] = item[3];
                    //}
                    temparry[4] = item[2];
                    checknum += int.Parse(item[3]);
                    checkerrnum += int.Parse(item[4]);
                    errnum += int.Parse(item[5]);
                    backnum += int.Parse(item[6]);

                }

                int sum = 0;
                for (int i = 0; i < temparry.Length; i++)
                {
                    sum += int.Parse(temparry[i] ?? "0");
                    rows[2 + i] = temparry[i];
                }
                rows[9] = checknum.ToString();
                rows[10] = checkerrnum.ToString();
                rows[11] = errnum.ToString();
                rows[12] = backnum.ToString();
                rows[13] = sum.ToString();
                resularry.Add(rows);
            }

            var obj = from a in resularry
                      select new
                      {
                          id = a[0],
                          datestr = a[1],
                          type1 = a[2],
                          type2 = a[3],
                          type3 = a[4],
                          type4 = a[5],
                          type5 = a[6],
                          type6 = a[7],
                          type7 = a[8],
                          checknum = a[9],
                          checkerrnum = a[10],
                          errnum = a[11],
                          backnum = a[12],
                          sum = a[13],
                      };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + 1 + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        [AllowAnonymous]
        public ActionResult OutPutAnalysis_ydTJcallback(string startdate, string enddate, string userid)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            List<string[]> listarray = bll.OutPutAnalysis_ydTJ(startdate, enddate, userid);
            //string[] filenametemplet = new string[] { 
            //    "寄件人姓名","寄件人电话","收件人姓名","收件人电话","收件人地址","混合字段","物品重量"};

            var datelist = listarray.Select(a => a[1]).Distinct();
            List<string[]> resularry = new List<string[]>();
            foreach (string date in datelist)
            {
                string[] rows = new string[14];
                rows[0] = userid;
                rows[1] = date;
                string[] temparry = new string[7] { "0", "0", "0", "0", "0", "0", "0" };
                int checknum = 0;
                int checkerrnum = 0;
                int errnum = 0;
                int backnum = 0;
                foreach (var item in listarray.Where(a => a[1] == date))
                {
                    temparry[4] = item[2];
                    checknum += int.Parse(item[3]);
                    checkerrnum += int.Parse(item[4]);
                    errnum += int.Parse(item[5]);
                    backnum += int.Parse(item[6]);

                }

                int sum = 0;
                for (int i = 0; i < temparry.Length; i++)
                {
                    sum += int.Parse(temparry[i] ?? "0");
                    rows[2 + i] = temparry[i];
                }
                rows[9] = checknum.ToString();
                rows[10] = checkerrnum.ToString();
                rows[11] = errnum.ToString();
                rows[12] = backnum.ToString();
                rows[13] = sum.ToString();
                resularry.Add(rows);
            }

            var obj = from a in resularry
                      select new
                      {
                          id = a[0],
                          datestr = a[1],
                          type1 = a[2],
                          type2 = a[3],
                          type3 = a[4],
                          type4 = a[5],
                          type5 = a[6],
                          type6 = a[7],
                          type7 = a[8],
                          checknum = a[9],
                          checkerrnum = a[10],
                          errnum = a[11],
                          backnum = a[12],
                          sum = a[13],
                      };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + 1 + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content("callback(" + sb.ToString() + ")");
        }
        #endregion
        /// <summary>
        /// 本中心邮政产量 (分中心管理员)
        /// </summary>
        /// <returns></returns>
        public ActionResult MyCenterProduction_ems()
        {

            return View();
        }
        [AllowAnonymous]
        public ActionResult MyCenterProduction_emsTJ(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            SessionModel model = Session["user"] as SessionModel;
            string Objid = model == null ? "" : model.User.Objectid.ToString();

            List<string[]> listarray = bll.MyCenterProduction_ems(startdate, enddate, Objid);
            List<string> userlist = listarray.Select(a => a[3]).Distinct().ToList();
            //string[] filenametemplet = new string[] { 
            //    "寄件人姓名","寄件人电话","收件人姓名","收件人电话","收件人地址","混合字段","物品重量"};
            string[] resultarr = new string[8];


            //List<string[]> resultlist = new List<string[]>();
            List<object> resultlistx = new List<object>();
            foreach (string user_id in userlist)
            {

                var group = listarray.Where(a => a[3] == user_id);
                string[] temparry = new string[9] { "0", "0", "0", "0", "0", "0", "0", "0", "0" };

                // temparry[1] = "";
                string username = user_id;
                int checknum = 0;
                int checkerrnum = 0;
                int errnum = 0;
                int backnum = 0;
                int first = 0;
                foreach (var item in group)
                {
                    if (first == 0)
                    {
                        temparry[0] = item[0];
                        temparry[1] = item[1];
                        username = item[3];
                    }
                    if (item[2] == "寄件人姓名")
                    {
                        temparry[2] = item[4];
                    }
                    else if (item[2] == "寄件人电话")
                    {
                        temparry[3] = item[4];
                    }
                    else if (item[2] == "收件人姓名")
                    {
                        temparry[4] = item[4];
                    }
                    else if (item[2] == "收件人电话")
                    {
                        temparry[5] = item[4];
                    }
                    else if (item[2] == "收件人地址")
                    {
                        temparry[6] = item[4];
                    }
                    else if (item[2] == "混合字段")
                    {
                        temparry[7] = item[4];
                    }
                    else if (item[2] == "物品重量")
                    {
                        temparry[8] = item[4];
                    }

                    checknum += int.Parse(item[5]);
                    checkerrnum += int.Parse(item[6]);
                    errnum += int.Parse(item[7]);
                    backnum += int.Parse(item[8]);
                    first++;
                }
                int sum = 0;

                for (int i = 2; i < 9; i++)
                {
                    sum += int.Parse(temparry[i] == "" ? "0" : temparry[i]);
                }
                resultlistx.Add(new
                {
                    id = temparry[0],
                    type1 = temparry[1],
                    type2 = temparry[2],
                    type3 = temparry[3],
                    type4 = temparry[4],
                    type5 = temparry[5],
                    type6 = temparry[6],
                    type7 = temparry[7],
                    type8 = temparry[8],
                    username = username,
                    sum = sum,
                    checknum = checknum,
                    checkerrnum = checkerrnum,
                    errnum = errnum,
                    backnum = backnum
                });


            }

            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        /// <summary>
        /// 本中心韵达A组产量 (分中心管理员)
        /// </summary>
        /// <returns></returns>
        public ActionResult MyCenterProduction_yd()
        {

            return View();
        }
        [AllowAnonymous]
        public ActionResult MyCenterProduction_ydTJ(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            SessionModel model = Session["user"] as SessionModel;
            string Objid = model == null ? "" : model.User.Objectid.ToString();

            List<string[]> listarray = bll.MyCenterProduction_yd(startdate, enddate, Objid);
            //List<string> userlist = listarray.Select(a => a[3]).Distinct().ToList();
            string[] filenametemplet = new string[] { 
                "寄件人姓名","寄件人电话","收件人姓名","收件人电话","收件人地址","混合字段","物品重量"};
            string[] resultarr = new string[8];


            //List<string[]> resultlist = new List<string[]>();
            List<object> resultlistx = new List<object>();
            foreach (var temparry in listarray)
            {

                resultlistx.Add(new
                {
                    id = temparry[0],
                    type1 = temparry[1],
                    type2 = "0",
                    type3 = "0",
                    type4 = "0",
                    type5 = "0",
                    type6 = temparry[3],
                    type7 = "0",
                    type8 = "0",
                    username = temparry[2],
                    sum = temparry[3],
                    checknum = temparry[4],
                    checkerrnum = temparry[5],
                    errnum = temparry[6],
                    backnum = temparry[7]
                });
            }
            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        /// <summary>
        /// 查询A组员工A组产量 (中心管理员)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCenterProduction_yd()
        {

            return View();
        }
        [AllowAnonymous]
        public ActionResult SelectCenterProduction_ydTJ(string startdate, string enddate, string objectid, string userid)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            //SessionModel model = Session["user"] as SessionModel;
            //string Objid = model == null ? "" : model.User.Objectid.ToString();

            List<string[]> listarray = bll.SelectCenterProduction_yd(startdate, enddate, objectid, userid);
            //List<string> userlist = listarray.Select(a => a[3]).Distinct().ToList();
            string[] filenametemplet = new string[] { 
                "寄件人姓名","寄件人电话","收件人姓名","收件人电话","收件人地址","混合字段","物品重量"};
            string[] resultarr = new string[8];


            //List<string[]> resultlist = new List<string[]>();
            List<object> resultlistx = new List<object>();
            foreach (var temparry in listarray)
            {

                resultlistx.Add(new
                {
                    id = temparry[0],
                    type1 = temparry[1],
                    type2 = "0",
                    type3 = "0",
                    type4 = "0",
                    type5 = "0",
                    type6 = temparry[3],
                    type7 = "0",
                    type8 = "0",
                    username = temparry[2],
                    sum = temparry[3],
                    checknum = temparry[4],
                    checkerrnum = temparry[5],
                    errnum = temparry[6],
                    backnum = temparry[7]
                });
            }
            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        /// <summary>
        /// 各中心EMS产量 超级管理员
        /// </summary>
        /// <returns></returns>
        public ActionResult AllCenterProduction_ems()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult AllCenterProduction_emsTJ(string startdate, string enddate, string objectid)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            BLL.DZ_Object_BLL objbll = new BLL.DZ_Object_BLL();
            //SessionModel model = Session["user"] as SessionModel;
            //string userid = model == null ? "" : model.User.Userid;
            List<string> objlist = objbll.GetAllList().Where(a => a[4] == "0").Select(a => a[1]).ToList<string>();//录入中心名称列表 isscan

            List<string[]> listarray = bll.CenterProduction_ems(startdate, enddate, objectid);
            //string[] filenametemplet = new string[] { 
            //    "寄件人姓名","寄件人电话","收件人姓名","收件人电话","收件人地址","混合字段","物品重量"};

            List<string[]> resultlist = new List<string[]>();
            List<object> resultlistx = new List<object>();
            foreach (string objname in objlist)
            {

                var group = listarray.Where(a => a[1] == objname);
                string[] temparry = new string[9] { "0", "0", "0", "0", "0", "0", "0", "0", "0" };

                temparry[1] = objname;
                int checknum = 0;
                int checkerrnum = 0;
                int errnum = 0;
                int backnum = 0;
                foreach (var item in group)
                {
                    temparry[0] = item[0];
                    if (item[2] == "寄件人姓名")
                    {
                        temparry[2] = item[3];
                    }
                    else if (item[2] == "寄件人电话")
                    {
                        temparry[3] = item[3];
                    }
                    else if (item[2] == "收件人姓名")
                    {
                        temparry[4] = item[3];
                    }
                    else if (item[2] == "收件人电话")
                    {
                        temparry[5] = item[3];
                    }
                    else if (item[2] == "收件人地址")
                    {
                        temparry[6] = item[3];
                    }
                    else if (item[2] == "混合字段")
                    {
                        temparry[7] = item[3];
                    }
                    else if (item[2] == "物品重量")
                    {
                        temparry[8] = item[3];
                    }
                    checknum += int.Parse(item[4]);
                    checkerrnum += int.Parse(item[5]);
                    errnum += int.Parse(item[6]);
                    backnum += int.Parse(item[7]);
                }
                int sum = 0;

                for (int i = 2; i < 9; i++)
                {
                    sum += int.Parse(temparry[i] == "" ? "0" : temparry[i]);
                }
                if (sum != 0)
                {
                    resultlistx.Add(new
                    {
                        id = temparry[0],
                        type1 = temparry[1],
                        type2 = temparry[2],
                        type3 = temparry[3],
                        type4 = temparry[4],
                        type5 = temparry[5],
                        type6 = temparry[6],
                        type7 = temparry[7],
                        type8 = temparry[8],
                        sum = sum,
                        checknum = checknum,
                        checkerrnum = checkerrnum,
                        errnum = errnum,
                        backnum = backnum
                    });

                }
            }

            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        /// <summary>
        /// 本中心EMS上传单量
        /// </summary>
        /// <returns></returns>
        public ActionResult MyCenterBarcodeNum_ems()
        {
            return View();
        }
        /// <summary>
        /// ems中心单量查询
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="objectid">不传值表示查本中心</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CenterBarcodeNum_ems(string startdate, string enddate, string objectid)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            BLL.DZ_Object_BLL objbll = new BLL.DZ_Object_BLL();
            if (string.IsNullOrEmpty(objectid))
            {
                SessionModel model = Session["user"] as SessionModel;
                objectid = model == null ? "" : model.User.Objectid.ToString();
            }
            startdate += " 00:00:00"; enddate += " 23:59:59";
            List<string[]> listarray = bll.AllCenterBarcodeNum_ems(startdate, enddate, objectid);
            List<object> resultlistx = new List<object>();
            foreach (var temparry in listarray)
            {
                resultlistx.Add(new
                {
                    id = temparry[0],
                    type1 = temparry[1],
                    type2 = temparry[2],
                    type3 = temparry[3]
                });
            }
            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        /// <summary>
        /// 各中心韵达产量 超级管理员
        /// </summary>
        /// <returns></returns>
        public ActionResult AllCenterProduction_yd()
        {
            return View();
        }

        /// <summary>
        /// 本人韵达退单量 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult selfPro_back_yd(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            SessionModel model = Session["user"] as SessionModel;
            string userid = model == null ? "" : model.User.Userid;
            List<string[]> list = bll.CenterPro_back_yd(startdate, enddate, userid);
            var resultlistx = list.Select(a => new { id = a[0], value = a[1] }).ToList();
            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        /// <summary>
        /// 本中心韵达退单量 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult myCenterPro_back_yd(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            SessionModel model = Session["user"] as SessionModel;
            string Objid = model == null ? "" : model.User.Objectid.ToString();
            List<string[]> list = bll.CenterPro_back_yd(startdate, enddate, Objid);
            var resultlistx = list.Select(a => new { id = a[0], value = a[1] }).ToList();
            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        /// <summary>
        /// 各中心韵达退单量 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult AllCenterPro_back_yd(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            List<string[]> list = bll.CenterPro_back_yd(startdate, enddate, "*");
            var resultlistx = list.Select(a => new { id = a[0], value = a[1] }).ToList();
            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        [AllowAnonymous]
        public ActionResult AllCenterProduction_ydTJ(string startdate, string enddate, string objectid)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            BLL.DZ_Object_BLL objbll = new BLL.DZ_Object_BLL();
            //SessionModel model = Session["user"] as SessionModel;
            //string userid = model == null ? "" : model.User.Userid;
            //  List<string> objlist = objbll.GetAllList().Where(a => a[4] == "0").Select(a => a[1]).ToList<string>();//录入中心名称列表
            List<string[]> listarray = bll.CenterProduction_yd(startdate, enddate);
            //string[] filenametemplet = new string[] { 
            //    "寄件人姓名","寄件人电话","收件人姓名","收件人电话","收件人地址","混合字段","物品重量"};

            List<string[]> resultlist = new List<string[]>();
            List<object> resultlistx = new List<object>();
            foreach (var item in listarray)
            {

                // var item = listarray.Where(a => a[1] == objname).FirstOrDefault();
                string[] temparry = new string[9] { "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                temparry[1] = item[1];
                temparry[6] = item == null ? "0" : item[2];
                int sum = int.Parse(temparry[6]);

                //for (int i = 2; i < 9; i++)
                //{
                //    sum += int.Parse(temparry[i] == "" ? "0" : temparry[i]);
                //}
                if (sum != 0)
                {
                    resultlistx.Add(new
                    {
                        id = item[0],
                        type1 = temparry[1],
                        type2 = temparry[2],
                        type3 = temparry[3],
                        type4 = temparry[4],
                        type5 = temparry[5],
                        type6 = temparry[6],
                        type7 = temparry[7],
                        type8 = temparry[8],
                        sum = sum,
                        checknum = item == null ? "0" : item[3],
                        checkerrnum = item == null ? "0" : item[4],
                        errnum = item == null ? "0" : item[5],
                        backnum = item == null ? "0" : item[6]
                    });

                }


            }
            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        [AllowAnonymous]
        public ActionResult AllCenterPro_PaiHang_ydTJ(string startdate, string enddate, string objectid)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            List<string[]> listarray23 = bll.CenterProduction_yd("2016-12-23", "2016-12-23");//23日产量
            string yesdate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            List<string[]> listarray = bll.CenterProduction_yd(yesdate, yesdate);

            List<string[]> resultlist = new List<string[]>();
            foreach (var item in listarray23)
            {
                string[] array = new string[8];
                array[0] = item[0];//中心id
                array[1] = item[1];//中心名称
                array[2] = item[2];//中心产量23
                array[3] = (int.Parse(item[2]) * 1.5).ToString("f0");//目标产量
                var yesobj = listarray.Where(a => a[1] == item[1]).FirstOrDefault();
                array[4] = yesobj == null ? "0" : yesobj[2];//昨日产量
                array[5] = array[3] == "0" ? "0" : (float.Parse(array[4]) / int.Parse(array[3]) * 100).ToString("f2") + "%";//完成率
                resultlist.Add(array);
            }
            resultlist.Reverse();
            var obj = resultlist.Select(a => new { id = a[0], objname = a[1], type1 = a[2], type2 = a[3], type3 = a[4], type4 = a[5] });
            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(obj);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlist.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content("callback(" + sb.ToString() + ")");
        }

        [AllowAnonymous]
        public ActionResult SelectOnlinePersonAndInput(string id)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            string[] array = bll.SelectOnlinePersonAndInput(id, 3);
            string result = "{\"input\":" + array[0] + ",\"count\":" + array[1] + ",\"check\":" + array[2] + ",\"count1\":" + array[3] + "}";
            //return Content(Common.JSON.SetJson(obj));
            return Content("callback(" + result + ")");
        }

        [AllowAnonymous]
        public ActionResult AllCen_YD_TJ(int objid, int time_m)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            string[] array = bll.AllCen_YD_TJ(objid, time_m);
            string json = Common.JSON.SetJson(new { online = array[0], speed = array[1] });
            return Content(json);
        }

        #region 平安产量查询

        public ActionResult myProduction_PA()
        {
            return View();
        }
        public ActionResult myCenterProduction_PA()
        {
            return View();
        }
        public ActionResult ALLCenterProduction_PA()
        {
            return View();
        }
        public ActionResult ALLCenterProDetail_PA()
        {
            return View();
        }
        /// <summary>
        /// 本人平安产量统计
        /// </summary>
        [AllowAnonymous]
        public ActionResult myProduction_PATJ(string startdate, string enddate, int rows, int page)
        {

            BLL.DZ_PINGAN_BLL bll = new BLL.DZ_PINGAN_BLL();
            BLL.dz_output_bll outbll = new BLL.dz_output_bll();
            BLL.DZ_Dirctory_BLL dicbll = new BLL.DZ_Dirctory_BLL();
            SessionModel model = Session["user"] as SessionModel;
            string userid = model == null ? "" : model.User.Userid;
            string wherestr = "";
            if (userid != "")
            {
                wherestr += "and t.userid='" + userid + "'";
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                wherestr += "and t.inputime>to_date('" + startdate + " 00:00:00','yyyy-MM-dd HH24:MI:SS')";
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                wherestr += "and t.inputime<=to_date('" + enddate + " 23:59:59','yyyy-MM-dd HH24:MI:SS')";
            }
            int allcount = 0;
            List<string[]> modelist = bll.LoadPageEntities_PA(wherestr, rows, page, out allcount);
            Dictionary<string, float> tempdic = new Dictionary<string, float>();
            List<object> resultlistx = new List<object>();
            Random r = new Random();
            foreach (var temparry in modelist)
            {
                int right = outbll.GetRightlength(temparry[1], temparry[0]);
                if (right == -1)
                {
                    float hegelv;
                    if (!tempdic.TryGetValue(temparry[1].Split('-')[1], out hegelv))
                    {
                        var entity = dicbll.GetDictionaryEntities("pingan", "合格率", temparry[1].Split('-')[1]);
                        if (entity != null && entity.Count > 0)
                        {
                            hegelv = float.Parse(entity.SingleOrDefault()[2]);
                        }
                        tempdic.Add(temparry[1].Split('-')[1], hegelv);
                    }

                    float newhglv = 0;
                    newhglv = hegelv - (float)r.Next(0, 10) / 10 * 2 / 100;
                    right = (int)(int.Parse(temparry[2]) * newhglv + 0.5);
                    outbll.insert(temparry[1], temparry[0], newhglv, right);

                }
                var obj = new
                {
                    userid = temparry[0],
                    type1 = temparry[1],
                    type2 = temparry[2],
                    type3 = temparry[3],
                    type4 = right == -1 ? "" : right.ToString()
                };
                resultlistx.Add(obj);
            }
            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            sb.Append("{\"total\":" + allcount + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        /// <summary>
        /// 本中心平安产量统计
        /// </summary>
        [AllowAnonymous]
        public ActionResult myCenterProduction_PATJ(string startdate, string enddate, string userid, int rows, int page)
        {
            BLL.DZ_PINGAN_BLL bll = new BLL.DZ_PINGAN_BLL();
            BLL.DZ_Dirctory_BLL dicbll = new BLL.DZ_Dirctory_BLL();
            BLL.dz_output_bll outbll = new BLL.dz_output_bll();
            SessionModel model = Session["user"] as SessionModel;
            int objectid = model == null ? -1 : model.User.Objectid;
            string wherestr = "";
            if (objectid != -1)
            {
                wherestr += " and u.objectid=" + objectid;
            }
            if (!string.IsNullOrEmpty(userid))
            {
                wherestr += " and t.userid='" + userid + "'";
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                wherestr += "and t.inputime>to_date('" + startdate + " 00:00:00','yyyy-MM-dd HH24:MI:SS')";
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                wherestr += "and t.inputime<=to_date('" + enddate + " 23:59:59','yyyy-MM-dd HH24:MI:SS')";
            }
            int allcount = 0;
            List<string[]> modelist = bll.LoadPageEntities_PA(wherestr, rows, page, out allcount);
            Dictionary<string, float> tempdic = new Dictionary<string, float>();
            List<object> resultlistx = new List<object>();
            Random r = new Random();
            foreach (var item in modelist)
            {
                int right = -1;
                float newhglv = 0;
                right = outbll.GetRightlength(item[1], item[0]);
                float hegelv;
                if (!tempdic.TryGetValue(item[1].Split('-')[1], out hegelv))
                {
                    var entity = dicbll.GetDictionaryEntities("pingan", "合格率", item[1].Split('-')[1]);
                    if (entity != null && entity.Count > 0)
                    {
                        hegelv = float.Parse(entity.SingleOrDefault()[2]);
                    }
                    tempdic.Add(item[1].Split('-')[1], hegelv);

                }
                if (right <= -1 && hegelv != 0)
                {
                    newhglv = hegelv - (float)r.Next(0, 10) / 10 * 2 / 100;
                    right = (int)(int.Parse(item[2]) * newhglv + 0.5);
                    outbll.insert(item[1], item[0], newhglv, right);
                }
                var obj = new
                {
                    userid = item[0],
                    type1 = item[1],
                    type2 = item[2],
                    type3 = item[3],
                    type4 = right <= 0 ? "" : right.ToString(),
                    type5 = newhglv
                };
                resultlistx.Add(obj);
            }


            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            sb.Append("{\"total\":" + allcount + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }

        /// <summary>
        /// 各中心心平安产量统计
        /// </summary>
        [AllowAnonymous]
        public ActionResult AllCenterProduction_PATJ(string startdate, string enddate, int rows, int page)
        {
            BLL.DZ_PINGAN_BLL bll = new BLL.DZ_PINGAN_BLL();
            string wherestr = "";
            BLL.DZ_Dirctory_BLL dicbll = new BLL.DZ_Dirctory_BLL();
            //if (!string.IsNullOrEmpty(objectid) && objectid!="*")
            //{
            //    wherestr += " and u.objectid=" + objectid;
            //}
            //if (!string.IsNullOrEmpty(userid))
            //{
            //    wherestr += " and t.userid='" + userid + "'";
            //}
            if (!string.IsNullOrEmpty(startdate))
            {
                wherestr += "and t.inputime>to_date('" + startdate + " 00:00:00','yyyy-MM-dd HH24:MI:SS')";
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                wherestr += "and t.inputime<=to_date('" + enddate + " 23:59:59','yyyy-MM-dd HH24:MI:SS')";
            }
            int allcount = 0; float hegelv = 0;
            List<string[]> modelist = bll.LoadPageEntitiesAllCen_PA(wherestr, rows, page, out allcount);
            string[] datearray1 = startdate.Split('-');
            string[] datearray2 = enddate.Split('-');
            int days = DateTime.DaysInMonth(int.Parse(datearray1[0]), int.Parse(datearray1[1]));

            if (datearray1[2] == "01" && datearray2[1] == datearray1[1] && datearray2[2] == days.ToString())
            {
                var entity = dicbll.GetDictionaryEntities("pingan", "合格率", datearray1[1]);
                if (entity != null && entity.Count > 0)
                {
                    hegelv = float.Parse(entity.SingleOrDefault()[2]);
                }
            }


            List<object> resultlistx = new List<object>();
            foreach (var temparry in modelist)
            {
                var obj = new
                {
                    objid = temparry[0],
                    type1 = temparry[1],
                    type2 = temparry[2],
                    type3 = temparry[3],
                    type3_1 = hegelv == 0 ? "" : ((int)(int.Parse(temparry[2]) * hegelv)).ToString()
                };
                resultlistx.Add(obj);
            }

            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            sb.Append("{\"total\":" + allcount + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }

        /// <summary>
        /// 各中心心平安产量统计在线人数及提交速度
        /// </summary>
        [AllowAnonymous]
        public ActionResult AllCen_PA_TJ(int objid)
        {
            BLL.DZ_PINGAN_BLL bll = new BLL.DZ_PINGAN_BLL();
            string[] array = bll.AllCen_PA_TJ(objid, 5);
            string json = Common.JSON.SetJson(new { online = array[0], speed = array[1] });

            return Content(json);
        }


        /// <summary>
        /// 各中心平安产量统计 可查员工
        /// </summary>
        [AllowAnonymous]
        public ActionResult AllCenterProDetail_PATJ(string startdate, string enddate, string userid, int objectid, int rows, int page)
        {
            BLL.DZ_PINGAN_BLL bll = new BLL.DZ_PINGAN_BLL();
            SessionModel model = Session["user"] as SessionModel;
            // int objectid = model == null ? -1 : model.User.Objectid;
            string wherestr = "";
            if (objectid != -1)
            {
                wherestr += " and u.objectid=" + objectid;
            }
            if (!string.IsNullOrEmpty(userid))
            {
                wherestr += " and t.userid='" + userid + "'";
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                wherestr += "and t.inputime>to_date('" + startdate + " 00:00:00','yyyy-MM-dd HH24:MI:SS')";
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                wherestr += "and t.inputime<=to_date('" + enddate + " 23:59:59','yyyy-MM-dd HH24:MI:SS')";
            }
            int allcount = 0;
            List<string[]> modelist = bll.LoadPageEntities_PA(wherestr, rows, page, out allcount);
            List<object> resultlistx = new List<object>();
            foreach (var temparry in modelist)
            {
                var obj = new
                {
                    userid = temparry[0],
                    type1 = temparry[1],
                    type2 = temparry[2],
                    type3 = temparry[3]
                };
                resultlistx.Add(obj);
            }

            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            sb.Append("{\"total\":" + allcount + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }

        #endregion
        #region 上海捷记
        public ActionResult AllCenterPro_Shjj()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult AllCenterPro_ShjjTJ(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            BLL.DZ_Object_BLL objbll = new BLL.DZ_Object_BLL();
            //SessionModel model = Session["user"] as SessionModel;
            //string userid = model == null ? "" : model.User.Userid;
            //  List<string> objlist = objbll.GetAllList().Where(a => a[4] == "0").Select(a => a[1]).ToList<string>();//录入中心名称列表 isscan

            List<string[]> listarray = bll.AllCenterOutput_shjj(startdate, enddate);
            List<string> objlist = listarray.Select(a => a[0]).Distinct().ToList();//录入中心名称列表 isscan

            //string[] filenametemplet = new string[] { 
            //    "寄件人姓名","寄件人电话","收件人姓名","收件人电话","收件人地址","混合字段","物品重量"};

            List<string[]> resultlist = new List<string[]>();
            List<object> resultlistx = new List<object>();
            foreach (string objname in objlist)
            {

                var group = listarray.Where(a => a[0] == objname);
                string[] temparry = new string[9] { "0", "0", "0", "0", "0", "0", "0", "0", "0" };

                temparry[0] = objname;
                int checknum = 0;
                int checkerrnum = 0;
                int errnum = 0;
                int backnum = 0;
                foreach (var item in group)
                {
                    temparry[1] = item[1];
                    if (item[2] == "寄件人姓名")
                    {
                        temparry[2] = item[3];
                    }
                    else if (item[2] == "寄件人电话")
                    {
                        temparry[3] = item[3];
                    }
                    else if (item[2] == "收件人姓名")
                    {
                        temparry[4] = item[3];
                    }
                    else if (item[2] == "收件人电话")
                    {
                        temparry[5] = item[3];
                    }
                    else if (item[2] == "收件人地址")
                    {
                        temparry[6] = item[3];
                    }
                    else if (item[2] == "内件品名")
                    {
                        temparry[7] = item[3];
                    }
                    else if (item[2] == "总重量")
                    {
                        temparry[8] = item[3];
                    }
                    checknum += int.Parse(item[4]);
                    checkerrnum += int.Parse(item[5]);
                    errnum += int.Parse(item[6]);
                    backnum += int.Parse(item[7]);
                }
                int sum = 0;

                for (int i = 2; i < 9; i++)
                {
                    sum += int.Parse(temparry[i] == "" ? "0" : temparry[i]);
                }
                if (sum != 0)
                {
                    resultlistx.Add(new
                    {
                        id = temparry[0],
                        type1 = temparry[1],
                        type2 = temparry[2],
                        type3 = temparry[3],
                        type4 = temparry[4],
                        type5 = temparry[5],
                        type6 = temparry[6],
                        type7 = temparry[7],
                        type8 = temparry[8],
                        sum = sum,
                        checknum = checknum,
                        checkerrnum = checkerrnum,
                        errnum = errnum,
                        backnum = backnum
                    });

                }
            }

            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());

        }
        /// <summary>
        /// 单量
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="objectid"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CenterBarcodeNum_shjj(string startdate, string enddate, string objectid)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            BLL.DZ_Object_BLL objbll = new BLL.DZ_Object_BLL();
            if (string.IsNullOrEmpty(objectid))
            {
                SessionModel model = Session["user"] as SessionModel;
                objectid = model == null ? "" : model.User.Objectid.ToString();
            }
            startdate += " 00:00:00"; enddate += " 23:59:59";
            List<string[]> listarray = bll.AllCenterBarcodeNum_shjj(startdate, enddate, objectid);
            List<object> resultlistx = new List<object>();
            foreach (var temparry in listarray)
            {
                resultlistx.Add(new
                {
                    id = temparry[0],
                    type1 = temparry[1],
                    type2 = temparry[2],
                    type3 = temparry[3]
                });
            }
            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        /// <summary>
        /// 本中心捷记产量 (分中心管理员)
        /// </summary>
        /// <returns></returns>
        public ActionResult MyCenterProduction_shjj()
        {

            return View();
        }
        [AllowAnonymous]
        public ActionResult MyCenterProduction_shjjTJ(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            SessionModel model = Session["user"] as SessionModel;
            string Objid = model == null ? "" : model.User.Objectid.ToString();

            List<string[]> listarray = bll.MyCenterProduction_shjj(startdate, enddate, Objid);
            List<string> userlist = listarray.Select(a => a[3]).Distinct().ToList();
            //string[] filenametemplet = new string[] { 
            //    "寄件人姓名","寄件人电话","收件人姓名","收件人电话","收件人地址","混合字段","物品重量"};
            string[] resultarr = new string[8];


            //List<string[]> resultlist = new List<string[]>();
            List<object> resultlistx = new List<object>();
            foreach (string user_id in userlist)
            {

                var group = listarray.Where(a => a[3] == user_id);
                string[] temparry = new string[9] { "0", "0", "0", "0", "0", "0", "0", "0", "0" };

                // temparry[1] = "";
                string username = user_id;
                int checknum = 0;
                int checkerrnum = 0;
                int errnum = 0;
                int backnum = 0;
                int first = 0;
                foreach (var item in group)
                {
                    if (first == 0)
                    {
                        temparry[0] = item[0];
                        temparry[1] = item[1];
                        username = item[3];
                    }
                    if (item[2] == "寄件人姓名")
                    {
                        temparry[2] = item[4];
                    }
                    else if (item[2] == "寄件人电话")
                    {
                        temparry[3] = item[4];
                    }
                    else if (item[2] == "收件人姓名")
                    {
                        temparry[4] = item[4];
                    }
                    else if (item[2] == "收件人电话")
                    {
                        temparry[5] = item[4];
                    }
                    else if (item[2] == "收件人地址")
                    {
                        temparry[6] = item[4];
                    }
                    else if (item[2] == "内件品名")
                    {
                        temparry[7] = item[4];
                    }
                    else if (item[2] == "总重量")
                    {
                        temparry[8] = item[4];
                    }

                    checknum += int.Parse(item[5]);
                    checkerrnum += int.Parse(item[6]);
                    errnum += int.Parse(item[7]);
                    backnum += int.Parse(item[8]);
                    first++;
                }
                int sum = 0;

                for (int i = 2; i < 9; i++)
                {
                    sum += int.Parse(temparry[i] == "" ? "0" : temparry[i]);
                }
                resultlistx.Add(new
                {
                    id = temparry[0],
                    type1 = temparry[1],
                    type2 = temparry[2],
                    type3 = temparry[3],
                    type4 = temparry[4],
                    type5 = temparry[5],
                    type6 = temparry[6],
                    type7 = temparry[7],
                    type8 = temparry[8],
                    username = username,
                    sum = sum,
                    checknum = checknum,
                    checkerrnum = checkerrnum,
                    errnum = errnum,
                    backnum = backnum
                });


            }

            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        #endregion
        #region 安能
        public ActionResult anwlwaitoinput()
        {
            return Content("");
        }
        public ActionResult AllCenterPro_anwl()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult AllCenterPro_AnwlTJ(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            BLL.DZ_Object_BLL objbll = new BLL.DZ_Object_BLL();
            //SessionModel model = Session["user"] as SessionModel;
            //string userid = model == null ? "" : model.User.Userid;
            //  List<string> objlist = objbll.GetAllList().Where(a => a[4] == "0").Select(a => a[1]).ToList<string>();//录入中心名称列表 isscan

            List<string[]> listarray = bll.AllCenterOutput_anwl(startdate, enddate);
            List<string> objlist = listarray.Select(a => a[0]).Distinct().ToList();//录入中心名称列表 isscan

            //string[] filenametemplet = new string[] { 
            //    "寄件人姓名","寄件人电话","收件人姓名","收件人电话","收件人地址","混合字段","物品重量"};

            List<string[]> resultlist = new List<string[]>();
            List<object> resultlistx = new List<object>();
            foreach (string objname in objlist)
            {

                var group = listarray.Where(a => a[0] == objname);
                string[] temparry = new string[9] { "0", "0", "0", "0", "0", "0", "0", "0", "0" };

                temparry[0] = objname;
                int checknum = 0;
                int checkerrnum = 0;
                int errnum = 0;
                int backnum = 0;
                foreach (var item in group)
                {
                    temparry[1] = item[1];
                    if (item[2] == "寄件人姓名")
                    {
                        temparry[2] = item[3];
                    }
                    else if (item[2] == "寄件人电话")
                    {
                        temparry[3] = item[3];
                    }
                    else if (item[2] == "收件人姓名")
                    {
                        temparry[4] = item[3];
                    }
                    else if (item[2] == "收件人电话")
                    {
                        temparry[5] = item[3];
                    }
                    else if (item[2] == "收件人地址")
                    {
                        temparry[6] = item[3];
                    }
                    else if (item[2] == "内件品名")
                    {
                        temparry[7] = item[3];
                    }
                    else if (item[2] == "总重量")
                    {
                        temparry[8] = item[3];
                    }
                    checknum += int.Parse(item[4]);
                    checkerrnum += int.Parse(item[5]);
                    errnum += int.Parse(item[6]);
                    backnum += int.Parse(item[7]);
                }
                int sum = 0;

                for (int i = 2; i < 9; i++)
                {
                    sum += int.Parse(temparry[i] == "" ? "0" : temparry[i]);
                }
                if (sum != 0)
                {
                    resultlistx.Add(new
                    {
                        id = temparry[0],
                        type1 = temparry[1],
                        type2 = temparry[2],
                        type3 = temparry[3],
                        type4 = temparry[4],
                        type5 = temparry[5],
                        type6 = temparry[6],
                        type7 = temparry[7],
                        type8 = temparry[8],
                        sum = sum,
                        checknum = checknum,
                        checkerrnum = checkerrnum,
                        errnum = errnum,
                        backnum = backnum
                    });

                }
            }

            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());

        }
        /// <summary>
        /// 单量
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="objectid"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CenterBarcodeNum_anwl(string startdate, string enddate, string objectid)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            BLL.DZ_Object_BLL objbll = new BLL.DZ_Object_BLL();
            if (string.IsNullOrEmpty(objectid))
            {
                SessionModel model = Session["user"] as SessionModel;
                objectid = model == null ? "" : model.User.Objectid.ToString();
            }
            startdate += " 00:00:00"; enddate += " 23:59:59";
            List<string[]> listarray = bll.AllCenterBarcodeNum_anwl(startdate, enddate, objectid);
            List<object> resultlistx = new List<object>();
            foreach (var temparry in listarray)
            {
                resultlistx.Add(new
                {
                    id = temparry[0],
                    type1 = temparry[1],
                    type2 = temparry[2],
                    type3 = temparry[3]
                });
            }
            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }
        /// <summary>
        /// 安能报表管理
        /// </summary>
        /// <returns></returns>
        public ActionResult AnwlCenterMonitor()
        {
            return View();
        }
        /// <summary>
        /// 本中心安能物流产量 (分中心管理员)
        /// </summary>
        /// <returns></returns>
        public ActionResult MyCenterProduction_anwl()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult MyCenterProduction_anwlTJ(string startdate, string enddate)
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            SessionModel model = Session["user"] as SessionModel;
            string Objid = model == null ? "" : model.User.Objectid.ToString();

            List<string[]> listarray = bll.MyCenterProduction_anwl(startdate, enddate, Objid);
            List<string> userlist = listarray.Select(a => a[3]).Distinct().ToList();
            //string[] filenametemplet = new string[] { 
            //    "寄件人姓名","寄件人电话","收件人姓名","收件人电话","收件人地址","混合字段","物品重量"};
            string[] resultarr = new string[8];


            //List<string[]> resultlist = new List<string[]>();
            List<object> resultlistx = new List<object>();
            foreach (string user_id in userlist)
            {

                var group = listarray.Where(a => a[3] == user_id);
                string[] temparry = new string[9] { "0", "0", "0", "0", "0", "0", "0", "0", "0" };

                // temparry[1] = "";
                string username = user_id;
                int checknum = 0;
                int checkerrnum = 0;
                int errnum = 0;
                int backnum = 0;
                int first = 0;
                foreach (var item in group)
                {
                    if (first == 0)
                    {
                        temparry[0] = item[0];
                        temparry[1] = item[1];
                        username = item[3];
                    }
                    if (item[2] == "寄件人姓名")
                    {
                        temparry[2] = item[4];
                    }
                    else if (item[2] == "寄件人电话")
                    {
                        temparry[3] = item[4];
                    }
                    else if (item[2] == "收件人姓名")
                    {
                        temparry[4] = item[4];
                    }
                    else if (item[2] == "收件人电话")
                    {
                        temparry[5] = item[4];
                    }
                    else if (item[2] == "收件地址")
                    {
                        temparry[6] = item[4];
                    }
                    else if (item[2] == "内件品名")
                    {
                        temparry[7] = item[4];
                    }
                    else if (item[2] == "总重量")
                    {
                        temparry[8] = item[4];
                    }

                    checknum += int.Parse(item[5]);
                    checkerrnum += int.Parse(item[6]);
                    errnum += int.Parse(item[7]);
                    backnum += int.Parse(item[8]);
                    first++;
                }
                int sum = 0;

                for (int i = 2; i < 9; i++)
                {
                    sum += int.Parse(temparry[i] == "" ? "0" : temparry[i]);
                }
                resultlistx.Add(new
                {
                    id = temparry[0],
                    type1 = temparry[1],
                    type2 = temparry[2],
                    type3 = temparry[3],
                    type4 = temparry[4],
                    type5 = temparry[5],
                    type6 = temparry[6],
                    type7 = temparry[7],
                    type8 = temparry[8],
                    username = username,
                    sum = sum,
                    checknum = checknum,
                    checkerrnum = checkerrnum,
                    errnum = errnum,
                    backnum = backnum
                });


            }

            StringBuilder sb = new StringBuilder();
            string json = Common.JSON.SetJson(resultlistx);
            //   json = json.Remove(json.Length - 2, 1).Remove(0, 1);
            sb.Append("{\"total\":" + resultlistx.Count + ",\"rows\":").Append(json).Append("}");
            //return Content(Common.JSON.SetJson(obj));
            return Content(sb.ToString());
        }

        #endregion
        #region 录单机构监控 EMS
        public ActionResult OrganizMonitor()
        {
            return View();
        }
        public ActionResult OrganizMonitor1()
        {
            return View();
        }
        public ActionResult OrganizMonitor2()
        {
            return View();
        }
        public ActionResult OrganizMonitor3()
        {
            return View();
        }
        #endregion
    }
}
