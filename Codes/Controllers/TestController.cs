using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IJSE.POS.DataAccess.DAL;
using IJSE.POS.Common.Models;

namespace IJSE.POS.Presentation.Web.Controllers
{
    public class TestController : Controller
    {
        private PosContext _db = new PosContext();
        // GET: Test
        public ActionResult Index()
        {
            //_db.Customer
            //_db.Invoice
            // _db.Item.Add()


            //retreve All the customers from the DB
            List<Customer> customerList = _db.Customer.ToList();

            //Select the first customer from the list
            Customer cus = customerList.FirstOrDefault();

            return View(cus);
        }
    }
}