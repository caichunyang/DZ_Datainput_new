using DZ.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace DZ.EMS.Filters
{
    public class RightFilters
    {
    }
    /// <summary> 
    /// Checks the User's authentication using FormsAuthentication 
    /// and redirects to the Login Url for the application on fail 
    /// </summary> 
    [RequiresAuthentication]
    public class RequiresAuthenticationAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            ////redirect if not authenticated 
            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{

            //    //use the current url for the redirect 
            //    string redirectOnSuccess = filterContext.HttpContext.Request.Url.AbsolutePath;

            //    //send them off to the login page 
            //    string redirectUrl = string.Format("?ReturnUrl={0}", redirectOnSuccess);
            //    string loginUrl = FormsAuthentication.LoginUrl + redirectUrl;
            //    filterContext.HttpContext.Response.Redirect(loginUrl, true);

            //}
            //if (filterContext == null)
            //{
            //    throw new ArgumentNullException("filterContext");
            //}


            #region 允许匿名
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Length > 0 |
                 filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Length > 0)
            {
                //filterContext.HttpContext.SkipAuthorization = true;
                return;
            }
            #endregion


            #region 是否登录
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)//未登录情况
            {
                //ActionResult result = new HttpUnauthorizedResult();
                //if (filterContext.HttpContext.Request.IsAjaxRequest())//如果是Ajax操作，那么需要返回一个状态值，通过js返回指定页面
                //{
                //    string url = UrlHelper.GenerateUrl("Default", "Login", "Home", filterContext.RouteData.Values, RouteTable.Routes, filterContext.RequestContext, true);
                //    result = new JsonResult() { Data = new { Error = true, Url = url } };
                //}
                //filterContext.Result = result;
                //return;
                HttpCookie sessionIdCookie = filterContext.HttpContext.Request.Cookies["ASP.NET_SessionId"];
                if (sessionIdCookie == null)
                {
                    //这里将用户权限放入session
                }

                #region Session是否过期
                SessionModel currentsession = filterContext.HttpContext.Session["user"] as SessionModel;
                if (currentsession == null)//session过期情况
                {
                    //ActionResult result = new HttpUnauthorizedResult();
                    //if (filterContext.HttpContext.Request.IsAjaxRequest())
                    //{
                    //    string url = UrlHelper.GenerateUrl("Default", "Login", "Home", filterContext.RouteData.Values, RouteTable.Routes, filterContext.RequestContext, true);
                    //    result = new JsonResult() { Data = new { Error = true, Url = url } };
                    //}
                    //filterContext.Result = result;
                    //return;
                    filterContext.HttpContext.Response.Redirect("/home/login");
                }
                else
                {
                    string controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
                    string action = filterContext.RouteData.Values["action"].ToString().ToLower();
                    var controlerlist = currentsession.ModuleList.Where(a => a.Controler == controller);
                    if (controlerlist != null && controlerlist.Count() > 0)
                    {
                        if (!string.IsNullOrEmpty(action))
                        {
                            var isaction = controlerlist.Where(b => b.Action == action).Count() > 0;
                            if (isaction)
                            {
                                return;
                            }
                            else
                            {
                                filterContext.HttpContext.Response.Redirect("/home/login/r");
                            }
                            
                        }
                        else
                        {
                            return;
                        }

                    }
                    else
                    {
                        filterContext.HttpContext.Response.Redirect("/home/login/r");
                    }
                    //if (currentsession.ModuleList.Where(a => a.Controler == controller).Count > 0)
                    //{ }
                    //else
                    //{

                    //}
                }
                #endregion
            }
            #endregion

            //if (filterContext.RouteData.Values["controller"].ToString()=="Home")
            //{
            //    return;
            //}
        }
    }

    ///// <summary> 
    ///// Checks the User's role using FormsAuthentication 
    ///// and throws and UnauthorizedAccessException if not authorized 
    ///// </summary> 
    //public class RequiresRoleAttribute : ActionFilterAttribute
    //{

    //    public string RoleToCheckFor { get; set; }

    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        //redirect if the user is not authenticated 
    //        if (!String.IsNullOrEmpty(RoleToCheckFor))
    //        {

    //            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
    //            {

    //                //use the current url for the redirect 
    //                string redirectOnSuccess = filterContext.HttpContext.Request.Url.AbsolutePath;

    //                //send them off to the login page 
    //                string redirectUrl = string.Format("?ReturnUrl={0}", redirectOnSuccess);
    //                string loginUrl = FormsAuthentication.LoginUrl + redirectUrl;
    //                filterContext.HttpContext.Response.Redirect(loginUrl, true);

    //            }
    //            else
    //            {
    //                bool isAuthorized = filterContext.HttpContext.User.IsInRole(this.RoleToCheckFor);
    //                if (!isAuthorized)
    //                    throw new UnauthorizedAccessException("You are not authorized to view this page");
    //            }
    //        }
    //        else
    //        {
    //            throw new InvalidOperationException("No Role Specified");
    //        }
    //    }
    //} 

}