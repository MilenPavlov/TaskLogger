using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using TaskLogger.App_Start;

[assembly: OwinStartup(typeof(TaskLogger.Startup))]

namespace TaskLogger
{
    using TaskLogger.Data.Concrete;
    using TaskLogger.Data.Context;

    public partial class Startup
    {
        public async void Configuration(IAppBuilder app)
        {

            var config = new HttpConfiguration();
            ConfigureOAuthTokenGeneration(app);
            ConfigureAuth(app);
            WebApiConfig.Register(config);
            app.UseWebApi(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            ConfigureDI(config);
            await DbConfig.RegisterAdmin();

        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            app.CreatePerOwinContext(TaskLoggerContext.Create);
            app.CreatePerOwinContext<TaskLoggerUserManager>(TaskLoggerUserManager.Create);
        }
    }
}
