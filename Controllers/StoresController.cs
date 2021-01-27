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
    public class StoresController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Stores
        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.stor_nameSortParm = String.IsNullOrEmpty(sortOrder) ? "stor_name_desc" : "";
            ViewBag.stor_addressSortParm = sortOrder == "stor_address" ? "stor_address_desc" : "stor_address";
            ViewBag.citySortParm = sortOrder == "city" ? "city_desc" : "city";
            ViewBag.stateSortParm = sortOrder == "state" ? "state_desc" : "state";
            ViewBag.zipSortParm = sortOrder == "zip" ? "zip_desc" : "zip";

            var stores = from s in db.stores select s;

            switch (sortOrder) {// stor_name,stor_address,city,state,zip
                case "stor_name_desc":
                    stores = stores.OrderByDescending(s => s.stor_name);
                    break;
                case "stor_address":
                    stores = stores.OrderBy(s => s.stor_address);
                    break;
                case "stor_address_desc":
                    stores = stores.OrderByDescending(s => s.stor_address);
                    break;
                case "city":
                    stores = stores.OrderBy(s => s.city);
                    break;
                case "city_desc":
                    stores = stores.OrderByDescending(s => s.city);
                    break;
                case "state":
                    stores = stores.OrderBy(s => s.state);
                    break;
                case "state_desc":
                    stores = stores.OrderByDescending(s => s.state);
                    break;
                case "zip":
                    stores = stores.OrderBy(s => s.zip);
                    break;
                case "zip_desc":
                    stores = stores.OrderByDescending(s => s.zip);
                    break;
                default:
                    stores = stores.OrderBy(s => s.stor_name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(stores.ToPagedList(pageNumber, pageSize));
        }

        // GET: Stores/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            stores stores = db.stores.Find(id);
            if (stores == null)
            {
                return View("NotFound");
            }
            return View(stores);
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stor_id,stor_name,stor_address,city,state,zip")] stores stores)
        {
            if (ModelState.IsValid)
            {
                db.stores.Add(stores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stores);
        }

        // GET: Stores/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            stores stores = db.stores.Find(id);
            if (stores == null)
            {
                return View("NotFound");
            }
            return View(stores);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stor_id,stor_name,stor_address,city,state,zip")] stores stores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stores);
        }

        // GET: Stores/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            stores stores = db.stores.Find(id);
            if (stores == null)
            {
                return View("NotFound");
            }
            return View(stores);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            stores stores = db.stores.Find(id);
            db.stores.Remove(stores);
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
