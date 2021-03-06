﻿using Microsoft.AspNet.Identity;
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
        EcommerceC2CA08Entities db = new EcommerceC2CA08Entities();
        public ActionResult SuccessFromPaypal(string id, int coin)
        {
            if (User.IsInRole("Merchant"))
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
                //lưu vào historyusecoin
                HistoryUseCoin huc = new HistoryUseCoin();
                huc.MerchantID = mp.MerchantID;
                huc.PaymentID = mp.PaymentID;
                huc.Description = "Đã mua";
                huc.Coin = coin;
                huc.Created = am.ActivityDate;
                db.HistoryUseCoins.Add(huc);
                //cộng coin cho merchant
                var m = db.Merchants.Where(a => a.MerchantID == mp.MerchantID).FirstOrDefault();
                if (m.Coin == null || m.Coin == 0)
                    m.Coin = coin;
                else m.Coin = m.Coin + coin;
                db.Entry(m).State = EntityState.Modified;

            
                db.SaveChanges();
                return RedirectToAction("Index2", "Coin");
            }
            else
                return RedirectToAction("Shop", "Home", new { area = "" });
            
        }
        public ActionResult CancelFromPaypal(string id)
        {
            if (User.IsInRole("Merchant"))
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
            else
                return RedirectToAction("Shop", "Home", new { area = "" });
            
        }

        public ActionResult NotifyFromPaypal()
        {
            if (User.IsInRole("Merchant"))
            {
                SetCallout("Thông báo!", "success");
                return RedirectToAction("Index2", "Coin");
            }
            else
                return RedirectToAction("Shop", "Home", new { area = "" });
            
        }

        
    }
}