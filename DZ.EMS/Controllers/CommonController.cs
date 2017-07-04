using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DZ.EMS.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/

        public ActionResult Index()
        {


            //在页面中写入如下代码测试：

            //string ServerCode = Convert.ToString(Session["ServerCode"]);

            //if (ServerCode == txtCheak.Text)

            //{

            //Response.Write("正确");

            //}

            //else

            //{

            //Response.Write("错误");

            //}
            return View();
        }
        [AllowAnonymous]
        public void CreateCode()
        {
            string[] str = new string[4];
            string ServerCode = "";

            string[] a = new string[62] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                int rd = random.Next(62);

                str[i] = a[rd];
            }

            string strs = "";

            foreach (string s in str)
            {
                strs = strs + s;
            }

            ServerCode = strs;

            Session["ServerCode"] = ServerCode.ToLower();

            using (Bitmap bitmap = new Bitmap(70, 24))
            {

                using (Graphics g = Graphics.FromImage(bitmap))
                {

                    g.Clear(Color.White);

                    for (int i = 0; i < 10; i++)
                    {

                        int x1 = random.Next(bitmap.Width);

                        int x2 = random.Next(bitmap.Height);

                        int x3 = random.Next(bitmap.Width);

                        int x4 = random.Next(bitmap.Height);

                        g.DrawLine(new Pen(Color.Silver), x1, x2, x3, x4);

                    }

                    for (int i = 0; i < 70; i++)
                    {
                        int i1 = random.Next(bitmap.Width);
                        int i2 = random.Next(bitmap.Height);

                        bitmap.SetPixel(i1, i2, Color.FromArgb(random.Next()));

                    }
                    Point p1 = new Point(0, 0);

                    Point p2 = new Point(75, 24);

                    g.DrawString(ServerCode, new Font("宋体", 16, FontStyle.Italic), new LinearGradientBrush(p1, p2, Color.OrangeRed, Color.Blue), new PointF(8, 0));

                    g.DrawRectangle(new Pen(Color.DarkSeaGreen), 0, 0, bitmap.Width - 1, bitmap.Height - 1);

                }
                
                bitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            }
        }
        [AllowAnonymous]
        public ActionResult GetObjectCombobox(string id)
        {
            BLL.DZ_Object_BLL bll = new BLL.DZ_Object_BLL();//a[4] 对应字段isscan 是否扫描组
            var list = bll.GetAllList();
            list.Add(new string[] { "*", "*", "0", "0", id });//a[4] isscan =0 录入中心
            //var result = from a in list where a[4]==1 select new { id=a[0],text=a[1] };
            var result = list.Where(a => a[4] == id).Select(a => new { id = a[0], text = a[1] }).OrderBy(a => a.id);//a[0] + "_" +
           
            return Json(result);
        }
        [AllowAnonymous]
        public ActionResult GetEMS_FileType(string id)
        {
            BLL.DZ_Dirctory_BLL bll = new BLL.DZ_Dirctory_BLL();//a[4] 
            var list = bll.GetEntities(id);
            list.Add(new string[] { "", id, "全部", "-1", id });//a[4] isscan =0 录入中心
            //var result = from a in list where a[4]==1 select new { id=a[0],text=a[1] };
            var result = list.Where(a => a[3] != "0").Select(a => new { id = a[3], text = a[2] }).OrderBy(a => a.id);//a[0] + "_" +

            return Json(result);
        }
        
    }
}
