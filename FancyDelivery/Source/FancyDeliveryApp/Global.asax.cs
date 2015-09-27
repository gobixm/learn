using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FancyDeliveryApp.Areas.Admin;
using FancyDeliveryApp.Areas.Default;

namespace FancyDeliveryApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(config => WebApiConfig.Register(config));

            var adminArea = new AdminAreaRegistration();
            var adminAreaContext = new AreaRegistrationContext(adminArea.AreaName, RouteTable.Routes);
            adminArea.RegisterArea(adminAreaContext);
            
            var defaultArea = new DefaultAreaRegistration();
            var defaultAreaContext = new AreaRegistrationContext(defaultArea.AreaName, RouteTable.Routes);
            defaultArea.RegisterArea(defaultAreaContext);

            

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
