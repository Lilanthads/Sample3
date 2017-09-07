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

namespace IJSE.POS.Presentation.Web.Controllers
{
    public class ItemDetailsController : Controller
    {
        private PosContext db = new PosContext();

        // GET: ItemDetails
        public ActionResult Index()
        {
            var itemDetail = db.ItemDetail.Include(i => i.Item);
            return View(itemDetail.ToList());
        }

        // GET: ItemDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemDetail itemDetail = db.ItemDetail.Find(id);
            if (itemDetail == null)
            {
                return HttpNotFound();
            }
            return View(itemDetail);
        }

        // GET: ItemDetails/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.Item, "ID", "Name");
            return View();
        }

        // POST: ItemDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EffectiveDate,UnitPrice,Active,ItemID")] ItemDetail itemDetail)
        {
            if (ModelState.IsValid)
            {
                db.ItemDetail.Add(itemDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.Item, "ID", "Name", itemDetail.ItemID);
            return View(itemDetail);
        }

        // GET: ItemDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemDetail itemDetail = db.ItemDetail.Find(id);
            if (itemDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Item, "ID", "Name", itemDetail.ItemID);
            return View(itemDetail);
        }

        // POST: ItemDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EffectiveDate,UnitPrice,Active,ItemID")] ItemDetail itemDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Item, "ID", "Name", itemDetail.ItemID);
            return View(itemDetail);
        }

        // GET: ItemDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemDetail itemDetail = db.ItemDetail.Find(id);
            if (itemDetail == null)
            {
                return HttpNotFound();
            }
            return View(itemDetail);
        }

        // POST: ItemDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemDetail itemDetail = db.ItemDetail.Find(id);
            db.ItemDetail.Remove(itemDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
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
