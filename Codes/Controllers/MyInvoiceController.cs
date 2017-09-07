using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using IJSE.POS.Common.Models;
using IJSE.POS.DataAccess.DAL;

using IJSE.POS.Presentation.Web.Models;
using IJSE.POS.Presentation.Web.Models.Invoice;
using IJSE.POS.Common.Ioc;
using IJSE.POS.BusinessService.Contracts;

using IJSE.POS.BusinessService;

namespace IJSE.POS.Presentation.Web.Controllers
{
    //[Authorize(Roles = "Operator")]
    public class MyInvoiceController : Controller
    {
      //  private PosContext db = new PosContext();

        //private IInvoiceManger _invoiceManger = UnityResolver.Resolve<IInvoiceManger>();

        //private InvoiceManger InvoiceManger = new InvoiceManger(repository, log);
        
        //InvoiceManger manger  = new InvoiceManger()
            
            // GET: MyInvoice
        public ActionResult Index()
        {
            
            return View();
        }

       // [Authorize]
        public ActionResult Create()
        {
            IInvoiceManger invoiceManger2 = UnityResolver.Resolve<IInvoiceManger>();
           // IInvoiceManger invoiceManger = new InvoiceManger(repository, log);

          //  ViewBag.CustomerID = new SelectList(db.Customer, "ID", "Name");
            return View();
        }

        public ActionResult AddInvoiceItem 
            (int itemId, string itemName, double price, int qty)
        {

            if (Session["InvoiceItems"] == null)
            {
                Session["InvoiceItems"] = new List<InvoiceItems>();
            }

        List<InvoiceItems> itemList = 
                (List<InvoiceItems>)Session["InvoiceItems"];

            InvoiceItems item = new InvoiceItems {
                ItemID = itemId, ItemName = itemName,
                ItemPrice = price, Quantity = qty, SubTotal = qty * price };

            itemList.Add(item);
            Session["InvoiceItems"] = itemList;
            // ResponseMessage response = 
            //    new ResponseMessage { Status = true, Message = "success" };
            string response = "OK";
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetInvoiceItems()
        {
            if (Session["InvoiceItems"] == null)
            {
                Session["InvoiceItems"] = new List<InvoiceItems>();
            }

            List<InvoiceItems> itemList = 
                (List<InvoiceItems>)Session["InvoiceItems"];
            
            return View(itemList);
        }
    }
}