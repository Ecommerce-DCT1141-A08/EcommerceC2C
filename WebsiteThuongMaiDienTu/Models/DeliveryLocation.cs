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
    
    public partial class DeliveryLocation
    {
        public int DeliveryLocationID { get; set; }
        public int DeliveryID { get; set; }
        public int DistrictID { get; set; }
    
        public virtual Delivery Delivery { get; set; }
        public virtual District District { get; set; }
    }
}
