using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Project.Models {
    public class OrdersReportModel {
        [Display(Name = "Store Name")]
        public string stor_name { get; set; }

        [Display(Name = "Order Number")]
        public string ord_num { get; set; }

        [Display(Name = "Title")]
        public string title { get; set; }
    }
}