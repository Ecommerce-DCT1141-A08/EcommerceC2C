using System.Web.Mvc;

namespace WebsiteThuongMaiDienTu.Areas.Webmaster
{
    public class WebmasterAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Webmaster";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Webmaster_default",
                "Webmaster/{controller}/{action}/{id}",
                new { controller = "Home", action = "Login", id = UrlParameter.Optional },
                new string[] { "WebsiteThuongMaiDienTu.Areas.Webmaster.Controllers" }//nhập chính xác lỗi do sai string[]
            );
        }
    }
}