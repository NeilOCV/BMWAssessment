using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BMWAssessment.Startup))]
namespace BMWAssessment
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
