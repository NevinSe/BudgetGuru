using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudgetGuru.Models;

namespace BudgetGuru.Controllers
{
    public class BillsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bills
        public ActionResult Index()
        {
            return View(db.Bills.Where(e => e.UserName == User.Identity.Name).ToList());
        }

        // GET: Bills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bills bills = db.Bills.Find(id);
            if (bills == null)
            {
                return HttpNotFound();
            }
            return View(bills);
        }

        // GET: Bills/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BillId,BillDescription,MonthlyCost,IsPaid")] Bills bills)
        {
            if (ModelState.IsValid)
            {
                bills.UserName = User.Identity.Name; 
                db.Bills.Add(bills);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bills);
        }

        // GET: Bills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bills bills = db.Bills.Find(id);
            if (bills == null)
            {
                return HttpNotFound();
            }
            return View(bills);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillId,UserName,BillDescription,MonthlyCost,IsPaid")] Bills bills)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bills).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bills);
        }

        // GET: Bills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bills bills = db.Bills.Find(id);
            if (bills == null)
            {
                return HttpNotFound();
            }
            return View(bills);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bills bills = db.Bills.Find(id);
            db.Bills.Remove(bills);
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
