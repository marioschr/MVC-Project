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
    public class TitleauthorsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Titleauthors
        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.au_lnameSortParm = String.IsNullOrEmpty(sortOrder) ? "au_lname_desc" : "";
            ViewBag.titleSortParm = sortOrder == "title" ? "title_desc" : "title";
            ViewBag.au_ordSortParm = sortOrder == "au_ord" ? "au_ord_desc" : "au_ord";
            ViewBag.royaltyperSortParm = sortOrder == "royaltyper" ? "royaltyper_desc" : "royaltyper";

            var titleauthor = db.titleauthor.Include(t => t.authors).Include(t => t.titles);
            switch (sortOrder) {// stor_name,stor_address,city,state,zip
                case "au_lname_desc":
                    titleauthor = titleauthor.OrderByDescending(s => s.authors.au_lname);
                    break;
                case "title":
                    titleauthor = titleauthor.OrderBy(s => s.titles.title);
                    break;
                case "title_desc":
                    titleauthor = titleauthor.OrderByDescending(s => s.titles.title);
                    break;
                case "au_ord":
                    titleauthor = titleauthor.OrderBy(s => s.au_ord);
                    break;
                case "au_ord_desc":
                    titleauthor = titleauthor.OrderByDescending(s => s.au_ord);
                    break;
                case "royaltyper":
                    titleauthor = titleauthor.OrderBy(s => s.royaltyper);
                    break;
                case "royaltyper_desc":
                    titleauthor = titleauthor.OrderByDescending(s => s.royaltyper);
                    break;
                default:
                    titleauthor = titleauthor.OrderBy(s => s.authors.au_lname);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(titleauthor.ToPagedList(pageNumber, pageSize));
        }

        // GET: Titleauthors/Details/5
        public ActionResult Details(string au_id, string title_id)
        {
            if (au_id == null || title_id == null)
            {
                return View("NotFound");
            }
            titleauthor titleauthor = db.titleauthor.Find(au_id,title_id);
            if (titleauthor == null)
            {
                return View("NotFound");
            }
            return View(titleauthor);
        }

        // GET: Titleauthors/Create
        public ActionResult Create()
        {
            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname");
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title");
            return View();
        }

        // POST: Titleauthors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "au_id,title_id,au_ord,royaltyper")] titleauthor titleauthor)
        {
            if (ModelState.IsValid)
            {
                db.titleauthor.Add(titleauthor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname", titleauthor.au_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", titleauthor.title_id);
            return View(titleauthor);
        }

        // GET: Titleauthors/Edit/5
        public ActionResult Edit(string au_id, string title_id)
        {
            if (au_id == null || title_id == null)
            {
                return View("NotFound");
            }
            titleauthor titleauthor = db.titleauthor.Find(au_id, title_id);
            if (titleauthor == null)
            {
                return View("NotFound");
            }
            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname", titleauthor.au_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", titleauthor.title_id);
            return View(titleauthor);
        }

        // POST: Titleauthors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "au_id,title_id,au_ord,royaltyper")] titleauthor titleauthor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(titleauthor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname", titleauthor.au_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", titleauthor.title_id);
            return View(titleauthor);
        }

        // GET: Titleauthors/Delete/5
        public ActionResult Delete(string au_id, string title_id)
        {
            if (au_id == null || title_id == null)
            {
                return View("NotFound");
            }
            titleauthor titleauthor = db.titleauthor.Find(au_id, title_id);
            if (titleauthor == null)
            {
                return View("NotFound");
            }
            return View(titleauthor);
        }

        // POST: Titleauthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string au_id, string title_id) {
            titleauthor titleauthor = db.titleauthor.Find(au_id, title_id);
            db.titleauthor.Remove(titleauthor);
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
