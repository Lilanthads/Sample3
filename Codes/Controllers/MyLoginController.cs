using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;
using IJSE.POS.Common.Models;

using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.Owin;

using IJSE.POS.DataAccess.DAL;

namespace IJSE.POS.Presentation.Web.Controllers
{
    public class MyLoginController : Controller
    {
        // GET: MyLogin
        PosContext _db = new PosContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string userPWD, string userRole)
        {
            //using System.Security.Claims;
            //using Microsoft.Owin.Security;
            //using Microsoft.Owin;

          //  _db.SystemUser.Where( u => u.Name)


            ClaimsIdentity identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, userRole)
            },
            "ApplicationCookie");


            IOwinContext ctx = Request.GetOwinContext();
            IAuthenticationManager authManager = ctx.Authentication;           

    AuthenticationProperties authenticationproperties = new 
                AuthenticationProperties {  IsPersistent = false  };

            authManager.SignIn(authenticationproperties,identity);            

            return RedirectToAction("Index", "Home");   
        }

        public ActionResult Logoff()
        {
            IOwinContext ctx = Request.GetOwinContext();
            IAuthenticationManager authManager = ctx.Authentication;
            authManager.SignOut("ApplicationCookie");

            return RedirectToAction("Login", "MyLogin");
        }



        //using System.Web.Security;


    }
}