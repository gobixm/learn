using System;
using System.Web.Mvc;
using FancyDeliveryApp.Areas.Admin.Models;
using Infrastructure;

namespace FancyDeliveryApp.Areas.Admin.Controllers
{
    public class DatabaseController : Controller
    {
        //
        // GET: /Admin/Database/
        public ActionResult Index()
        {
            return View();
        }

        //
        // Post: /Admin/Database/PrepareDatabase
        [HttpPost]
        public ActionResult PrepareDatabase()
        {
            try
            {
                SessionHelper.PrepareDatabase();
                return PartialView("_PrepareDatabaseResult", new PrepareDatabaseResultModel { Success = true });
            }
            catch (Exception)
            {
                return PartialView("_PrepareDatabaseResult", new PrepareDatabaseResultModel { Success = false });
            }
        }
    }
}
