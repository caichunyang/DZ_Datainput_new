using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace DZ.EMS
{
    public class Do_work
    {
        private static string pubdatestr = DateTime.Now.ToString("yyyy-MM-dd");
        public Do_work(int time)
        {
            BLL.DZ_ShowOutPut_BLL showbll = new BLL.DZ_ShowOutPut_BLL();
            BLL.DZ_OVERDUE_BLL overbll = new BLL.DZ_OVERDUE_BLL();

            //array[0]表示接收image量 按天分组 array[1]表示上传image量 outstate==1 按天分组
            //string datestr = DateTime.Now.ToString("yyyy-MM-dd");
            //string[] array = showbll.InputOverdueTJ_YD(datestr);

            //      showbll.InputOverdueTJ_YD2(DateTime.Now.ToString("yyyy-MM-dd"));
            // showbll.repireOutput();
            //int day = DateTime.Now.Day;
            //Thread thr = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep(60 * 1000 * 5);
            //        if (day != DateTime.Now.Day)//更新前一天数据
            //        {
            //            DateTime yestotay = DateTime.Now.AddDays(-1);
            //            string datestr2 = yestotay.ToString("yyyy-MM-dd");
            //            string datenum2 = yestotay.ToString("yyyyMMdd");
            //            //string[] array_yd2 = showbll.InputOverdueTJ_YD(datestr2);
            //            //string[] array_ems2 = showbll.InputOverdueTJ_EMS(datestr2);
            //            string[] array_yd3 = showbll.InputOverdueTJ_YD2(datestr2);
            //            //overbll.myinsert("yd", datenum2, array_yd2);
            //            //overbll.myinsert("ems", datenum2, array_ems2);
            //            overbll.insert2("yd", datenum2, array_yd3);
            //            day = DateTime.Now.Day;
            //            continue;
            //        }
            //        string datestr = DateTime.Now.ToString("yyyy-MM-dd");
            //        string datenum = DateTime.Now.ToString("yyyyMMdd");
            //        string[] array_yd = showbll.InputOverdueTJ_YD(datestr);
            //        string[] array_ems = showbll.InputOverdueTJ_EMS(datestr);
            //       // string[] array_yd4 = showbll.InputOverdueTJ_YD2(datestr);
            //        overbll.myinsert("yd", datenum, array_yd);
            //        overbll.myinsert("ems", datenum, array_ems);
            //       // overbll.insert2("yd", "", array_yd4);

            //    }
            //});
            //thr.IsBackground = true;
            // thr.Start();
        }

        /// <summary>
        /// 一月份不适用
        /// </summary>
        /// <param name="month"></param>
        /// <param name="monthdays1">上一个月天数</param>
        /// <param name="monthdays">这个月天数</param>
        public void GenarateMonthReport_yd()//int month,int monthdays1,int monthdays
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            //        List<string[]> list=new List<string[]> ();
            //       for (int i = 1; i <=monthdays; i++)
            //        {
            //           if (i==1)
            //{
            //      string zipname1="2016"+(month-1).ToString()+monthdays1.ToString()+"2300";
            //                 string zipname2="2016"+month.ToString()+"012300";
            //}

            //        }
            string[] inputarray = {
                                
                                };

            string[] array = bll.EveryMonthYD_TJ("20161205230000", "20161206230000");
            Console.WriteLine(array.ToString());
        }
        public void UpdatePA_RightLength()
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            bll.updatePA_newlength("2016-11-01 00:00:00", "2016-11-02 00:00:00", 92.58);
        }
        public void updateoutput()
        {
            BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
            bll.UpdateYD_output();
        }

        public void Getyddayproinfo()
        {
            //Thread thr = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep(1000 * 10 );
            //          string datestr = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
            //        if (pubdatestr != datestr)
            //        {
            //            pubdatestr = datestr;
            //        }


            //    }

            //});

            Thread thr = new Thread(() =>
           {
               while (true)
               {

                   BLL.DZ_ShowOutPut_BLL bll = new BLL.DZ_ShowOutPut_BLL();
                   BLL.DZ_Recive_BLL Recbll = new BLL.DZ_Recive_BLL();

                   DateTime dtnow = DateTime.Now;//.AddDays(-1);
                   string datestr = dtnow.ToString("yyyy-MM-dd");
                   string yesteday = dtnow.ToString("yyyy-MM-dd");
                   //   var histroylist=   bll.SelectZhongHe_history_yd(datestr, datestr);
                   //   var histroylist=   bll.SelectZhongHe_history_yd("2017-05-18", "2017-05-18");//记录在 dz_hourse_record
                   //  bll.InsertHourse_record("yd", histroylist);
                   //   if (bll.IsHadSameZhongHe_history_yd("yd", "2017-05-18") >= 0)
                   //{
                   //var histroylist = bll.SelectZhongHe_history_yd("2017-06-05", "2017-06-05");//记录在 dz_hourse_record
                   //histroylist = bll.SelectZhongHe_historyDeal_yd("2017-06-05", histroylist);
                   //bll.InsertHourse_record("yd", histroylist);

                   //}


                   if (pubdatestr != datestr)
                   {
                       // bll.SelectInputAndOverdue_yd(yesteday);//预期情况 DZ_INPUTOVERDUE
                       var model = Recbll.GetModel(yesteday, "yd");
                       if (model == null)
                       {
                           string startdate = dtnow.AddDays(-1).ToString("yyyyMMdd") + "230000";
                           string enddate = dtnow.ToString("yyyyMMdd") + "230000";
                           string[] array = bll.EveryMonthYD_TJ(startdate, enddate);//string[] 长3
                           bool result = Recbll.Insert(new string[] { datestr, array[0], array[1], array[2], "", "yd", "" });//每天晚上统计 韵达23点 接收上传
                       }

                       //每天一次
                       if (bll.IsHadSameZhongHe_history_yd("yd", yesteday) <= 0)
                       {
                           //var histroylist = bll.SelectZhongHe_history_yd(yesteday, yesteday);//记录在 dz_hourse_record
                           //  histroylist = bll.SelectZhongHe_historyDeal_yd(yesteday,histroylist);
                           //bll.InsertHourse_record("yd", histroylist);
                       }

                       pubdatestr = datestr;
                   }

                    Thread.Sleep(1000 * 60 * 60);
               }

           });
            thr.IsBackground = true;
           // thr.Start();
        }

    }
}