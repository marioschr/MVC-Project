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

        private const string Sql = "select t1.au_id, t1.au_lname, SUM(t1.price) as price from authors as t1 group by t1.id, t1.product";
        // GET: Search/Create
        public ActionResult Create()
        {
            /*string query = "SELECT authors.au_id, authors.au_lname, authors.au_fname, authors.phone, authors.address, authors.city, authors.state, authors.zip, authors.contract, SUM(sales.ord_num) AS sum_ord "
                + "FROM authors, sales, titleauthor "
                + "WHERE authors.au_id = titleauthor.au_id " +
                "AND titleauthor.title_id = sales.title_id"
                + "GROUP BY authors.au_id, authors.au_lname, authors.au_fname, authors.phone, authors.address, authors.city, authors.state, authors.zip, authors.contract";*/

            string query = "SELECT SUM([sales].[qty]) AS all_sales, [authors].[au_id], [authors].[au_fname], [authors].[au_lname], [authors].[phone], [authors].[address], " +
                "[authors].[city], [authors].[state], [authors].[zip], [authors].[contract] " +
                "FROM[dbo].[sales],[dbo].[authors],[dbo].[titleauthor] " +
                "WHERE[titleauthor].[au_id] = [authors].[au_id] AND[titleauthor].[title_id] = [sales].[title_id] " +
                "GROUP BY[authors].[au_id], [authors].[au_fname], [authors].[au_lname], [authors].[phone], [authors].[address], " +
                "[authors].[city], [authors].[state], [authors].[zip], [authors].[contract]";

            IEnumerable<SearchModelBOne> data = db.Database.SqlQuery<SearchModelBOne>(query);

            return View(data.ToList());
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
