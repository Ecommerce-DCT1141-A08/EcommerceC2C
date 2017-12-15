//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteThuongMaiDienTu.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int OrderID { get; set; }
        public int PosterID { get; set; }
        public int PriceID { get; set; }
        public int MerchantID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Total { get; set; }
    
        public virtual Merchant Merchant { get; set; }
        public virtual OrderA OrderA { get; set; }
        public virtual Poster Poster { get; set; }
        public virtual Price Price1 { get; set; }
    }
}
