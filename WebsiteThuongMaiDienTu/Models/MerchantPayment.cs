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
    
    public partial class MerchantPayment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MerchantPayment()
        {
            this.HistoryUseCoins = new HashSet<HistoryUseCoin>();
        }
    
        public int PaymentID { get; set; }
        public string PaypalTransactionID { get; set; }
        public int MerchantID { get; set; }
        public Nullable<int> CoinPackID { get; set; }
        public int QuantityPack { get; set; }
        public decimal PriceTotal { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public int StartusID { get; set; }
    
        public virtual CoinPack CoinPack { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoryUseCoin> HistoryUseCoins { get; set; }
        public virtual Merchant Merchant { get; set; }
    }
}
