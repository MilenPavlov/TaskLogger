using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;
using System.Web.Http;
using Swashbuckle.Application;
using TaskLogger.App_Start;

[assembly: OwinStartup(typeof(TaskLogger.Startup))]

namespace TaskLogger
{
    using TaskLogger.Data.Concrete;
    using TaskLogger.Data.Context;
    using TaskLogger.OwinData;

    public partial class Startup
    {
        public async void Configuration(IAppBuilder app)
        {

            var config = new HttpConfiguration();
            ConfigureOAuthTokenGeneration(app);
            ConfigureOAuthTokenConsumption(app);
            ConfigureAuth(app);
            WebApiConfig.Register(config);
            app.UseWebApi(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            config.EnableSwagger(c => c.SingleApiVersion("v1", "TaskLogger"))
                .EnableSwaggerUi();
            ConfigureDI(config);
            await DbConfig.RegisterAdmin();

        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            app.CreatePerOwinContext(TaskLoggerContext.Create);
            app.CreatePerOwinContext<TaskLoggerUserManager>(TaskLoggerUserManager.Create);

            var options = new OAuthAuthorizationServerOptions()
                                                          {
                                                              AllowInsecureHttp = true,
                                                              TokenEndpointPath =  new PathString("/oauth/token"),
                                                              AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                                                              Provider = new CustomOAuthProvider(),
                                                              AccessTokenFormat = new CustomJwtFormat("http://localhost:27568")
                                                          };
            app.UseOAuthAuthorizationServer(options);
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            var issuer = "http://localhost:27568";
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions()
                                               {
                                                   AuthenticationMode = AuthenticationMode.Active,
                                                   AllowedAudiences = new[] { audienceId},
                                                   IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                                                                                      {
                                                                                          new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret), 
                                                                                      }
                                               });
        }
    }
}
