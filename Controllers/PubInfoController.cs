using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Project.Models;
using PagedList;

namespace MVC_Project.Controllers
{
    public class PubInfoController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: PubInfo
        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.pub_nameSortParm = String.IsNullOrEmpty(sortOrder) ? "pub_name" : "";

            var pub_info = db.pub_info.Include(p => p.publishers);
            switch (sortOrder) {// job_desc,min_lvl,max_lvl
                case "pub_name_desc":
                    pub_info = pub_info.OrderByDescending(s => s.publishers.pub_name);
                    break;
                default:
                    pub_info = pub_info.OrderBy(s => s.publishers.pub_name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(pub_info.ToPagedList(pageNumber, pageSize));
        }

        // GET: PubInfo/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            pub_info pub_info = db.pub_info.Find(id);
            if (pub_info == null)
            {
                return View("NotFound");
            }
            return View(pub_info);
        }

        // GET: PubInfo/Create
        public ActionResult Create()
        {
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name");
            return View();
        }

        // POST: PubInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(pub_info pub_info)
        {
            if (ModelState.IsValid)
            {
                byte[] bytes;
                HttpPostedFileBase data = pub_info.file;
                using (BinaryReader br = new BinaryReader(pub_info.file.InputStream)) {
                    bytes = br.ReadBytes(pub_info.file.ContentLength);
                }

                db.pub_info.Add(new pub_info {
                    pub_id = pub_info.pub_id,
                    logo = bytes,
                    pr_info = pub_info.pr_info
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", pub_info.pub_id);
            return View(pub_info);
        }

        // GET: PubInfo/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            pub_info pub_info = db.pub_info.Find(id);
            if (pub_info == null)
            {
                return View("NotFound");
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", pub_info.pub_id);
            return View(pub_info);
        }

        // POST: PubInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pub_id,logo,pr_info")] pub_info pub_info)
        {
            if (ModelState.IsValid)
            {
                byte[] bytes;
                HttpPostedFileBase data = pub_info.file;
                using (BinaryReader br = new BinaryReader(pub_info.file.InputStream)) {
                    bytes = br.ReadBytes(pub_info.file.ContentLength);
                }

                db.Entry(new pub_info {
                    pub_id = pub_info.pub_id,
                    logo = bytes,
                    pr_info = pub_info.pr_info
                }).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", pub_info.pub_id);
            return View(pub_info);
        }

        // GET: PubInfo/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            pub_info pub_info = db.pub_info.Find(id);
            if (pub_info == null)
            {
                return View("NotFound");
            }
            return View(pub_info);
        }

        // POST: PubInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            pub_info pub_info = db.pub_info.Find(id);
            db.pub_info.Remove(pub_info);
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
