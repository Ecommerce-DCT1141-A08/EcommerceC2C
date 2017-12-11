using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuongMaiDienTu.Areas.Merchant.Models;
using WebsiteThuongMaiDienTu.Models;

namespace WebsiteThuongMaiDienTu.Areas.Merchant.Controllers
{
    public class PayPalController : BaseController
    {
        EcommerceC2CA08 db = new EcommerceC2CA08();
        public ActionResult SuccessFromPaypal(string id, int coin)
        {
            SetCallout("Giao dịch thành công!", "success");
            var mp = db.MerchantPayments.Where(a => a.PaymentID.ToString() == id).FirstOrDefault();
            mp.StartusID = 1;// 1: giao dịch thành công
            db.Entry(mp).State = EntityState.Modified;

            //lưu vào activitymerchant
            ActivityMerchant am = new ActivityMerchant();
            am.MerchantID = mp.MerchantID;
            am.Object = "Order" + mp.PaymentID;
            am.Description = "Hoàn tất thanh toán đơn hàng mua Coin";
            am.ActivityDate = DateTime.Now;
            db.ActivityMerchants.Add(am);
            //cộng coin cho merchant
            var m = db.Merchants.Where(a => a.MerchantID == mp.MerchantID).FirstOrDefault();
            if (m.Coin == null || m.Coin == 0)
                m.Coin = coin;
            else m.Coin = m.Coin + coin;
            db.Entry(m).State = EntityState.Modified;
            
            db.SaveChanges();
            return RedirectToAction("Index2", "Coin");
        }

        public ActionResult CancelFromPaypal(string id)
        {
            SetCallout("Giao dịch đã được hủy!", "warning");
            var mp = db.MerchantPayments.Where(a => a.PaymentID.ToString() == id).FirstOrDefault();
            mp.StartusID = 2; // 2: giao dịch bị hủy
            //lưu vào activitymerchant
            ActivityMerchant am = new ActivityMerchant();
            am.MerchantID = mp.MerchantID;
            am.Object = "Order" + mp.PaymentID;
            am.Description = "Hủy đơn hàng mua Coin";
            am.ActivityDate = DateTime.Now;
            db.ActivityMerchants.Add(am);
            db.Entry(mp).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index2", "Coin");
        }

        public ActionResult NotifyFromPaypal()
        {
            SetCallout("Thông báo!", "success");
            return RedirectToAction("Index2", "Coin");
        }

        
    }
}