using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using TaskLogger.Data.Abstract;
using TaskLogger.Data.Concrete;

namespace TaskLogger.App_Start
{
    public partial class StartUp
    {
        public void ConfigureDI(HttpConfiguration config)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(RegisterServices(new ContainerBuilder()));
        }

        private IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            return builder.Build();
        }
    }
}
