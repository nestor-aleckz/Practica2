using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Practica2AyD1.Startup))]
namespace Practica2AyD1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
