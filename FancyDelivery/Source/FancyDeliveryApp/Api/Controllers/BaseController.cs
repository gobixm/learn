using System;
using System.Web.Http;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Api.Controllers
{
    public class BaseController : ApiController
    {
        public BaseController(IRepository repository)
        {
            Repository = repository;
        }

        public IRepository Repository { get; }
    }
}
