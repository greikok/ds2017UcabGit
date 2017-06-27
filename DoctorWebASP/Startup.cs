using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoctorWebASP.Startup))]
namespace DoctorWebASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
