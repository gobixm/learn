using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Infrastructure;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Api.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IRepository repository) : base(repository)
        {
        }

        [HttpGet]
        public List<Category> All()
        {
            var categories = Repository.GetCategories().ToList();
            categories.ForEach(x => x.Products = null);
            return categories;
        }
    }
}
