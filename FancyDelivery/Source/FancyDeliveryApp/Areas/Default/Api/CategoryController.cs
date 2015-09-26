using System;
using System.Collections.Generic;
using System.Linq;
using FancyDeliveryApp.Areas.Default.Controllers;
using Infrastructure;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Areas.Default.Api
{
    public class CategoryController : BaseController
    {
        public CategoryController(IRepository repository) : base(repository)
        {
        }

        public List<Category> GetAllCategories()
        {
            var categories = Repository.GetCategories().ToList();
            categories.ForEach(x=>x.Products = null);
            return categories;
        }
    }
}
