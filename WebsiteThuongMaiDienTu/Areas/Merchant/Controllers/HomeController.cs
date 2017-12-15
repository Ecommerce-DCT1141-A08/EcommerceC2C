using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteThuongMaiDienTu.Models;

namespace WebsiteThuongMaiDienTu.Areas.Merchant.Controllers
{
    public class HomeController : BaseController
    {
        EcommerceC2CA08Entities db = new EcommerceC2CA08Entities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //[CustomAuthorize(Roles = "Merchant")]
        // GET: Merchant/Home
        public ActionResult Index()
        {
            if (User.IsInRole("Merchant"))
            {
                string user_id = User.Identity.GetUserId();
                var merchant = db.AspNetUsers.Where(a => a.Id == user_id).FirstOrDefault();
                MvcApplication.merchant_id = db.Merchants.Where(a => a.UserID == user_id).Select(a => a.MerchantID).FirstOrDefault();
                if (merchant.EmailConfirmed == false)
                {
                    SetCallout("Tài khoản chưa được xác nhận. Vui lòng kiểm tra email để xác nhận tài khoản!", "warning");
                }
                return View();
            }
            else
                return RedirectToAction("Shop", "Home", new { area = "" });
        }

        //[CustomAuthorize(Roles = "Merchant")]
        // GET: Merchant/Home
        public ActionResult Shop()
        {
            return View("Index");
        }

        //[CustomAuthorize(Roles = "Merchant")]
        public ActionResult Logout()
        {
            if (User.IsInRole("Merchant"))
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
                return RedirectToAction("Shop", "Home", new { area = "" });
            //return RedirectToAction("LogOff", "Account", new { area = "" });
            
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}