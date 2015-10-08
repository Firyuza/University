using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebUniversity.Startup))]
namespace WebUniversity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
