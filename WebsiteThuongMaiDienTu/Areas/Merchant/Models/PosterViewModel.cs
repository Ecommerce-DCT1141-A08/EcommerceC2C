using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebsiteThuongMaiDienTu.Models;

namespace WebsiteThuongMaiDienTu.Areas.Merchant.Models
{
    public class UploadPoster
    {
        public int MerchantID { get; set; }
        public int CategoryID { get; set; }
        public bool StatusID { get; set; }
        public bool Type { get; set; }
        [Required(ErrorMessage = "Please enter your title !")]
        public string Keyword { get; set; }
        [Required(ErrorMessage = "Please enter product description !")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter product quantity !")]
        public Nullable<int> Quantity { get; set; }
        [Required(ErrorMessage = "Please enter product price !")]
        public Nullable<decimal> SellingPrice { get; set; }
        public virtual LaptopConfig LaptopConfig { get; set; }
        public virtual PhoneConfig PhoneConfig { get; set; }
        public virtual Accessory Accessory { get; set; }
    }

    public class EditPoster
    {
        public int PosterID { get; set; }
        public string Keyword { get; set; }
        [Required(ErrorMessage = "Please enter product quantity !")]
        public Nullable<int> Quantity { get; set; }
        [Required(ErrorMessage = "Please enter product price !")]
        public string SellingPrice { get; set; }
    }

    
}