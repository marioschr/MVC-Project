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
    using System.ComponentModel.DataAnnotations;

    public partial class authors
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public authors()
        {
            this.titleauthor = new HashSet<titleauthor>();
        }

        [Required(ErrorMessage = "Authors ID is Required. It cannot be empty")]
        [Display(Name ="Authors ID")]
        public string au_id { get; set; }

        [Display(Name = "Last name")]
        public string au_lname { get; set; }

        [Display(Name = "First name")]
        public string au_fname { get; set; }

        [Display(Name = "Phone Number")]
        public string phone { get; set; }

        [Display(Name = "Address")]
        public string address { get; set; }

        [Display(Name = "City")]
        public string city { get; set; }

        [Display(Name = "State")]
        public string state { get; set; }

        [Display(Name = "Zip Code")]
        public string zip { get; set; }

        [Display(Name = "Contract")]
        public bool contract { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<titleauthor> titleauthor { get; set; }
    }
}
