﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteThuongMaiDienTu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [CustomAuthorize(Roles = "Merchant")]
        public ActionResult Shop(string id)
        {
            ViewBag.Message = "Your shop page.";

            return RedirectToAction("Index", "Home", new { area = "Merchant", id = id });
        }
    }
}