using System;
using System.Web.Mvc;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Areas.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
        public BaseAdminController(IUnitOfWork unitOfWork, IRepository repository)
        {
            UnitOfWork = unitOfWork;
            Repository = repository;
        }

        public IUnitOfWork UnitOfWork { get; }
        public IRepository Repository { get; }
    }
}
