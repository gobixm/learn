using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using FancyDeliveryApp.Api.Models;
using Infrastructure;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Api.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork unitOfWork, IRepository repository) : base(unitOfWork, repository)
        {
        }

        [HttpGet]
        public List<CategoryDto> All()
        {
            var categories = Repository.GetCategories().ToList();
            return Mapper.Map<ICollection<Category>, List<CategoryDto>>(categories);
        }
    }
}
