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
    
    public partial class WebResource
    {
        public WebResource()
        {
            this.Lawsuits = new HashSet<Lawsuit>();
        }
    
        public int WebResourceId { get; set; }
        public string WebResourceTitle { get; set; }
        public string WebResourceBody { get; set; }
        public string WebResourceKey { get; set; }
    
        public virtual ICollection<Lawsuit> Lawsuits { get; set; }
    }
}
