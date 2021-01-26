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
    public class DiscountsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Discounts
        public ActionResult Index(string sortOrder, string currentFilter, int? page) {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.discounttypeSortParm = String.IsNullOrEmpty(sortOrder) ? "discounttype_desc" : "";
            ViewBag.stor_idSortParm = sortOrder == "stor_id" ? "stor_id_desc" : "stor_id";
            ViewBag.lowqtySortParm = sortOrder == "lowqty" ? "lowqty_desc" : "lowqty";
            ViewBag.highqtySortParm = sortOrder == "highqty" ? "highqty_desc" : "highqty";
            ViewBag.discountSortParm = sortOrder == "discount" ? "discount_desc" : "discount";

            var discounts = db.discounts.Include(d => d.stores);
            switch (sortOrder) { //discounttype,stor_id,lowqty,highqty,discoun
                case "discounttype_desc":
                    discounts = discounts.OrderByDescending(s => s.discounttype);
                    break;
                case "stor_id":
                    discounts = discounts.OrderBy(s => s.stor_id);
                    break;
                case "stor_id_desc":
                    discounts = discounts.OrderByDescending(s => s.stor_id);
                    break;
                case "lowqty":
                    discounts = discounts.OrderBy(s => s.lowqty);
                    break;
                case "lowqty_desc":
                    discounts = discounts.OrderByDescending(s => s.lowqty);
                    break;
                case "highqty":
                    discounts = discounts.OrderBy(s => s.highqty);
                    break;
                case "highqty_desc":
                    discounts = discounts.OrderByDescending(s => s.highqty);
                    break;
                case "discount":
                    discounts = discounts.OrderBy(s => s.discount);
                    break;
                case "discount_desc":
                    discounts = discounts.OrderByDescending(s => s.discount);
                    break;
                default:
                    discounts = discounts.OrderBy(s => s.discounttype);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(discounts.ToPagedList(pageNumber, pageSize));
        }

        // GET: Discounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
            }
            discounts discounts = db.discounts.Find(id);
            if (discounts == null)
            {
                return View("NotFound");
            }
            return View(discounts);
        }

        // GET: Discounts/Create
        public ActionResult Create()
        {
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name");
            return View();
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "discounttype,stor_id,lowqty,highqty,discount,discount_id")] discounts discounts)
        {
            if (ModelState.IsValid)
            {
                db.discounts.Add(discounts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", discounts.stor_id);
            return View(discounts);
        }

        // GET: Discounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            discounts discounts = db.discounts.Find(id);
            if (discounts == null)
            {
                return View("NotFound");
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", discounts.stor_id);
            return View(discounts);
        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "discounttype,stor_id,lowqty,highqty,discount,discount_id")] discounts discounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", discounts.stor_id);
            return View(discounts);
        }

        // GET: Discounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            discounts discounts = db.discounts.Find(id);
            if (discounts == null)
            {
                return View("NotFound");
            }
            return View(discounts);
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            discounts discounts = db.discounts.Find(id);
            db.discounts.Remove(discounts);
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
