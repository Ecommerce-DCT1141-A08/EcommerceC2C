using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuongMaiDienTu.Areas.Merchant.Models;
using WebsiteThuongMaiDienTu.Models;
using System.Data.Entity;

namespace WebsiteThuongMaiDienTu.Areas.Merchant.Controllers
{
    public class CoinController : BaseController
    {
        EcommerceC2CA08Entities db = new EcommerceC2CA08Entities();
        // GET: Merchant/Coin
        public ActionResult Index()
        {
            if (User.IsInRole("Merchant"))
            {
                string user_id = User.Identity.GetUserId();
                var merchant = db.Merchants.FirstOrDefault(a => a.UserID == user_id);

                CoinPocket c = new CoinPocket();
                c.MerchantID = merchant.MerchantID;
                c.UserID = user_id;
                c.Username = User.Identity.GetUserName();
                c.Email = db.AspNetUsers.Where(a => a.Id == user_id).FirstOrDefault().Email;
                c.Coin = merchant.Coin ?? 0;
                c.RatingScore = merchant.RatingScore ?? 0;
                c.RatingQuantity = merchant.RatingQuantity ?? 0;

                ViewBag.Coin = new SelectList(db.CoinPacks, "Coin", "Price");

                return View(c);
            }
            else
                return RedirectToAction("Shop", "Home", new { area = "" });
        }

        public ActionResult Index2()
        {
            if (User.IsInRole("Merchant"))
            {
                string user_id = User.Identity.GetUserId();
                var merchant = db.Merchants.FirstOrDefault(a => a.UserID == user_id);

                CoinPocket c = new CoinPocket();
                c.MerchantID = merchant.MerchantID;
                c.UserID = user_id;
                c.Username = User.Identity.GetUserName();
                c.Email = db.AspNetUsers.Where(a => a.Id == user_id).FirstOrDefault().Email;
                c.Coin = merchant.Coin ?? 0;
                c.RatingScore = merchant.RatingScore ?? 0;
                c.RatingQuantity = merchant.RatingQuantity ?? 0;

                ViewBag.Coin = new SelectList(db.CoinPacks, "Coin", "Price");

                return View(c);
            }
            else
                return RedirectToAction("Shop", "Home", new { area = "" });
            
        }
        public ActionResult ValidateCommand(string pack, string quantity, string coin, string cointotal, string totalPrice)
        {
            if (User.IsInRole("Merchant"))
            {
                bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSandbox"]);
                PayPalModel paypal = new PayPalModel(useSandbox);

                paypal.item_name = "CoinPack " + pack;
                paypal.coin = coin;
                paypal.cointotal = cointotal;
                paypal.price = pack;
                paypal.quantity = quantity;
                paypal.amount = totalPrice;

                string user_id = User.Identity.GetUserId();
                paypal.merchant = db.Merchants.Include(a => a.AspNetUser).Where(a => a.UserID == user_id).FirstOrDefault();

                //lưu data
                MerchantPayment mp = new MerchantPayment();
                mp.MerchantID = paypal.merchant.MerchantID;
                mp.QuantityPack = Convert.ToInt32(quantity);
                mp.PriceTotal = Convert.ToDecimal(totalPrice);
                mp.Created = DateTime.Now;
                mp.StartusID = 0;//0: chưa thanh toán
                mp.CoinPackID = db.CoinPacks.Where(a => a.Coin.ToString() == coin).Select(a => a.CoinPackID).FirstOrDefault();
                db.MerchantPayments.Add(mp);
                db.SaveChanges();


                //lưu vào activitymerchant
                ActivityMerchant am = new ActivityMerchant();
                am.MerchantID = mp.MerchantID;
                am.Object = "Order" + mp.PaymentID;
                am.Description = "Tạo đơn hàng mua Coin";
                am.ActivityDate = mp.Created;
                db.ActivityMerchants.Add(am);            

                paypal.paymentID = mp.PaymentID.ToString();
                paypal.date = mp.Created.ToString();
                db.SaveChanges();

                return View(paypal);
            }
            else
                return RedirectToAction("Shop", "Home", new { area = "" });
            
        }

        public ActionResult CompletePayment()
        {
            if (User.IsInRole("Merchant"))
            {
                SetCallout("Giao dịch thành công!", "success");
                return RedirectToAction("Index2");
            }
            else
                return RedirectToAction("Shop", "Home", new { area = "" });
            
        }
    }
}