using System;
using System.Web.Mvc;

namespace FancyDeliveryApp.Areas.Default.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
