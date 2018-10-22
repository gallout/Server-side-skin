using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;
using Autofac.Integration.WebApi;
using Autofac;
using testview.Models;
using Microsoft.OData.Edm;

using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;

namespace testview
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // OData:
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Student>("ODataStudent");

            config.MapODataServiceRoute(
                routeName: "odata",
                routePrefix: null,
                model: builder.GetEdmModel());


            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            GlobalConfiguration.Configuration.Formatters
            .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            var cors = new EnableCorsAttribute("http://localhost:3000", "*", "*");
            config.EnableCors(cors);
        }
    }
}
