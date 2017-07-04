using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DZ.EMS.Filters
{
    public class ErrorFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            //在派生类中重写时，获取或设置一个值，该值指定是否禁用IIS自定义错误。
            //filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            //filterContext.Result = new ViewResult {ViewName="Error" };
            //UrlHelper url = new UrlHelper(filterContext.RequestContext);
            //filterContext.Result = new RedirectResult(url.Action("error", "home"));
        
            filterContext.Result = new RedirectResult("/home/error"); 
           // filterContext.HttpContext.Response.Redirect("/home/error");
            base.OnException(filterContext);
        }
    }
}