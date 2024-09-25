//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TechFixWebApi
{
    using System;
    using System.Collections.Generic;
    
    public partial class Quotation
    {
        public int QuotationID { get; set; }
        public Nullable<int> RequestQuotationID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<double> TotalAmount { get; set; }
        public Nullable<System.DateTime> QuotationDate { get; set; }
        public Nullable<int> SupplierID { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual RequestQuotation RequestQuotation { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
