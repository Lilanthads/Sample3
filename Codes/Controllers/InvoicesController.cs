using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IJSE.POS.Common.Models;
using IJSE.POS.DataAccess.DAL;

using IJSE.POS.Presentation.Web.Models;
using IJSE.POS.Presentation.Web.Models.Invoice;
using IJSE.POS.BusinessService;

namespace IJSE.POS.Presentation.Web.Controllers
{
    public class InvoicesController : Controller
    {
        private PosContext db = new PosContext();

        

        // GET: Invoices
        public ActionResult Index()
        {
            var invoice = db.Invoice.Include(i => i.Customer);
            return View(invoice.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "Name");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,InvoiceDate,ItemTotal,Discount,Tax,InvoiceTotal,CustomerID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoice.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "Name", invoice.CustomerID);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "Name", invoice.CustomerID);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InvoiceDate,ItemTotal,Discount,Tax,InvoiceTotal,CustomerID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "Name", invoice.CustomerID);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoice.Find(id);
            db.Invoice.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult AddInvoiceItem(int itemId, string itemName, double price, int qty)
        {

            if ( Session["InvoiceItems"] == null)
            {
                Session["InvoiceItems"] = new List<InvoiceItems>();
            }

            List<InvoiceItems> itemList = (List<InvoiceItems>)Session["InvoiceItems"];

            InvoiceItems item = new InvoiceItems { ItemID = itemId, ItemName = itemName, ItemPrice = price, Quantity = qty, SubTotal = qty * price };

            itemList.Add(item);

            Session["InvoiceItems"] = itemList;

            ResponseMessage response = new ResponseMessage { Status = true, Message = "success" };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInvoiceItems()
        {
            if (Session["InvoiceItems"] == null)
            {
                Session["InvoiceItems"] = new List<InvoiceItems>();
            }

            List<InvoiceItems> itemList = (List<InvoiceItems>)Session["InvoiceItems"];


            return View(itemList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
