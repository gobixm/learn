using System;
using System.Web.Mvc;
using Infrastructure.Repositories;

namespace FancyDeliveryApp.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Category/Edit/5

        //
        // GET: /Admin/Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
