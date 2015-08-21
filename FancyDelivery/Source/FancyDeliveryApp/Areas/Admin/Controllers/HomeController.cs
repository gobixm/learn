using System;
using System.Web.Mvc;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public HomeController(IRepository repository) : base(repository)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
