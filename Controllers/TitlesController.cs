using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MVC_Project.Models;
using PagedList;

namespace MVC_Project.Controllers
{
    public class TitlesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Titles
        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.titleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.typeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.priceSortParm = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.advanceSortParm = sortOrder == "advance" ? "advance_desc" : "advance";
            ViewBag.royaltySortParm = sortOrder == "royalty" ? "royalty_desc" : "royalty";
            ViewBag.ytd_salesSortParm = sortOrder == "ytd_sales" ? "ytd_sales_desc" : "ytd_sales";
            ViewBag.pubdateSortParm = sortOrder == "pubdate" ? "pubdate_desc" : "pubdate";
            ViewBag.pub_nameSortParm = sortOrder == "pub_name" ? "pub_name_desc" : "pub_name";

            var titles = db.titles.Include(t => t.publishers).Include(t => t.roysched);
            switch (sortOrder) {// title,type,price,advance,royalty,ytd_sales,pubdate,pub_name
                case "title_desc":
                    titles = titles.OrderByDescending(s => s.title);
                    break;
                case "type":
                    titles = titles.OrderBy(s => s.type);
                    break;
                case "type_desc":
                    titles = titles.OrderByDescending(s => s.type);
                    break;
                case "price":
                    titles = titles.OrderBy(s => s.price);
                    break;
                case "price_desc":
                    titles = titles.OrderByDescending(s => s.price);
                    break;
                case "advance":
                    titles = titles.OrderBy(s => s.advance);
                    break;
                case "advance_desc":
                    titles = titles.OrderByDescending(s => s.advance);
                    break;
                case "royalty":
                    titles = titles.OrderBy(s => s.royalty);
                    break;
                case "royalty_desc":
                    titles = titles.OrderByDescending(s => s.royalty);
                    break;
                case "ytd_sales":
                    titles = titles.OrderBy(s => s.ytd_sales);
                    break;
                case "ytd_sales_desc":
                    titles = titles.OrderByDescending(s => s.ytd_sales);
                    break;
                case "pubdate":
                    titles = titles.OrderBy(s => s.pubdate);
                    break;
                case "pubdate_desc":
                    titles = titles.OrderByDescending(s => s.pubdate);
                    break;
                case "pub_name":
                    titles = titles.OrderBy(s => s.publishers.pub_name);
                    break;
                case "pub_name_desc":
                    titles = titles.OrderByDescending(s => s.publishers.pub_name);
                    break;
                default:
                    titles = titles.OrderBy(s => s.title);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(titles.ToPagedList(pageNumber, pageSize));
        }

        // GET: Titles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            Console.WriteLine("alex   --- id: " + id);
            titles titles = db.titles.Find(id);
            if (titles == null)
            {
                return View("NotFound");
            }
            return View(titles);
        }

        // GET: Titles/Create
        public ActionResult Create()
        {
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name");
            ViewBag.title_id = new SelectList(db.roysched, "title_id", "title_id");
            return View();
        }

        // POST: Titles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title_id,title,type,pub_id,price,advance,royalty,ytd_sales,notes,pubdate")] titles titles)
        {
            if (ModelState.IsValid)
            {
                db.titles.Add(titles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", titles.pub_id);
            ViewBag.title_id = new SelectList(db.roysched, "title_id", "title_id", titles.title_id);
            return View(titles);
        }

        // GET: Titles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            titles titles = db.titles.Find(id);
            if (titles == null)
            {
                return View("NotFound");
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", titles.pub_id);
            ViewBag.title_id = new SelectList(db.roysched, "title_id", "title_id", titles.title_id);
            return View(titles);
        }

        // POST: Titles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "title_id,title,type,pub_id,price,advance,royalty,ytd_sales,notes,pubdate")] titles titles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(titles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", titles.pub_id);
            ViewBag.title_id = new SelectList(db.roysched, "title_id", "title_id", titles.title_id);
            return View(titles);
        }

        // GET: Titles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            titles titles = db.titles.Find(id);
            if (titles == null)
            {
                return View("NotFound");
            }
            return View(titles);
        }

        // POST: Titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            titles titles = db.titles.Find(id);
            db.titles.Remove(titles);
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
