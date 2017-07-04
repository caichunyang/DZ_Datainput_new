using DZ.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DZ.EMS.Controllers
{
 
    public class PingAnInputController : Controller
    {
        //
        // GET: /PingAnInput/

           [AllowAnonymous]
        public ActionResult Insert(string model)
        {
            try
            {
                string skey = OupPlugins.Decrypt(model);
                dz_input_pingan inputmodel = Common.JSON.DeserializeObject<dz_input_pingan>(skey);
                BLL.DZ_Input_pingan_BLL bll = new BLL.DZ_Input_pingan_BLL();
                bool result = false;
                if (inputmodel != null && !string.IsNullOrEmpty(inputmodel.userid))
                {
                    inputmodel.inputime = DateTime.Now;
                    inputmodel.inputlvalue = inputmodel.inputlvalue.Trim();
                    inputmodel.inputlength = System.Text.Encoding.GetEncoding("GB2312").GetBytes(inputmodel.inputlvalue).Length;
                    result = bll.Add(inputmodel);
                }
                return Content(result ? "1" : "0");
            }
            catch (Exception)
            {
                return Content( "0");
                //throw;
            }
        }

    }
}
