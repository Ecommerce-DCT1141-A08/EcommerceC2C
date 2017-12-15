using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using WebsiteThuongMaiDienTu.Models;
using Microsoft.AspNet.Identity;
using WebsiteThuongMaiDienTu.Areas.Merchant.Models;
using System.Data.Entity;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Net;

namespace WebsiteThuongMaiDienTu.Areas.Merchant.Controllers
{
    public class PosterController : BaseController
    {
        EcommerceC2CA08Entities db = new EcommerceC2CA08Entities();
        // GET: Merchant/Poster
        public ActionResult Index()
        {
            return View();
        }
        //GET
        [CustomAuthorize(Roles = "Merchant")]
        public ActionResult CreatePoster()
        {
            //if (User.IsInRole("Merchant"))
            //{
            ViewBag.Category = new SelectList(db.Categories, "CategoryID", "Name");
            return View();
            //}
            //else
                //return RedirectToAction("Shop", "Home", new { area = "" });
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CustomAuthorize(Roles = "Merchant")]
        public ActionResult CreatePoster(UploadPoster P)
        {
            if (User.IsInRole("Merchant"))
            {
                if (ModelState.IsValid)
                {
                    string user_id = User.Identity.GetUserId();
                    var merchant = db.Merchants.FirstOrDefault(a => a.UserID == user_id);
                    int usecoin = 0;
                    if (P.Type == false)
                        usecoin = Convert.ToInt32(ConfigurationManager.AppSettings["posterGeneral"]);
                    else
                        usecoin = Convert.ToInt32(ConfigurationManager.AppSettings["posterVip"]);
                    if (merchant.Coin == null || merchant.Coin < usecoin)
                    {
                        SetCallout("Tài khoản Coin không đủ để đăng tin!", "warning");
                        ViewBag.Category = new SelectList(db.Categories, "CategoryID", "Name");
                        return View(P);
                    }
                    
                    Price price = new Price();//lưu price
                    price.SellingPrice = P.SellingPrice;
                    price.Created = DateTime.Now;
                    db.Prices.Add(price);//---lưu price
                    db.SaveChanges();

                    Poster poster = new Poster();//lưu poster
                    poster.CategoryID = P.CategoryID; poster.MerchantID = merchant.MerchantID;
                    poster.StatusID = P.Quantity <= 0 ? false : true; poster.Type = P.Type;
                    poster.Created = DateTime.Now; poster.LastChanged = DateTime.Now;
                    poster.Keyword = P.Keyword; poster.Description = P.Description;
                    poster.Quantity = P.Quantity; poster.PriceID = price.PriceID;
                    db.Posters.Add(poster);//---lưu poster
                    db.SaveChanges();
                    var pri = db.Prices.Where(p => p.PriceID == price.PriceID).FirstOrDefault();
                    pri.PosterID = poster.PosterID;
                    db.Entry(pri).State = EntityState.Modified;

                    List<WebsiteThuongMaiDienTu.Models.Image> images = new List<WebsiteThuongMaiDienTu.Models.Image>();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];

                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            WebsiteThuongMaiDienTu.Models.Image image = new WebsiteThuongMaiDienTu.Models.Image()//lưu tên tệp ảnh vào bảng
                            {
                                Filename = fileName,
                                Extension = Path.GetExtension(fileName),
                                PosterID = poster.PosterID
                            };
                            db.Images.Add(image);//---lưu tên tệp ảnh vào bảng
                            images.Add(image);

                            var path = Path.Combine(Server.MapPath("~/images/Products/"), image.Filename);
                            file.SaveAs(path);
                        }
                    }

