﻿using System;
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
    public class DebtsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Debts
        public ActionResult Index()
        {
            return View(db.Debts.Where(e => e.UserName == User.Identity.Name).ToList());
        }

        // GET: Debts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Debt debt = db.Debts.Find(id);
            if (debt == null)
            {
                return HttpNotFound();
            }
            return View(debt);
        }
        public ActionResult MakePayment(int? id)
        {
            Debt debt = db.Debts.Where(d => d.DebtId == id).Single();
            return View(debt);
        }
        [HttpPost]
        public ActionResult MakePayment(Debt debt, string Payment)
        {
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var currentDebt = db.Debts.Where(d => d.DebtId == debt.DebtId).Single();
            var currentBudget = db.Budgets.Where(b => b.UserName == User.Identity.Name && b.MonthId == month && b.Year == year).Single();
            currentDebt.AmountPaid += int.Parse(Payment);
            currentDebt.DebtValue -= int.Parse(Payment);
            currentBudget.MonthTotalCost += int.Parse(Payment);
            db.SaveChanges();
            return RedirectToAction("Index", "Budget");
        }
        // GET: Debts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Debts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DebtId,Description,Interest,DebtValue")] Debt debt)
        {
            if (ModelState.IsValid)
            {
                debt.UserName = User.Identity.Name;
                db.Debts.Add(debt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(debt);
        }
        // GET: Debts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Debt debt = db.Debts.Find(id);
            if (debt == null)
            {
                return HttpNotFound();
            }
            return View(debt);
        }

        // POST: Debts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DebtId,UserName,Description,Interest,DebtValue,AmountPaid")] Debt debt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(debt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(debt);
        }

        // GET: Debts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Debt debt = db.Debts.Find(id);
            if (debt == null)
            {
                return HttpNotFound();
            }
            return View(debt);
        }

        // POST: Debts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Debt debt = db.Debts.Find(id);
            db.Debts.Remove(debt);
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
