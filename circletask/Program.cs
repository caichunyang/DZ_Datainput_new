using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.BLL;

namespace circletask
{
    class Program
    {
        static void Main(string[] args)
        {
            dowork();
        }
        private static void dowork()
        {
            myclass my = new myclass();
            my.SelectZhongHe_yd();
            my.insertUpRecv_yd();
        }
    }
    public class myclass
    {
        public void SelectZhongHe_yd()
        {
            DateTime dtnow = DateTime.Now;
            string yestoday = dtnow.AddDays(-1).ToString("yyyy-MM-dd");
            DZ_ShowOutPut_BLL bll = new DZ_ShowOutPut_BLL();
            DZ_Recive_BLL Recbll = new DZ_Recive_BLL();
            if (bll.IsHadSameZhongHe_history_yd("yd", yestoday) <= 0)
            {
                var histroylist = bll.SelectZhongHe_history_yd(yestoday, yestoday);//记录在 dz_hourse_record
                histroylist = bll.SelectZhongHe_historyDeal_yd(yestoday, histroylist);
                bll.InsertHourse_record("yd", histroylist);
            }

            var model = Recbll.GetModel(yestoday, "yd");
            if (model == null)
            {
                string startdate = dtnow.AddDays(-1).ToString("yyyyMMdd") + "230000";
                string enddate = dtnow.ToString("yyyyMMdd") + "230000";
                string[] array = bll.EveryMonthYD_TJ(startdate, enddate);//string[] 长3
                bool result = Recbll.Insert(new string[] { yestoday, array[0], array[1], array[2], "", "yd", "" });//每天晚上统计 韵达23点 接收上传
            }
        }
        /// <summary>
        /// 中通每天 接收上传量 统计入库
        /// </summary>
        public void insertUpRecv_yd()
        {
            DateTime dtnow = DateTime.Now;
            DZ_ShowOutPut_BLL bll = new DZ_ShowOutPut_BLL();
            string yestoday = dtnow.AddDays(-1).ToString("yyyy-MM-dd");
            var list = GethoursUpReclist(yestoday);
            bll.GetInsertUpRecv_yd(yestoday, string.Join("_", list));
        }

        private List<string> GethoursUpReclist(string start)
        {
            DZ_ShowOutPut_BLL bll = new DZ_ShowOutPut_BLL();
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
    }
}
