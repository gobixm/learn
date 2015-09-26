using System;
using System.Web.Mvc;

namespace FancyDeliveryApp.Areas.Default
{
    public class DefaultAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Default";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, controller = "Home" },
                new[] { "FancyDeliveryApp.Areas.Default.Controllers" }
                );
        }
    }
}
