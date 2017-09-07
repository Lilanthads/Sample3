using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IJSE.POS.Presentation.Web.Controllers
{
    public class DynamicController : Controller
    {
        // GET: Dynamic
        public ActionResult Test(string viewName)
        {
            //  ViewBag.Message = "Your contact page.";

            return View("~/Views/Dynamic/" + viewName + ".cshtml");
        }
    }
}