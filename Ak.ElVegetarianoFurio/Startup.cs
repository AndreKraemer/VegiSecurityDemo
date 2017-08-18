using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ak.ElVegetarianoFurio.Startup))]
namespace Ak.ElVegetarianoFurio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
