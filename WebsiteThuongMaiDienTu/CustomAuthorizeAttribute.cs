using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteThuongMaiDienTu
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string ViewName { get; set; }

        public CustomAuthorizeAttribute()
        {
            ViewName = "AuthorizeFailed";
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            IsUserAuthorized(filterContext);
        }

        void IsUserAuthorized(AuthorizationContext filterContext)
        {
            //nếu người dùng có quyền
            if (filterContext.Result == null)
                return;
            if(filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                ViewDataDictionary dict = new ViewDataDictionary();
                dict.Add("Message", "Tài khoản của bạn chỉ dùng để mua hàng. Bạn không có quyền truy cập trang này!");
                var result = new ViewResult() { ViewName = this.ViewName, ViewData = dict };
                filterContext.Result = result;
            }
        }
    }
}