using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteThuongMaiDienTu.Startup))]
namespace WebsiteThuongMaiDienTu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        //public static string User_ID;
    }
}
