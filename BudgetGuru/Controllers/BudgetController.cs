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
    public class BudgetController: Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            BudgetModel budgetModel = new BudgetModel();
            budgetModel.Budget = db.Budgets.Where(b => b.UserName == User.Identity.Name).Single();
            budgetModel.BillsList = db.Bills.Where(b => b.UserName == User.Identity.Name).ToList();
            budgetModel.DebtList = db.Debts.Where(b => b.UserName == User.Identity.Name).ToList();
            budgetModel.ExpendituresList = db.Expenditures.Where(b => b.UserName == User.Identity.Name).ToList();
            budgetModel.Budget.Bills = budgetModel.BillsList.Where(b=>b.IsPaid != true).Select(p => p.MonthlyCost).Sum();
            budgetModel.Budget.Debt = budgetModel.DebtList.Select(p => p.DebtValue).Sum();
            budgetModel.Budget.ExtraExpenditures = budgetModel.ExpendituresList.Select(p => p.ExpenditureCost).Sum();
            budgetModel.Budget.MonthlyExpenditures = budgetModel.Budget.ExtraExpenditures + budgetModel.BillsList.Where(b => b.IsPaid == true).Select(p => p.MonthlyCost).Sum();
            return View(budgetModel);
        }
        public ActionResult Home()
        {
            BudgetModel budgetModel = new BudgetModel()
            {
                Budget = new Budget(),
                Goals = new Goals()
            };
            return View(budgetModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Home(BudgetModel budgetModel, string Month, string Date, string Year)
        {
            budgetModel.Budget.UserName = User.Identity.Name;
            budgetModel.Goals.AchievementDate = new DateTime(int.Parse(Year), int.Parse(Month), int.Parse(Date));
            budgetModel.Goals.UserName = budgetModel.Budget.UserName;
            db.Goals.Add(budgetModel.Goals);
            db.Budgets.Add(budgetModel.Budget);
            db.SaveChanges();
            return RedirectToAction("CreateBills");
        }
        public ActionResult CreateBills()
        {
            Bills bills = new Bills();
            return View(bills);
        }
        [HttpPost]
        public ActionResult CreateBills(Bills bills)
        {
            bills.UserName = User.Identity.Name;
            bills.IsPaid = false;
            db.Bills.Add(bills);
            db.SaveChanges();
            return RedirectToAction("CreateBills");
        }
        public ActionResult CreateDebt()
        {
            Debt debt = new Debt();
            return View();
        }
        [HttpPost]
        public ActionResult CreateDebt(Debt debt)
        {
            debt.UserName = User.Identity.Name;
            db.Debts.Add(debt);
            db.SaveChanges();
            return RedirectToAction("CreateDebt");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BudgetModel budgetModel)
        {

            return RedirectToAction("Home");
        }
    }
}