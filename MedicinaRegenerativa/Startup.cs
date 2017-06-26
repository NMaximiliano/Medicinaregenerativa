using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedicinaRegenerativa.Startup))]
namespace MedicinaRegenerativa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
