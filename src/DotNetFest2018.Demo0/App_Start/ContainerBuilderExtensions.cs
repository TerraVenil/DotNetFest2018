using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace DotNetFest2018.Demo0.App_Start
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterWebApi(this ContainerBuilder builder)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }

        public static void RegisterOptions(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(OptionsManager<>))
              .As(typeof(IOptions<>))
              .SingleInstance();

            builder.RegisterGeneric(typeof(OptionsFactory<>))
                .As(typeof(IOptionsFactory<>))
                .SingleInstance();
        }
    }
}