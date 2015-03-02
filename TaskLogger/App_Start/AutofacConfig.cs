﻿using System;
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

namespace TaskLogger
{
    public partial class Startup
    {
        public static void ConfigureDI(HttpConfiguration config)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(RegisterServices(new ContainerBuilder()));
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            return builder.Build();
        }
    }
}
