using System;
using System.Web.Mvc;

namespace FancyDeliveryApp.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, controller = "Home" },
                namespaces: new string[] { "FancyDeliveryApp.Areas.Admin.Controllers" }
                );
        }
    }
}
