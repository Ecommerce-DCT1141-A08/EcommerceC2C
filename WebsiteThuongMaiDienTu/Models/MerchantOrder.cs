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
    
    public partial class MerchantOrder
    {
        public int MerchantOrderID { get; set; }
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int MerchantID { get; set; }
        public int DeliveryID { get; set; }
        public int StartusID { get; set; }
        public Nullable<int> QuantityTotal { get; set; }
        public Nullable<decimal> ShippingTotal { get; set; }
        public Nullable<decimal> PriceTotal { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Delivery Delivery { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual OrderA OrderA { get; set; }
    }
}
