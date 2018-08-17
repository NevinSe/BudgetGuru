using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using BudgetGuru.Models;

namespace BudgetGuru.Controllers
{
    public class BudgetController: Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var user = User.Identity.Name;
            BudgetModel budgetModel = new BudgetModel();
            budgetModel.Budget = db.Budgets.Where(b => b.UserName == user && b.MonthId == month && b.Year == year).SingleOrDefault();
            if (budgetModel.Budget == default(Budget))
            {
                budgetModel = NewMonth();
            }
            budgetModel.BillsList = db.Bills.Where(b => b.UserName == user && b.IsPaid != true).ToList();
            budgetModel.DebtList = db.Debts.Where(b => b.UserName == user).ToList();
            budgetModel.ExpendituresList = db.Expenditures.Where(b => b.UserName == user && b.ExpendMonth == month && b.ExpendYear == year).ToList();
            budgetModel.Budget.Bills = budgetModel.BillsList.Where(b=>b.IsPaid != true).Select(p => p.MonthlyCost).Sum();
            budgetModel.Budget.Debt = budgetModel.DebtList.Select(p => p.DebtValue).Sum();
            budgetModel.Budget.ExtraExpenditures = budgetModel.ExpendituresList.Select(p => p.ExpenditureCost).Sum();
            budgetModel.Budget.MonthlyExpenditures = db.Bills.Where(b => b.UserName == user && b.IsPaid == true).Select(p => p.MonthlyCost).Sum();
            budgetModel.Budget.MonthTotalCost = budgetModel.Budget.MonthlyExpenditures + budgetModel.Budget.ExtraExpenditures;
            budgetModel.Budget.NetProfit = Math.Round(budgetModel.Budget.Income/12 , 0) - budgetModel.Budget.MonthlyExpenditures;
            db.SaveChanges();
            budgetModel.GoalsList = db.Goals.Where(g => g.UserName == user).ToList();
            budgetModel.BudgetList = db.Budgets.Where(b => b.UserName == user && b.MonthId != month).OrderByDescending(q=>q.MonthId).Select(p => p).ToList();
            budgetModel.Budget.Income = Math.Round(budgetModel.Budget.Income / 12, 0);
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
        public ActionResult Budget()
        {
            Budget budget = new Budget();
            return View(budget);
        }
        [HttpPost]
        public ActionResult Budget(Budget budget)
        {
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var userBudget = db.Budgets.Where(b => b.UserName == User.Identity.Name && b.MonthId == month && b.Year == year).Single();
            userBudget.MonthlyLimit = budget.MonthlyLimit;
            db.SaveChanges();
            return RedirectToAction("Index","Budget");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Home(BudgetModel budgetModel, string Month, string Date, string Year)
        {
            budgetModel.Budget.UserName = User.Identity.Name;
            budgetModel.Goals.AchievementDate = new DateTime(int.Parse(Year), int.Parse(Month), int.Parse(Date)).Date;
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
        public ActionResult BudgetChart()
        {
            string Yellow1 = @"<Chart BackColor=""darkorange"" BackGradientStyle=""TopBottom"" BorderColor=""black"" BorderWidth=""0"" BorderlineDashStyle=""Solid"" Palette=""BrightPastel"">
                                <ChartAreas>
	                                <ChartArea Name=""Default"" _Template_=""All"" BackColor=""Transparent"" BackSecondaryColor=""Black"" BorderColor=""64, 64, 64, 64"" BorderDashStyle=""Solid"" ShadowColor=""Transparent"">
		                                <AxisY>
			                                <LabelStyle Font=""Trebuchet MS, 8.25pt, style=Bold"" />
		                                </AxisY>
		                                <AxisX LineColor=""64, 64, 64, 64"" Interval=""1"" >
			                                <LabelStyle Font=""Trebuchet MS, 8.25pt, style=Bold"" />
		                                </AxisX>
	                                </ChartArea>
                                </ChartAreas>
                                <Legends>
                                <Legend _Template_=""All"" BackColor=""Transparent"" Docking=""Bottom"" Font=""Trebuchet MS, 8.25pt, style=Bold"" LegendStyle=""Row"">
                                </Legend>
                                </Legends>
                                </Chart>";
            double[] profit = new double[12];
           
            for (int i = 1; i < 13; i++)
            {
                var netProfit = db.Budgets.Where(p => p.MonthId == i).Select(p => p.NetProfit).SingleOrDefault();
                if (netProfit == default(double))
                {
                    profit[i - 1] = 0;
                }
                else
                {
                    profit[i - 1] = netProfit;
                }
            }
            var chart = new System.Web.Helpers.Chart(width: 800, height: 400, theme: Yellow1) 
            .AddTitle("Monthly Profits")
            .SetXAxis("Months")
            .SetYAxis("Money")
            .AddSeries(
                chartType: "Line",
                name: "Employee",
                xValue: new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" },
                xField: "Months",
                yValues: profit,
                yFields:"$").Write();
                return null;
        }
        public BudgetModel NewMonth()
        {
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            BudgetModel budgetModel = new BudgetModel();
            budgetModel.Budget.Income = db.Budgets.Where(b => b.UserName == User.Identity.Name && b.MonthId == (month - 1)).Select(i => i.Income).Single();
            budgetModel.Budget.MonthId = month;
            budgetModel.Budget.Year = year;
            return budgetModel;
        }
    }
}