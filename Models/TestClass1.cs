using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Project.Models
{
    public class TestClass1
    {
        public string au_id { get; set; }
        public string au_lname { get; set; }
        public string au_fname { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public bool contract { get; set; }

        public string stor_id { get; set; }
        public string ord_num { get; set; }
        public System.DateTime ord_date { get; set; }
        public short qty { get; set; }
        public string payterms { get; set; }
        public string title_id { get; set; }

        public virtual stores stores { get; set; }

        public virtual titles titles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<titleauthor> titleauthor { get; set; }
    }
}