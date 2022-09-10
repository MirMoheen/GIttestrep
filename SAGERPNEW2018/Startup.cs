using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SAGERPNEW2018.Startup))]
namespace SAGERPNEW2018
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
