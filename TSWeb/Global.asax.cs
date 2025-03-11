using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TSWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new HandleErrorAttribute());

        }

        protected void Application_BeginRequest()
        {
            // Cấu hình lại HttpContext nếu cần
            HttpContext.Current = this.Context;
        }


    }
}
