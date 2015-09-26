using System;
using System.Web.Http;
using System.Web.Mvc;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Areas.Default.Controllers
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