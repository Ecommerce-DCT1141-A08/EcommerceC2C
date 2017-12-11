using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteThuongMaiDienTu.Areas.Merchant.Models
{
    public class ProfileViewModel
    {
        public int MerchantID { get; set; }
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        //public string ShopName { get; set; }
        public string Address { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public Nullable<int> WardID { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string Phone { get; set; }
        //public Nullable<int> Coin { get; set; }
        public Nullable<decimal> RatingScore { get; set; }
        public Nullable<int> RatingQuantity { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
    }

    public class ChangeProfile
    {
        public string Address { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public Nullable<int> WardID { get; set; }
        public string Phone { get; set; }
    }
}