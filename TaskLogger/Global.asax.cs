using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using TaskLogger.App_Start;

namespace TaskLogger
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = new HttpConfiguration();
            GlobalConfiguration.Configure(WebApiConfig.Register);            
            //StartUp.ConfigureDI(config);
            DbConfig.RegisterAdmin();
        }
    }
}
