using System;
using System.Web.Mvc;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Areas.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
        public BaseAdminController(IRepository repository)
        {
            Repository = repository;
        }

        public IRepository Repository { get; }
    }
}
