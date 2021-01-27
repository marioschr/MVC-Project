using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class SearchController : Controller
    {

        private pubsEntities db = new pubsEntities();
        // GET: Search
        public ActionResult Index()
        {
            List<authors> authorsList = db.authors.ToList();
            List<titleauthor> titleauthorList = db.titleauthor.ToList();

            DateTime.TryParse(Request.QueryString["dateFrom"], out DateTime datefrom);
            DateTime.TryParse(Request.QueryString["dateTo"], out DateTime dateto);
            List<sales> salesList;// = db.sales.ToList();


            String dateTimeFrom = Request.QueryString["dateFrom"];
            Debug.Write("alex   --- dateTimeFrom: " + dateTimeFrom);
            Debug.Write("alex   --- dateTimeFrom: " + Request.QueryString["dateFrom"]);

            if (Request.QueryString["dateFrom"] != null && Request.QueryString["dateForm"] != "" && Request.QueryString["dateTo"] != null && Request.QueryString["dateTo"] != "")
            {
                salesList = db.sales.Where(m => m.ord_date >= datefrom && m.ord_date <= dateto).ToList();
            }
            else if (Request.QueryString["dateFrom"] != null && Request.QueryString["dateForm"] != "")
            {
                salesList = db.sales.Where(m => m.ord_date >= datefrom).ToList();
            }
            else if (Request.QueryString["dateTo"] != null && Request.QueryString["dateTo"] != "")
            {
                salesList = db.sales.Where(m => m.ord_date <= dateto).ToList();
            }
            else
            {
                salesList = db.sales.ToList();
            }




            ViewData["jointables"] = from sa in salesList
                                     join ta in titleauthorList on sa.title_id equals ta.title_id into table1
                                     from ta in table1.DefaultIfEmpty()
                                     join aut in authorsList on ta.au_id equals aut.au_id into table2
                                     from aut in table2.DefaultIfEmpty()
                                     select new SearchModelBOne { authorsList = aut, salesList = sa, titleauthorList = ta };


            /*ViewData["jointables"] = from aut in authorsList
                                     join ta in titleauthorList on aut.au_id equals ta.au_id into table1
                                     from ta in table1.DefaultIfEmpty()
                                     join sa in salesList on ta.title_id equals sa.title_id into table2
                                     from sa in table2.DefaultIfEmpty()
                                     select new SearchModelBOne { authorsList = aut, titleauthorList = ta, salesList = sa };


            string number_X = Request.QueryString["numberX"];
            if (number_X != null && number_X != "")
            {
                //list2 = db.authors.Where(m => m.au_lname.StartsWith(lastName));
                //list = db.sales.SqlQuery("SELECT")
            }*/


            return View(ViewData["jointables"]);
        }

        // GET: Search/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Search/TopAuthors
        public ActionResult TopAuthors()
        {

            string query = "SELECT ";

            String dateTimeFrom = Request.QueryString["dateFrom"];
            String dateTimeTo = Request.QueryString["dateTo"];
            String numberX = Request.QueryString["numberX"];

            if (numberX != null && numberX != "")
            {
                query += "TOP " + numberX + " ";
            }

            query += "SUM(sales.qty) AS all_sales, authors.au_id, authors.au_fname, authors.au_lname, authors.phone, authors.address, " +
                "authors.city, authors.state, authors.zip, authors.contract " +
                "FROM dbo.sales, dbo.authors, dbo.titleauthor " +
                "WHERE titleauthor.au_id = authors.au_id AND titleauthor.title_id = sales.title_id ";


            if (Request.QueryString["dateFrom"] != null && Request.QueryString["dateForm"] != "")
            {
                query += "AND sales.ord_date >= '" + dateTimeFrom + "' ";
            }

            if (Request.QueryString["dateTo"] != null && Request.QueryString["dateTo"] != "")
            {
                query += "AND sales.ord_date <= '" + dateTimeTo + "' ";
            }

            query +="GROUP BY authors.au_id, authors.au_fname, authors.au_lname, authors.phone, authors.address, " +
                "authors.city, authors.state, authors.zip, authors.contract " +
                "ORDER BY all_sales DESC";


            IEnumerable <TestClass1> myAreaList = db.Database.SqlQuery<TestClass1>(query);

            //return View(data);
            return View(myAreaList);
        }

        // POST: Search/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
