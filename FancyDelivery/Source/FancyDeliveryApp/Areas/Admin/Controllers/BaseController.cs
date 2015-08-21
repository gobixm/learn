using System.Web.Mvc;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        private readonly IRepository repository;

        public BaseController(IRepository repository)
         {
             this.repository = repository;
         }
    }
}