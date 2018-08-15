using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetGuru.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            using(var dbContext = new Models.ChartModel())
            {
                return View(dbContext.Budgets.ToList());
            }
        }
    }
}