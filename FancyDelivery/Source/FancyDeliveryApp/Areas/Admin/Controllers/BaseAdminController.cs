using System.Web.Mvc;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Areas.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
        private readonly IRepository _repository;
        public IRepository Repository
        {
            get { return _repository; }
        }

        public BaseAdminController(IRepository repository)
         {
             this._repository = repository;
         }
    }
}