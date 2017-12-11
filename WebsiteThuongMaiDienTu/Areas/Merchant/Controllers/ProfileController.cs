using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThuongMaiDienTu.Models;
using Microsoft.AspNet.Identity;
using WebsiteThuongMaiDienTu.Areas.Merchant.Models;
using System.Data.Entity;

namespace WebsiteThuongMaiDienTu.Areas.Merchant.Controllers
{
    public class ProfileController : BaseController
    {
        EcommerceC2CA08 db = new EcommerceC2CA08();
        // GET: Merchant/Profile
        public ActionResult Index()
        {
            string user_id = User.Identity.GetUserId();
            var merchant = db.Merchants.Include(a => a.Province).Include(a => a.District).Include(a => a.Ward)
                .FirstOrDefault(a => a.UserID == user_id);

            ProfileViewModel p = new ProfileViewModel();
            p.MerchantID = merchant.MerchantID;
            p.UserID = user_id;
            p.Username = User.Identity.GetUserName();
            p.Email = db.AspNetUsers.Where(a => a.Id == user_id).FirstOrDefault().Email;
            p.Address = merchant.Address ?? "";
            p.ProvinceID = merchant.Province == null ? -1 : merchant.Province.Id;
            p.DistrictID = merchant.District == null ? -1 : merchant.District.Id;
            p.WardID = merchant.Ward == null ? -1 : merchant.Ward.Id;
            p.Province = merchant.Province == null ? null : merchant.Province.Type + " " + merchant.Province.Name;
            p.District = merchant.District == null ? null : merchant.District.Type + " " + merchant.District.Name;
            p.Ward = merchant.Ward == null ? null : merchant.Ward.Type +" " + merchant.Ward.Name;
            p.Phone = merchant.Phone;
            p.RatingScore = merchant.RatingScore ?? 0;
            p.RatingQuantity = merchant.RatingQuantity ?? 0;
            p.Created = merchant.Created;
            
            ViewBag.Province = new SelectList(db.Provinces, "Id", "Name");
            ViewBag.District = null; ViewBag.Ward = null;
            if (p.ProvinceID != null)
                ViewBag.District = new SelectList(db.Districts.Where(a => a.ProvinceId == p.ProvinceID), "Id", "Name");
            if (p.DistrictID != null)
                ViewBag.Ward = new SelectList(db.Wards.Where(a => a.DistrictID == p.DistrictID), "Id", "Name");

            return View(p);
        }

        public JsonResult GetDistrict(int provinceid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Districts.Where(a => a.ProvinceId == provinceid).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWard(int districtid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Wards.Where(a => a.DistrictID == districtid).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeProfile(string Phone, string Address, int? Province, int? District, int? Ward)
        {
            if (Province.ToString() == "" || Province == -1) {
                Province = null;
            }
            if (District.ToString() == "" || District == -1)
            {
                District = null;
            }
            if (Ward.ToString() == "" || Ward == -1)
            {
                Ward = null;
            }
            string user_id = User.Identity.GetUserId();
            var merchant = db.Merchants.Where(a => a.UserID == user_id).FirstOrDefault();
            merchant.Address = Address;
            merchant.Phone = Phone;
            merchant.ProvinceID = Province;
            merchant.DistrictID = District;
            merchant.WardID = Ward;
            db.Entry(merchant).State = EntityState.Modified;
            db.SaveChanges();
            SetCallout("Chỉnh sửa thông tin tài khoản thành công!", "success");
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public JsonResult Change(string Phone, string Address, int? ProvinceID, int? DistrictID, int? WardID)
        //{
        //    bool status = false;
        //    if (ModelState.IsValid)
        //    {
        //        string user_id = User.Identity.GetUserId();
        //        
        //        db.Entry(x).State = EntityState.Modified;
        //        db.SaveChanges();
        //        status = true;
        //        SetCallout("Chỉnh sửa thông tin tài khoản thành công!", "success");
        //    }
        //    return new JsonResult { Data = new { status = status } };
        //}
    }
}