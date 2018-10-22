using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using testview.DAL;
using testview.Interfaces;

namespace testview.App_Start
{
    public class DIConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var dependencyContainer = new ContainerBuilder();
            dependencyContainer.RegisterApiControllers(Assembly.GetExecutingAssembly());
            dependencyContainer.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(dependencyContainer.Build());
        }

    }
}