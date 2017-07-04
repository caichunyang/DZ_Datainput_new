using DZ.MODEL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DZ.EMS.Controllers
{
    public class ExcelOutCenterController : Controller
    {
        [AllowAnonymous]
        public ActionResult ExcelOutCenter()
        {
            var count = HttpContext.Application["onLine"];
            ViewBag.count = count;
            MODEL.SessionModel session = Session["user"] as SessionModel;
            if (session != null)
            {
                var list = session.ModuleList.Where(a => a.Controler == "exceloutcenter" && a.Action != "exceloutcenter" && a.Isview == 1);
                ViewBag.tabs = list;
                return View();
            }
            else
            {
                return RedirectToAction("login", "home");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index_ems()
        {
            SessionModel model = Session["user"] as SessionModel;
            if (model != null)
            {
                ViewBag.objid = model.User.Objectid;
                ViewBag.objname = model.objname;
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult All_Index_ems()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult GetBoxNum(string thedate, int objectid)
        {
            BLL.DZ_EXCEL_EMS_BLL bll = new BLL.DZ_EXCEL_EMS_BLL();
            var newlistarray = bll.GetBoxNo(thedate, objectid);
            var obj = from a in newlistarray select new { boxno = a[0], a = a[1], b = a[2], c = a[3], d = a[4], e = a[5] };
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + obj.Count() + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
            return Content(sb.ToString());
        }
        [AllowAnonymous]
        public ActionResult Download(string boxno, int type, int objectid, string objectname)
        {
            BLL.DZ_EXCEL_EMS_BLL bll = new BLL.DZ_EXCEL_EMS_BLL();
            var newlist = bll.EXCEL_OUT(boxno, objectid, objectname);
            if (type == 0)
            {
                newlist = newlist.Where(a => a[4] != "1" && a[5] != "1" && a[6] != "1").ToList<string[]>();
                //Common.ExcelHelper excel = new Common.ExcelHelper();
                string directpath = "/Excel" + "/" + objectname;
                string localpath = Server.MapPath(directpath);
                if (!System.IO.Directory.Exists(localpath))
                {
                    System.IO.Directory.CreateDirectory(localpath);
                }
                string path_ = directpath + "/" + boxno + "_正常数据.xls";
                string path = Server.MapPath(path_);//boxno + 

                Common.NPOI_excel npoi = new Common.NPOI_excel();
                npoi.BuildExcel(newlist, path);
                return Content(path_);
            }
            else
            {
                newlist = newlist.Where(a => a[4] == "1" || a[5] == "1" || a[6] == "1").ToList<string[]>();
                Common.ExcelHelper excel = new Common.ExcelHelper();
                string directpath = "/Excel" + "/" + objectname;
                string localpath = Server.MapPath(directpath);
                if (!System.IO.Directory.Exists(localpath))
                {
                    System.IO.Directory.CreateDirectory(localpath);
                }
                string path_ = directpath + "/" + boxno + "_异常数据.xls";
                string path = Server.MapPath(path_);//boxno + 

                Common.NPOI_excel npoi = new Common.NPOI_excel();
                npoi.BuildExcel(newlist, path);
                return Content(path_);
            }

        }

        [AllowAnonymous]
        public ActionResult Index_Shjj()
        {
            return View();
        }
        public List<string[]> GenarateExcel_shjj(string boxno, string userid)//string startdate, string enddate // i.barcode,p.values3,i.filedtype 
        {
            BLL.dz_output_bll bll = new BLL.dz_output_bll();
            List<string[]> list = bll.ShjjExcelGenart_y(boxno, userid);
            List<string> barcodes = list.Select(a => a[0]).Distinct().ToList();
            List<string[]> resultlist = new List<string[]>();
            foreach (var barcode in barcodes)
            {
                List<string[]> templist = list.Where(a => a[0] == barcode).ToList();
                string[] onerow = new string[14];
                for (int i = 0; i < templist.Count; i++)
                {
                    if (i == 0)
                    {
                        onerow[0] = templist[i][0];

                    }
                    if (templist[i][2] == "4")//收件人
                    {
                        onerow[1] = templist[i][1];
                    }
                    //if (templist[i][2] == "6")//收件邮箱
                    //{
                    onerow[2] = "";
                    //}
                    //if (templist[i][2] == "6")//收件省州
                    //{
                    onerow[3] = "";
                    ////}
                    //if (templist[i][2] == "6")//收件城市
                    //{
                    onerow[4] = "";

                    //}
                    //if (templist[i][2] == "6")//收件人单位
                    //{
                    onerow[5] = "";
                    //}
                    if (templist[i][2] == "5")//收件人地址
                    {
                        onerow[6] = templist[i][1];

                    }
                    //if (templist[i][2] == "6")//收件人邮编
                    //{
                    onerow[7] = "";
                    //}
                    if (templist[i][2] == "6")//收件人电话
                    {
                        onerow[8] = templist[i][1];
                    }
                    if (templist[i][2] == "3")//物品
                    {
                        onerow[9] = templist[i][1];
                    }
                    if (templist[i][2] == "7")//重量
                    {
                        onerow[10] = templist[i][1];
                    }
                    if (templist[i][2] == "1")//发件人
                    {
                        onerow[11] = templist[i][1];
                    }
                    if (templist[i][2] == "2")//发件电话
                    {
                        onerow[12] = templist[i][1];
                    }
                    //if (templist[i][2] == "1")
                    //{
                    onerow[13] = "";
                    //}

                }
                resultlist.Add(onerow);
            }
            return resultlist;
        }
        public List<string[]> GenarateExcel_yz(string boxno, string userid)//string startdate, string enddate // i.barcode,p.values3,i.filedtype 
        {
            BLL.dz_output_bll bll = new BLL.dz_output_bll();
            List<string[]> list = bll.ShjjExcelGenart_y(boxno, userid);
            List<string> barcodes = list.Select(a => a[0]).Distinct().ToList();
            List<string[]> resultlist = new List<string[]>();
            foreach (var barcode in barcodes)
            {
                List<string[]> templist = list.Where(a => a[0] == barcode).ToList();
                string[] onerow = new string[14];
                for (int i = 0; i < templist.Count; i++)
                {
                    if (i == 0)
                    {
                        onerow[0] = templist[i][0];

                    } if (templist[i][2] == "1")//发件人
                    {
                        onerow[1] = templist[i][1];
                    }
                    if (templist[i][2] == "2")//发件电话
                    {
                        onerow[2] = templist[i][1];
                    }
                    if (templist[i][2] == "8")//发件地址
                    {
                        onerow[3] = templist[i][1];
                    }
                    if (templist[i][2] == "4")//收件人
                    {
                        onerow[4] = templist[i][1];
                    }
                    if (templist[i][2] == "6")//收件人电话
                    {
                        onerow[5] = templist[i][1];
                    }
                    if (templist[i][2] == "5")//收件人地址
                    {
                        onerow[6] = templist[i][1];

                    }
                    if (templist[i][2] == "7")//重量
                    {
                        onerow[7] = templist[i][1];

                    }
                    if (templist[i][2] == "9")//邮编
                    {
                        onerow[8] = templist[i][1];

                    }
                }
                resultlist.Add(onerow);
            }
            return resultlist;
        }
            public List<string[]> GenarateExcel_anwl(string boxno, string userid)//string startdate, string enddate // i.barcode,p.values3,i.filedtype 
        {
            BLL.dz_output_bll bll = new BLL.dz_output_bll();
            List<string[]> list = bll.AnwlExcelGenart_y(boxno, userid);
            List<string> barcodes = list.Select(a => a[0]).Distinct().ToList();
            List<string[]> resultlist = new List<string[]>();
            foreach (var barcode in barcodes)
            {
                List<string[]> templist = list.Where(a => a[0] == barcode).ToList();
                string[] onerow = new string[14];
                for (int i = 0; i < templist.Count; i++)
                {
                    if (i == 0)
                    {
                        onerow[0] = templist[i][0];

                    } if (templist[i][2] == "16")//发件人
                    {
                        onerow[1] = templist[i][1];
                    }
                    if (templist[i][2] == "13")//发件电话
                    {
                        onerow[2] = templist[i][1];
                    }
                    if (templist[i][2] == "14")//发件地址
                    {
                        onerow[3] = templist[i][1];
                    }
                    if (templist[i][2] == "16")//收件人
                    {
                        onerow[4] = templist[i][1];
                    }
                    if (templist[i][2] == "17")//收件人电话
                    {
                        onerow[8] = templist[i][1];
                    }
                    if (templist[i][2] == "1")//收件人地址
                    {
                        onerow[6] = templist[i][1];

                    }
                    if (templist[i][2] == "25")//重量
                    {
                        onerow[7] = templist[i][1];

                    }
                 
                }
                resultlist.Add(onerow);
            }
            return resultlist;
        }
        [AllowAnonymous]
        public ActionResult Download_pub(string boxno, int type, string userid)//, string item
        {
            SessionModel model = Session["user"] as SessionModel;
            BLL.DZ_ROLEMODULE_BLL role = new BLL.DZ_ROLEMODULE_BLL();
            BLL.DZ_ROLE_BLL bll = new BLL.DZ_ROLE_BLL();
            if (model != null)
            {
                if (model.User.Objectid == 10)
                {
                    return Content("{\"msg\":\"无权限访问\"}");
                }
                var rolelist = bll.GetUserrole(userid);
                if (rolelist.Contains(2))
                {
                    return Content(Common.JSON.SetJson(Download_yz(boxno, userid, type)));
                }
                else if (rolelist.Contains(5))
                {
                    return Content(Common.JSON.SetJson(Download_shjj(boxno, userid, type)));
                }
                     else if (rolelist.Contains(6))
                {
                    return Content(Common.JSON.SetJson(Download_anwl(boxno, userid, type)));
                }
                else
                {
                    return Content("{\"msg\":\"无权限访问\"}");
                }
            }
            return Content("{\"msg\":\"请登录\"}");
            //if (model != null)
            //{
            //    var moduleidlist = model.ModuleList.Select(a => a.Moduleid);
            //    if (moduleidlist.Contains(76))
            //    {
            //        return Content(  Download_yz(boxno, type));
            //    }
            //   else if (moduleidlist.Contains(70))
            //    {
            //       return Content( Download_shjj(boxno, type));
            //    }
            //    else
            //    {
            //        return Content("请登录");
            //    }
            //    //switch (item)
            //    //{
            //    //    case "yz": Download_yz(boxno, type); break;
            //    //    case "shjj": Download_shjj(boxno, type); break;
            //    //    default: return Content("");
            //    //}
            //}
            //else
            //{
            //    return Content("");
            //}
        }
        public MODEL.downinfo Download_shjj(string boxno, string up_userid, int type)
        {

            string userid = "";
            SessionModel model = Session["user"] as SessionModel;
            if (model != null)
            {
                userid = model.User.Userid;
            }

            List<string[]> newlist = GenarateExcel_shjj(boxno, up_userid);
            Common.NPOI_excel npoi = new Common.NPOI_excel();
            string directpath = "/Excel" + "/shjj";
            string localpath = Server.MapPath(directpath);
            if (!System.IO.Directory.Exists(localpath))
            {
                System.IO.Directory.CreateDirectory(localpath);
            }

            string[] files = Directory.GetFiles(localpath, boxno + "*.xls");
            List<string> filenamelist = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                filenamelist.Add(Path.GetFileName(files[i]));
            }
            if (type == 0)
            {
                string path_ = directpath + "/" + boxno + "_" + userid + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + ".xls";
                string path = Server.MapPath(path_);//boxno + 


                //   newlist = newlist.Where(a => a[1] != "0" && a[1] != "").ToList();
                npoi.BuildExcel_shjj(newlist, path);
                MODEL.downinfo resultmodel = new downinfo() { url = path_, length = files.Length, urllist = filenamelist };
                //string result = path_ + "^" + files.Length + "^" + string.Join("||", filenamelist);
                return resultmodel;
            }
            else
            {
                string path_ = directpath + "/" + boxno + "_" + DateTime.Now.ToString("ddhhmmss") + "_异常数据.xls";
                string path = Server.MapPath(path_);//boxno + 

                newlist = newlist.Where(a => a[1] == "0" || a[1] == "").ToList();
                npoi.BuildExcel_shjj(newlist, path);
                return new downinfo() { url = path_, length = files.Length, urllist = filenamelist };
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="boxno"></param>
        /// <param name="up_userid"></param>
        /// <param name="type">type 0 正常数据</param>
        /// <returns></returns>
            public MODEL.downinfo Download_anwl(string boxno, string up_userid, int type)
           {
            string userid = "";
            SessionModel model = Session["user"] as SessionModel;
            if (model != null)
            {
                userid = model.User.Userid;
            }

            List<string[]> newlist = GenarateExcel_anwl(boxno, up_userid);
            Common.NPOI_excel npoi = new Common.NPOI_excel();
            string directpath = "/Excel" + "/anwl";
            string localpath = Server.MapPath(directpath);
            if (!System.IO.Directory.Exists(localpath))
            {
                System.IO.Directory.CreateDirectory(localpath);
            }

            string[] files = Directory.GetFiles(localpath, boxno + "*.xls");
            List<string> filenamelist = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                filenamelist.Add(Path.GetFileName(files[i]));
            }
            if (type == 0)
            {
                string path_ = directpath + "/" + boxno + "_" + userid + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + ".xls";
                string path = Server.MapPath(path_);//boxno + 


                //   newlist = newlist.Where(a => a[1] != "0" && a[1] != "").ToList();
                npoi.BuildExcel_shjj(newlist, path);
                MODEL.downinfo resultmodel = new downinfo() { url = path_, length = files.Length, urllist = filenamelist };
                //string result = path_ + "^" + files.Length + "^" + string.Join("||", filenamelist);
                return resultmodel;
            }
            else
            {
                string path_ = directpath + "/" + boxno + "_" + DateTime.Now.ToString("ddhhmmss") + "_异常数据.xls";
                string path = Server.MapPath(path_);//boxno + 

                newlist = newlist.Where(a => a[1] == "0" || a[1] == "").ToList();
                npoi.BuildExcel_shjj(newlist, path);
                return new downinfo() { url = path_, length = files.Length, urllist = filenamelist };
            }

        }
        public MODEL.downinfo Download_yz(string boxno, string up_userid, int type)
        {

            string userid = "";
            SessionModel model = Session["user"] as SessionModel;
            if (model != null)
            {
                userid = model.User.Userid;
            }

            List<string[]> newlist = GenarateExcel_yz(boxno, up_userid);
            Common.NPOI_excel npoi = new Common.NPOI_excel();
            string directpath = "/Excel" + "/yz";
            string localpath = Server.MapPath(directpath);
            if (!System.IO.Directory.Exists(localpath))
            {
                System.IO.Directory.CreateDirectory(localpath);
            }

            string[] files = Directory.GetFiles(localpath, boxno + "_" + userid + "*.xls");
            List<string> filenamelist = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                filenamelist.Add(Path.GetFileName(files[i]));
            }
            if (type == 0)
            {
                string path_ = directpath + "/" + boxno + "_" + userid + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + ".xls";
                string path = Server.MapPath(path_);//boxno + 


                //   newlist = newlist.Where(a => a[1] != "0" && a[1] != "").ToList();
                npoi.BuildExcel(newlist, path);
                //string result = Url.Encode(path_ + "^" + files.Length + "^" + string.Join("||", filenamelist));
                MODEL.downinfo resultmodel = new downinfo() { url = path_, length = files.Length, urllist = filenamelist };
                //string result = path_ + "^" + files.Length + "^" + string.Join("||", filenamelist);
                return resultmodel;
            }
            else
            {
                string path_ = directpath + "/" + boxno + "_" + DateTime.Now.ToString("ddhhmmss") + "_异常数据.xls";
                string path = Server.MapPath(path_);//boxno + 

                newlist = newlist.Where(a => a[1] == "0" || a[1] == "").ToList();
                npoi.BuildExcel(newlist, path);
                return new downinfo() { url = path_, length = files.Length, urllist = filenamelist };
            }

        }
        [AllowAnonymous]
        public ActionResult GetBoxNum_shjj(string thedate)
        {
            BLL.DZ_EXCEL_EMS_BLL bll = new BLL.DZ_EXCEL_EMS_BLL();
            MODEL.SessionModel session = Session["user"] as SessionModel;
            string userid = "x";
            if (session != null)
            {
                userid = session.User.Objectid == 0 || session.User.Objectid == 10 ? "" : session.User.Userid;//"中心管理组"

                var newlistarray = bll.GetBoxNo_shjj(thedate, userid);
                //string objname = "";
                //if (objectid != -1)
                //{
                //    objname = session.objname;
                //}
                var obj = from a in newlistarray select new { boxno = a[0], a = a[1], b = a[2], c = a[3], d = a[4], e = a[5], f = a[6], userid = a[7] };
                StringBuilder sb = new StringBuilder();

                sb.Append("{\"total\":" + obj.Count() + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
                return Content(sb.ToString());
            }
            else
            {
                return Content("{\"total\":0,\"rows\":\"\"}");
            }
        }
        public ActionResult Index_Anwl()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetBoxNum_anwl(string thedate)
        {
            BLL.DZ_EXCEL_EMS_BLL bll = new BLL.DZ_EXCEL_EMS_BLL();
            SessionModel session = Session["user"] as SessionModel;
            if (session != null)
            {
                string userid = session.User.Objectid == 0 || session.User.Objectid == 10 ? "" : session.User.Userid;//"中心管理组"
                var newlistarray = bll.GetBoxNo_anwl(thedate, userid);
                var obj = from a in newlistarray select new { boxno = a[0], a = a[1], b = a[2], c = a[3], d = a[4], e = a[5], f = a[6], g = a[7], h = a[8],userid=a[9] };
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"total\":" + obj.Count() + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
                return Content(sb.ToString());
            }
            return Content("");
        }

           [AllowAnonymous]
        public ActionResult GetRecivNum_anwl(string startdate,string enddate)
        {
            BLL.DZ_EXCEL_EMS_BLL bll = new BLL.DZ_EXCEL_EMS_BLL();
            SessionModel session = Session["user"] as SessionModel;
            if (session != null)
            {
                
                string userid ="" ;//"中心管理组"
                var newlistarray = bll.GetRecivNoByUser_anwl(startdate,enddate, userid);
                var obj = from a in newlistarray select new { userid = a[0], a = a[1], b = a[2], c = a[3], d = a[4], e = a[5], f = a[6], g = a[7], h = a[8],groupname=a[9] };
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"total\":" + obj.Count() + ",\"rows\":").Append(Common.JSON.SetJson(obj)).Append("}");
                return Content(sb.ToString());
            }
            return Content("");
        }
    }
}
