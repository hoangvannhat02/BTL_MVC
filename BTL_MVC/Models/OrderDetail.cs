//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BTL_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public Nullable<int> Quantily { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Acount { get; set; }
    
        public virtual Orders Orders { get; set; }
        public virtual Products Products { get; set; }
    }
}
