using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Project.Models;
using PagedList;

namespace MVC_Project.Controllers
{
    public class AuthorsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: authors
        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.au_lnameSortParm = String.IsNullOrEmpty(sortOrder) ? "au_lname_desc" : "";
            ViewBag.au_fnameSortParm = sortOrder == "au_fname" ? "au_fname_desc" : "au_fname";
            ViewBag.phoneSortParm = sortOrder == "phone" ? "phone_desc" : "phone";
            ViewBag.addressSortParm = sortOrder == "address" ? "address_desc" : "address";
            ViewBag.citySortParm = sortOrder == "city" ? "city_desc" : "city";
            ViewBag.stateSortParm = sortOrder == "state" ? "state_desc" : "state";
            ViewBag.zipSortParm = sortOrder == "zip" ? "zip_desc" : "zip";
            ViewBag.contractSortParm = sortOrder == "contract" ? "contract_desc" : "contract";

            var authors= from s in db.authors select s;
            switch (sortOrder) { //au_lname,au_fname,phone,address,city,state,zip,contract
                case "au_lname_desc":
                    authors = authors.OrderByDescending(s => s.au_lname);
                    break;
                case "au_fname":
                    authors = authors.OrderBy(s => s.au_fname);
                    break;
                case "au_fname_desc":
                    authors = authors.OrderByDescending(s => s.au_fname);
                    break;
                case "phone":
                    authors = authors.OrderBy(s => s.phone);
                    break;
                case "phone_desc":
                    authors = authors.OrderByDescending(s => s.phone);
                    break;
                case "address":
                    authors = authors.OrderBy(s => s.address);
                    break;
                case "address_desc":
                    authors = authors.OrderByDescending(s => s.address);
                    break;
                case "city":
                    authors = authors.OrderBy(s => s.city);
                    break;
                case "city_desc":
                    authors = authors.OrderByDescending(s => s.city);
                    break;
                case "state":
                    authors = authors.OrderBy(s => s.state);
                    break;
                case "state_desc":
                    authors = authors.OrderByDescending(s => s.state);
                    break;
                case "zip":
                    authors = authors.OrderBy(s => s.zip);
                    break;
                case "zip_desc":
                    authors = authors.OrderByDescending(s => s.zip);
                    break;
                case "contract":
                    authors = authors.OrderBy(s => s.contract);
                    break;
                case "contract_desc":
                    authors = authors.OrderByDescending(s => s.contract);
                    break;
                default:
                    authors = authors.OrderBy(s => s.au_lname);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(authors.ToPagedList(pageNumber, pageSize));
        }

        // GET: authors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            authors authors = db.authors.Find(id);
            if (authors == null)
            {
                return View("NotFound");
            }
            return View(authors);
        }

        // GET: authors/Create
        public ActionResult Create()
        {
            return View(new Models.authors());
        }

        // POST: authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "au_id,au_lname,au_fname,phone,address,city,state,zip,contract")] authors authors)
        {
            if (ModelState.IsValid)
            {
                db.authors.Add(authors);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return RedirectToAction("Create");
                }
                return RedirectToAction("Index");
            }

            return View(authors);
        }

        // GET: authors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            authors authors = db.authors.Find(id);
            if (authors == null)
            {
                return View("NotFound");
            }
            return View(authors);
        }

        // POST: authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "au_id,au_lname,au_fname,phone,address,city,state,zip,contract")] authors authors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(authors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(authors);
        }

        // GET: authors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            authors authors = db.authors.Find(id);
            if (authors == null)
            {
                return View("NotFound");
            }
            return View(authors);
        }

        // POST: authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            authors authors = db.authors.Find(id);
            db.authors.Remove(authors);
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
