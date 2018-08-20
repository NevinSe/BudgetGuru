using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BudgetGuru.Models;

namespace BudgetGuru.Controllers
{
    public class ExpendituresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Expenditures
        public ActionResult Index()
        {
            return View(db.Expenditures.Where(e=> e.UserName == User.Identity.Name).ToList());
        }

        // GET: Expenditures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expenditures expenditures = db.Expenditures.Find(id);
            if (expenditures == null)
            {
                return HttpNotFound();
            }
            return View(expenditures);
        }

        // GET: Expenditures/Create
        public ActionResult Create()
        {
            Expenditures expenditures = new Expenditures();
            return View(expenditures);
        }

        // POST: Expenditures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpenditureDescription,ExpenditureCost")] Expenditures expenditures, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                expenditures.UserName = User.Identity.Name;
                expenditures.ExpendMonth = DateTime.Now.Month;
                expenditures.ExpendYear = DateTime.Now.Year;
                db.Expenditures.Add(expenditures);
                db.SaveChanges();
                return RedirectToAction("Index", "Budget");
            }
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                ViewBag.Message = "File uploaded successfully.";
            }
            return View(expenditures);
        }

        // GET: Expenditures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expenditures expenditures = db.Expenditures.Find(id);
            if (expenditures == null)
            {
                return HttpNotFound();
            }
            return View(expenditures);
        }

        // POST: Expenditures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpenditureId,UserName,ExpenditureDescription,ExpenditureCost")] Expenditures expenditures)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expenditures).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenditures);
        }

        // GET: Expenditures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expenditures expenditures = db.Expenditures.Find(id);
            if (expenditures == null)
            {
                return HttpNotFound();
            }
            return View(expenditures);
        }

        // POST: Expenditures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expenditures expenditures = db.Expenditures.Find(id);
            db.Expenditures.Remove(expenditures);
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
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase postedFile)
        {
            

            return RedirectToAction("Create", "Expenditures");
        }
    }
}
