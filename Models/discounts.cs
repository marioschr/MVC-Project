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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Globalization;
    using System.Web.Mvc;
    using PagedList;

    public partial class discounts {
        [Display(Name = "Discount Type")]
        [Required]
        [MaxLength(40)]
        public string discounttype { get; set; }

        [Display(Name = "Store")]
        [MaxLength(4)]
        public string stor_id { get; set; }

        [Display(Name = "Low Qty")]
        [Range(0,Int16.MaxValue)]
        public Nullable<short> lowqty { get; set; }

        [Display(Name = "High Qty")]
        [Range(0, Int16.MaxValue)]
        public Nullable<short> highqty { get; set; }

        [Display(Name = "Discount")]
        [Required]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal discount { get; set; }


        public int discount_id { get; set; }
    
        public virtual stores stores { get; set; }

        public IPagedList<discounts> discounts_pages { get; set; }

    }
}
