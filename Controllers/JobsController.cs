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
    public class JobsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Jobs
        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.job_descSortParm = String.IsNullOrEmpty(sortOrder) ? "job_desc_desc" : "";
            ViewBag.min_lvlSortParm = sortOrder == "min_lvl" ? "min_lvl_desc" : "min_lvl";
            ViewBag.max_lvlSortParm = sortOrder == "max_lvl" ? "max_lvl_desc" : "max_lvl";
            var jobs = from s in db.jobs select s;
            switch (sortOrder) {// job_desc,min_lvl,max_lvl
                case "job_desc_desc":
                    jobs = jobs.OrderByDescending(s => s.job_desc);
                    break;
                case "min_lvl":
                    jobs = jobs.OrderBy(s => s.min_lvl);
                    break;
                case "min_lvl_desc":
                    jobs = jobs.OrderByDescending(s => s.min_lvl);
                    break;
                case "max_lvl":
                    jobs = jobs.OrderBy(s => s.max_lvl);
                    break;
                case "max_lvl_desc":
                    jobs = jobs.OrderByDescending(s => s.max_lvl);
                    break;
                default:
                    jobs = jobs.OrderBy(s => s.job_desc);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(jobs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Jobs/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return View("NotFound");
            }
            return View(jobs);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "job_id,job_desc,min_lvl,max_lvl")] jobs jobs)
        {
            if (ModelState.IsValid)
            {
                db.jobs.Add(jobs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobs);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return View("NotFound");
            }
            return View(jobs);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "job_id,job_desc,min_lvl,max_lvl")] jobs jobs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobs);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return View("NotFound");
            }
            return View(jobs);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            jobs jobs = db.jobs.Find(id);
            db.jobs.Remove(jobs);
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
