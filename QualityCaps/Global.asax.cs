using QualityCaps.Models;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace QualityCaps
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }
        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex.Message.Contains("404"))
            {
                Session["ExceptionObject"] = ex;
                Server.ClearError();
                Server.Transfer("PageNotFound.html");
            }
            else
            {
                Session["ExceptionObject"] = ex;
                Server.ClearError();
                Server.Transfer("Error.html");

            }
        }

    }
}
