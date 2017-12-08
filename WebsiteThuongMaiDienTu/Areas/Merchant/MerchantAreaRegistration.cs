using System.Web.Mvc;

namespace WebsiteThuongMaiDienTu.Areas.Merchant
{
    public class MerchantAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Merchant";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Merchant_default",
                url: "Merchant/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "WebsiteThuongMaiDienTu.Areas.Merchant.Controllers" }
            );
        }
    }
}