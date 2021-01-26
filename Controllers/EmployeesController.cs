using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Project.Models;
using PagedList;

namespace MVC_Project.Controllers
{
    public class EmployeesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Employees
        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.fnameSortParm = String.IsNullOrEmpty(sortOrder) ? "fname_desc" : "";
            ViewBag.minitSortParm = sortOrder == "minit" ? "minit_desc" : "minit";
            ViewBag.lnameSortParm = sortOrder == "lname" ? "lname_desc" : "lname";
            ViewBag.job_lvlSortParm = sortOrder == "job_lvl" ? "job_lvl_desc" : "job_lvl";
            ViewBag.hire_dateSortParm = sortOrder == "hire_date" ? "hire_date_desc" : "hire_date";
            ViewBag.job_descSortParm = sortOrder == "job_desc" ? "job_desc_desc" : "job_desc";
            ViewBag.pub_nameSortParm = sortOrder == "pub_name" ? "pub_name_desc" : "pub_name";
            var employee = db.employee.Include(e => e.jobs).Include(e => e.publishers);
            switch (sortOrder) {// fname,minit,lname,job_lvl,hire_date,job_desc,pub_name
                case "fname_desc":
                    employee = employee.OrderByDescending(s => s.fname);
                    break;
                case "minit":
                    employee = employee.OrderBy(s => s.minit);
                    break;
                case "minit_desc":
                    employee = employee.OrderByDescending(s => s.minit);
                    break;
                case "lname":
                    employee = employee.OrderBy(s => s.lname);
                    break;
                case "lname_desc":
                    employee = employee.OrderByDescending(s => s.lname);
                    break;
                case "job_lvl":
                    employee = employee.OrderBy(s => s.job_lvl);
                    break;
                case "job_lvl_desc":
                    employee = employee.OrderByDescending(s => s.job_lvl);
                    break;
                case "hire_date":
                    employee = employee.OrderBy(s => s.hire_date);
                    break;
                case "hire_date_desc":
                    employee = employee.OrderByDescending(s => s.hire_date);
                    break;
                case "job_desc":
                    employee = employee.OrderBy(s => s.jobs.job_desc);
                    break;
                case "job_desc_desc":
                    employee = employee.OrderByDescending(s => s.jobs.job_desc);
                    break;
                case "pub_name":
                    employee = employee.OrderBy(s => s.publishers.pub_name);
                    break;
                case "pub_name_desc":
                    employee = employee.OrderByDescending(s => s.publishers.pub_name);
                    break;
                default:
                    employee = employee.OrderBy(s => s.fname);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(employee.ToPagedList(pageNumber, pageSize));
        }

        // GET: Employees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            employee employee = db.employee.Find(id);
            if (employee == null)
            {
                return View("NotFound");
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_desc");
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emp_id,fname,minit,lname,job_id,job_lvl,pub_id,hire_date")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_desc", employee.job_id);
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", employee.pub_id);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            employee employee = db.employee.Find(id);
            if (employee == null)
            {
                return View("NotFound");
            }
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_desc", employee.job_id);
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", employee.pub_id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "emp_id,fname,minit,lname,job_id,job_lvl,pub_id,hire_date")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_desc", employee.job_id);
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", employee.pub_id);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            employee employee = db.employee.Find(id);
            if (employee == null)
            {
                return View("NotFound");
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            employee employee = db.employee.Find(id);
            db.employee.Remove(employee);
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
