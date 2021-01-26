using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        [HandleError]
        public ActionResult Index()
        {
            return View("Error");
        }

        [HandleError]
        public ActionResult NotFound() {
            return View();
        }
    }
}