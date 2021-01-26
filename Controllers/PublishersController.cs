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
    public class PublishersController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Publishers
        public ActionResult Index(string sortOrder, string currentFilter, int? page) {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.pub_idSortParm = String.IsNullOrEmpty(sortOrder) ? "pub_id" : "";
            ViewBag.pub_nameSortParm = sortOrder == "pub_name" ? "pub_name_desc" : "pub_name";
            ViewBag.citySortParm = sortOrder == "city" ? "city_desc" : "city";
            ViewBag.stateSortParm = sortOrder == "state" ? "state_desc" : "state";
            ViewBag.countrySortParm = sortOrder == "country" ? "country_desc" : "country";
            var publishers = db.publishers.Include(p => p.pub_info);
            switch (sortOrder) {// pub_id,pub_name,city,state,country
                case "pub_id_desc":
                    publishers = publishers.OrderByDescending(s => s.pub_id);
                    break;
                case "pub_name":
                    publishers = publishers.OrderBy(s => s.pub_name);
                    break;
                case "pub_name_desc":
                    publishers = publishers.OrderByDescending(s => s.pub_name);
                    break;
                case "city":
                    publishers = publishers.OrderBy(s => s.city);
                    break;
                case "city_desc":
                    publishers = publishers.OrderByDescending(s => s.city);
                    break;
                case "state":
                    publishers = publishers.OrderBy(s => s.state);
                    break;
                case "state_desc":
                    publishers = publishers.OrderByDescending(s => s.state);
                    break;
                case "country":
                    publishers = publishers.OrderBy(s => s.country);
                    break;
                case "country_desc":
                    publishers = publishers.OrderByDescending(s => s.country);
                    break;
                default:
                    publishers = publishers.OrderBy(s => s.pub_id);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(publishers.ToPagedList(pageNumber, pageSize));

        }

        // GET: Publishers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            publishers publishers = db.publishers.Find(id);
            if (publishers == null)
            {
                return View("NotFound");
            }
            return View(publishers);
        }

        // GET: Publishers/Create
        public ActionResult Create()
        {
            ViewBag.pub_id = new SelectList(db.pub_info, "pub_id", "pr_info");
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pub_id,pub_name,city,state,country")] publishers publishers)
        {
            if (ModelState.IsValid)
            {
                db.publishers.Add(publishers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pub_id = new SelectList(db.pub_info, "pub_id", "pr_info", publishers.pub_id);
            return View(publishers);
        }

        // GET: Publishers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            publishers publishers = db.publishers.Find(id);
            if (publishers == null)
            {
                return View("NotFound");
            }
            ViewBag.pub_id = new SelectList(db.pub_info, "pub_id", "pr_info", publishers.pub_id);
            return View(publishers);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pub_id,pub_name,city,state,country")] publishers publishers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publishers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pub_id = new SelectList(db.pub_info, "pub_id", "pr_info", publishers.pub_id);
            return View(publishers);
        }

        // GET: Publishers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            publishers publishers = db.publishers.Find(id);
            if (publishers == null)
            {
                return View("NotFound");
            }
            return View(publishers);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            publishers publishers = db.publishers.Find(id);
            db.publishers.Remove(publishers);
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
