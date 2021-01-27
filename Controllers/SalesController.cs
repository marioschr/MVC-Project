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
    public class SalesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Sales
        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.titleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.qtySortParm = sortOrder == "qty" ? "qty_desc" : "qty";
            ViewBag.paytermsSortParm = sortOrder == "payterms" ? "payterms_desc" : "payterms";
            ViewBag.stor_nameSortParm = sortOrder == "stor_name" ? "stor_name_desc" : "stor_name";
            ViewBag.ord_dateSortParm = sortOrder == "ord_date" ? "ord_date_desc" : "ord_date";

            var sales = db.sales.Include(s => s.stores).Include(s => s.titles);
            switch (sortOrder) {// titlord_date,qty,payterms,stor_name,title
                case "title_desc":
                    sales = sales.OrderByDescending(s => s.titles.title);
                    break;
                case "qty":
                    sales = sales.OrderBy(s => s.qty);
                    break;
                case "qty_desc":
                    sales = sales.OrderByDescending(s => s.qty);
                    break;
                case "payterms":
                    sales = sales.OrderBy(s => s.payterms);
                    break;
                case "payterms_desc":
                    sales = sales.OrderByDescending(s => s.payterms);
                    break;
                case "ord_date":
                    sales = sales.OrderBy(s => s.ord_date);
                    break;
                case "ord_date_desc":
                    sales = sales.OrderByDescending(s => s.ord_date);
                    break;
                case "stor_name":
                    sales = sales.OrderBy(s => s.stores.stor_name);
                    break;
                case "stor_name_desc":
                    sales = sales.OrderByDescending(s => s.stores.stor_name);
                    break;
                default:
                    sales = sales.OrderBy(s => s.titles.title);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(sales.ToPagedList(pageNumber, pageSize));
        }

        // GET: Sales/Details/5
        public ActionResult Details(string stor_id, string ord_num, string title_id)
        {
            if (stor_id == null || ord_num == null || title_id == null)
            {
                return View("NotFound");
            }
            sales sales = db.sales.Find(stor_id, ord_num, title_id);
            if (sales == null)
            {
                return View("NotFound");
            }
            return View(sales);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name");
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stor_id,ord_num,ord_date,qty,payterms,title_id")] sales sales)
        {
            if (ModelState.IsValid)
            {
                db.sales.Add(sales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sales.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", sales.title_id);
            return View(sales);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(string stor_id, string ord_num, string title_id) {
            if (ord_num == null) {
                return View("NotFound");
            }
            sales sales = db.sales.Find(stor_id, ord_num, title_id);

            if (sales == null)
            {
                return View("NotFound");
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sales.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", sales.title_id);
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stor_id,ord_num,ord_date,qty,payterms,title_id")] sales sales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sales.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", sales.title_id);
            return View(sales);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(string stor_id, string ord_num, string title_id)
        {
            if (stor_id == null)
            {
                return View("NotFound");
            }
            sales sales = db.sales.Find(stor_id, ord_num, title_id);
            if (sales == null)
            {
                return View("NotFound");
            }
            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string stor_id, string ord_num, string title_id)
        {
            sales sales = db.sales.Find(stor_id, ord_num, title_id);
            db.sales.Remove(sales);
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
