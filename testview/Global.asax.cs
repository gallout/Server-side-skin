﻿using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using testview.App_Start;

namespace testview
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            GlobalConfiguration.Configure(config =>
            {
                config.MapHttpAttributeRoutes();
                WebApiConfig.Register(config);
                DIConfig.Register(config);
            });

        }
    }
}
