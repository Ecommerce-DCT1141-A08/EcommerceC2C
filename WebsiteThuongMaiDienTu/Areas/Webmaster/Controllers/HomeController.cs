using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebsiteThuongMaiDienTu.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace WebsiteThuongMaiDienTu.Areas.Webmaster.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        EcommerceC2CA08Entities db = new EcommerceC2CA08Entities();
        // GET: Webmaster/Home
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Login(WebsiteThuongMaiDienTu.Models.Webmaster web)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Webmasters.Where(a => a.Username.Equals(web.Username) && a.Password.Equals(web.Password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["WebmasterID"] = obj.WebmasterID.ToString();
                    Session["WebmasterName"] = obj.Fullname.ToString();
                    Session["WebmasterUser"] = obj.Username.ToString();
                    //lưu thông tin LastLogin
                    obj.LastLogin = DateTime.Now;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Đăng nhập không thành công");
            }
            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session["WebmasterID"] = null;
            Session["WebmasterName"] = null;
            Session["WebmasterUser"] = null;
            return RedirectToAction("Login", "Home");
        }
    }
}