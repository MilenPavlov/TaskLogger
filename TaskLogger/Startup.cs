using System.Web.Http;
using Microsoft.Owin;
using Owin;
using TaskLogger.App_Start;

[assembly: OwinStartup(typeof(TaskLogger.Startup))]

namespace TaskLogger
{
    public partial class Startup
    {
        public async void Configuration(IAppBuilder app)
        {

            var config = new HttpConfiguration();
            ConfigureAuth(app);
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            ConfigureDI(config);
            await DbConfig.RegisterAdmin();

        }     
    }
}
