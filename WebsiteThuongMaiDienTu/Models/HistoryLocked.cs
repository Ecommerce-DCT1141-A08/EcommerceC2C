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
    
    public partial class HistoryLocked
    {
        public int ID { get; set; }
        public Nullable<int> MerchantID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<System.DateTime> LockedDate { get; set; }
        public string Description { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Merchant Merchant { get; set; }
    }
}