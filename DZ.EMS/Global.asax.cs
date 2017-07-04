using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DZ.EMS
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            Application["onLine"] = 0;
           Do_work dowork = new Do_work(5);//5分钟刷新一次当天 逾期统计表 dz_overdue
          // dowork.Getyddayproinfo();
          // dowork.updateoutput();
          // dowork.UpdatePA_RightLength();
           //dowork.GenarateMonthReport_yd();
           //BLL.DZ_PINGAN_BLL bll = new BLL.DZ_PINGAN_BLL();
           //bll.TransferrData();
            //Application.Lock();
            //Application["count"] = 0; //Application.Set("count",0) /Application.Add("count",0)   初始化变量，这个作用等同，都是将count设置为0。

            //Application["online"] = 0;

            //Application.Unlock();
        }
        void Session_Start(object sender, EventArgs e)
        {
            //在新会话启动时运行的代码
            Application.Lock();										//锁定Application
           // Application["total"] = (int)Application["total"] + 1;	  //总访问量加1
            Application["onLine"] = (int)Application["onLine"] + 1;	//在线人数加1
            Application.UnLock();	
        }

        void Session_End(object sender, EventArgs e)
        {
            //在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式 
            //设置为 StateServer 或 SQLServer，则不会引发该事件。

            // ZC ： 
            //  1）、本函数中使用 Page 时编译会报错“非静态的字段、方法或属性“System.Web.UI.Page.Request.get”要求对象引用”
            //  2）、本函数中是无法获得 HttpContext 对象的。不报错，直接退出本函数，什么反应也没有...

            // 怎么有时 Session_Start 之后 立即就 Session_End 了呢 ？？ 怎么回事 ？这个现象肯定是不对的。

            Application.Lock();                                          //锁定Application
            Application["onLine"] = (int)Application["onLine"] - 1;      //总访问数量不变，在线人数减1
            Application.UnLock();          
        }
       
    }
}