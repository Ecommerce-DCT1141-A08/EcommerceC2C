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
                name:"Webmaster_default",
                url:"Webmaster/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "WebsiteThuongMaiDienTu..Areas.Webmaster.Controller" }
            );
        }
    }
}