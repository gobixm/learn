using System;
using System.Web.Mvc;
using Infrastructure.Repositories;
using Infrastructure;

namespace FancyDeliveryApp.Areas.Admin.Controllers
{
    public class CategoryController : BaseAdminController
    {
        //
        // GET: /Admin/Category/

        //
        // GET: /Admin/Category/Create
        public CategoryController(IRepository repository) : base(repository)
        {
        }

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Category/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                Repository.NewCategory(category);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Admin/Category/Delete/5
        public ActionResult Delete(int id)
        {            
            return View();
        }

        //
        // POST: /Admin/Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                Repository.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            return View(Repository.GetCategory(id));
        }

        public ActionResult Edit(int id)
        {
            return View(Repository.GetCategory(id));
        }

        //
        // POST: /Admin/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                category.Id = id;
                Repository.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Index()
        {
            return View(Repository.GetCategories());
        }
    }
}