                    if (P.CategoryID == 1)//lưu thông tin cấu hình
                    {
                        PhoneConfig phone = P.PhoneConfig;
                        phone.PosterID = poster.PosterID;
                        phone.CategoryID = P.CategoryID;
                        db.PhoneConfigs.Add(phone);
                    }
                    else if(P.CategoryID == 2)
                    {
                        LaptopConfig laptop = P.LaptopConfig;
                        laptop.PosterID = poster.PosterID;
                        laptop.CategoryID = P.CategoryID;
                        db.LaptopConfigs.Add(laptop);
                    }
                    else if (P.CategoryID == 3)
                    {
                        Accessory access = P.Accessory;
                        access.PosterID = poster.PosterID;
                        access.CategoryID = P.CategoryID;
                        db.Accessories.Add(access);
                    }
                    //trừ Coin trong table Merchant
                    merchant.Coin = merchant.Coin - usecoin;
                    db.Entry(merchant).State = EntityState.Modified;
                    //lưu vào historyusecoin
                    HistoryUseCoin huc = new HistoryUseCoin();
                    huc.MerchantID = merchant.MerchantID;
                    huc.PosterID = poster.PosterID;
                    huc.Description = "Đăng tin";
                    huc.Coin = usecoin;
                    huc.Created = poster.Created;
                    db.HistoryUseCoins.Add(huc);

                    db.SaveChanges();
                    return RedirectToAction("ListPoster", "Poster");
                }
                return View(P);
                
            }
            else
            return RedirectToAction("Shop", "Home", new { area = "" });
        }

        //GET
        public ActionResult ListPoster()
        {
            if (User.IsInRole("Merchant"))
            {
                string user_id = User.Identity.GetUserId();
                var merchant = db.Merchants.FirstOrDefault(a => a.UserID == user_id);
                var listposter = db.Posters.Include(p => p.Category).Include(p => p.Price)
                    .Include(p => p.Discount).Include(p => p.Images).Where(p => p.MerchantID == merchant.MerchantID);
                return View(listposter.ToList());
            }
            else
                return RedirectToAction("Shop", "Home", new { area = "" });
        }

        //GET
        public ActionResult EditPoster(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poster poster = db.Posters.Find(id);
            if (poster == null)
            {
                return HttpNotFound();
            }
            return PartialView(new EditPoster { PosterID = poster.PosterID, Keyword = poster.Keyword,
                Quantity = poster.Quantity, SellingPrice = String.Format("{0:0}", poster.Price.SellingPrice) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPoster(EditPoster poster)
        {
            if (ModelState.IsValid)
            {
                string user_id = User.Identity.GetUserId();
                var merchant = db.Merchants.FirstOrDefault(a => a.UserID == user_id);
                var p = db.Posters.Find(poster.PosterID);
                if (db.Posters.Where(a => a.PosterID == poster.PosterID 
                && a.Price.SellingPrice.ToString() == poster.SellingPrice).ToList().Count() == 0)
                {
                    //thêm giá mới
                    Price price = new Price();//lưu price
                    price.PosterID = poster.PosterID;
                    price.SellingPrice = Convert.ToDecimal(poster.SellingPrice);
                    price.Created = DateTime.Now;
                    db.Prices.Add(price);//---lưu price
                    db.SaveChanges();
                    p.PriceID = price.PriceID;
                }
                p.Quantity = poster.Quantity;
                p.LastChanged = DateTime.Now;
                db.Entry(p).State = EntityState.Modified;

                //trừ Coin trong table Merchant
                merchant.Coin = merchant.Coin - Convert.ToInt32(ConfigurationManager.AppSettings["posterEdit"]);
                db.Entry(merchant).State = EntityState.Modified;
                //lưu vào historyusecoin
                HistoryUseCoin huc = new HistoryUseCoin();
                huc.MerchantID = merchant.MerchantID;
                huc.PosterID = poster.PosterID;
                huc.Description = "Sửa tin";
                huc.Coin = Convert.ToInt32(ConfigurationManager.AppSettings["posterEdit"]);
                huc.Created = p.LastChanged;
                db.HistoryUseCoins.Add(huc);

                db.SaveChanges();
                SetCallout("Tin đăng đã được cập nhật!", "success");
                return RedirectToAction("ListPoster");
            }
            return new EmptyResult();
        }
    }
}