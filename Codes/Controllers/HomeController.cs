using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace IJSE.POS.Presentation.Web.Controllers
{
    public class HomeController : Controller
    {
       [Authorize]
        public ActionResult Index()
        {
            IJSE.POS.Common.Ioc.UnityResolver.Register();
            return View();
        }


        [Authorize (Roles = "Admin,PowerUser")] 
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}