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
    
    public partial class HistoryUseCoin
    {
        public int ID { get; set; }
        public Nullable<int> PaymentID { get; set; }
        public Nullable<int> PosterID { get; set; }
        public string Description { get; set; }
        public Nullable<int> Coin { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
    
        public virtual MerchantPayment MerchantPayment { get; set; }
        public virtual Poster Poster { get; set; }
    }
}