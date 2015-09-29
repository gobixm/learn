using System;
using System.Web.Http;
using FancyDeliveryApp.Api.Extensions;
using FancyDeliveryApp.Api.Models;
using Infrastructure;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Api.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IRepository repository) : base(repository)
        {
        }

        [HttpGet]
        public Pageable<ProductDto> ByCategory(int categoryId, int pageNumber = 1, int pageSize = 20)
        {
            return Repository.GetCategoryProducts(categoryId, pageNumber, pageSize)
                .Map<Product, ProductDto>();
        }
    }
}
