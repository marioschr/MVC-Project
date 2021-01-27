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
    public class RoyschedsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Royscheds
        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.titleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.lorangeSortParm = sortOrder == "lorange" ? "lorange_desc" : "lorange";
            ViewBag.hirangeSortParm = sortOrder == "hirange" ? "hirange_desc" : "hirange";
            ViewBag.royaltySortParm = sortOrder == "royalty" ? "royalty_desc" : "royalty";
            var roysched = db.roysched.Include(r => r.titles);
            switch (sortOrder) {// title,lorange,hirange,royalty
                case "title_desc":
                    roysched = roysched.OrderByDescending(s => s.titles.title);
                    break;
                case "lorange":
                    roysched = roysched.OrderBy(s => s.lorange);
                    break;
                case "lorange_desc":
                    roysched = roysched.OrderByDescending(s => s.lorange);
                    break;
                case "hirange":
                    roysched = roysched.OrderBy(s => s.hirange);
                    break;
                case "hirange_desc":
                    roysched = roysched.OrderByDescending(s => s.hirange);
                    break;
                case "royalty":
                    roysched = roysched.OrderBy(s => s.royalty);
                    break;
                case "royalty_desc":
                    roysched = roysched.OrderByDescending(s => s.royalty);
                    break;
                default:
                    roysched = roysched.OrderBy(s => s.titles.title);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(roysched.ToPagedList(pageNumber, pageSize));
        }

        // GET: Royscheds/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            roysched roysched = db.roysched.Find(id);
            if (roysched == null)
            {
                return View("NotFound");
            }
            return View(roysched);
        }

        // GET: Royscheds/Create
        public ActionResult Create()
        {
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title");
            return View();
        }

        // POST: Royscheds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title_id,lorange,hirange,royalty,roysched_id")] roysched roysched)
        {
            if (ModelState.IsValid)
            {
                db.roysched.Add(roysched);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", roysched.title_id);
            return View(roysched);
        }

        // GET: Royscheds/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            roysched roysched = db.roysched.Find(id);
            if (roysched == null)
            {
                return View("NotFound");
            }
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", roysched.title_id);
            return View(roysched);
        }

        // POST: Royscheds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "title_id,lorange,hirange,royalty,roysched_id")] roysched roysched)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roysched).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", roysched.title_id);
            return View(roysched);
        }

        // GET: Royscheds/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            roysched roysched = db.roysched.Find(id);
            if (roysched == null)
            {
                return View("NotFound");
            }
            return View(roysched);
        }

        // POST: Royscheds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            roysched roysched = db.roysched.Find(id);
            db.roysched.Remove(roysched);
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
