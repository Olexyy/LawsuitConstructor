//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Category
    {
        public Category()
        {
            this.SubCategories = new HashSet<SubCategory>();
        }
    
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryWeight { get; set; }
    
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
