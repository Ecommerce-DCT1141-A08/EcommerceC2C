using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteThuongMaiDienTu.Areas.Webmaster.Controllers
{
    public class BaseController : Controller
    {
        protected void SetCallout(string message, string type)
        {
            TempData["CalloutMessage"] = message;
            if (type == "success")
                TempData["CalloutType"] = "callout-success";
            else if (type == "warning")
                TempData["CalloutType"] = "callout-warning";
            else if (type == "info")
                TempData["CalloutType"] = "callout-info";
            else if (type == "danger")
                TempData["CalloutType"] = "callout-danger";
        }
    }
}