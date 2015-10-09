using System;
using System.Web.Http;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Api.Controllers
{
    public class BaseController : ApiController
    {
        public BaseController(IUnitOfWork unitOfWork, IRepository repository)
        {
            UnitOfWork = unitOfWork;
            Repository = repository;
        }

        public IUnitOfWork UnitOfWork { get; }
        public IRepository Repository { get; }
    }
}
