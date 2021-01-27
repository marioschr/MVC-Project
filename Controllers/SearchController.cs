using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class SearchController : Controller
    {

        private pubsEntities db = new pubsEntities();

        // GET: Search/OrdersReport
        public ActionResult OrdersReport()
        {
            string query = "SELECT ord_num, stor_name, title FROM dbo.stores,dbo.titles,dbo.sales WHERE sales.stor_id = stores.stor_id AND sales.title_id = titles.title_id ";

            DateTime.TryParse(Request.QueryString["dateFrom"], out DateTime datefrom);
            DateTime.TryParse(Request.QueryString["dateTo"], out DateTime dateto);


            String dateTimeFrom = Request.QueryString["dateFrom"];
            String dateTimeTo = Request.QueryString["dateTo"];
            String storeName = Request.QueryString["storeName"];

            if (Request.QueryString["dateFrom"] != null && Request.QueryString["dateForm"] != "") {
                query += "AND sales.ord_date >= '" + dateTimeFrom + "' ";
            }

            if (Request.QueryString["dateTo"] != null && Request.QueryString["dateTo"] != "") {
                query += "AND sales.ord_date <= '" + dateTimeTo + "' ";
            }

            if (Request.QueryString["storeName"] != null && Request.QueryString["storeName"] != "") {
                query += "AND stores.stor_name LIKE '%" + storeName + "%' ";
            }

            IEnumerable<OrdersReportModel> myAreaList = db.Database.SqlQuery<OrdersReportModel>(query);

            return View(myAreaList);
        }

        // GET: Search/TopAuthors
        public ActionResult TopAuthors()
        {
            string query = "SELECT ";

            String dateTimeFrom = Request.QueryString["dateFrom"];
            String dateTimeTo = Request.QueryString["dateTo"];
            String numberX = Request.QueryString["numberX"];

            if (!String.IsNullOrEmpty(numberX))
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


            IEnumerable <TopAuthorsModel> myAreaList = db.Database.SqlQuery<TopAuthorsModel>(query);
            
            return View(myAreaList);
        }
    }
}
