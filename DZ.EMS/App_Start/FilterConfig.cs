using DZ.EMS.Filters;
using System.Web;
using System.Web.Mvc;

namespace DZ.EMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomExceptionAttribute());
            //filters.Add(new ErrorFilter());
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new RequiresAuthenticationAttribute());
            
        }
    }
}