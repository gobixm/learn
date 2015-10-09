using System;
using System.Web.Mvc;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public HomeController(IUnitOfWork unitOfWork, IRepository repository) : base(unitOfWork, repository)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
