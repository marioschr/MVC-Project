//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class sales
    {
        public string stor_id { get; set; }
        public string ord_num { get; set; }
        public System.DateTime ord_date { get; set; }
        public short qty { get; set; }
        public string payterms { get; set; }
        public string title_id { get; set; }
    
        public virtual stores stores { get; set; }
        public virtual titles titles { get; set; }
    }
}
