using Autofac;
using Autofac.Integration.WebApi;
using DotNetFest2018.Demo0.App_Start;
using DotNetFest2018.Demo0.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace DotNetFest2018.Demo0
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = new ConfigurationBuilder();
            config
                .AddJsonFile($"appsettings.json", optional: true);

            IConfiguration configurationRoot = config.Build();

            var builder = new ContainerBuilder();

            builder.RegisterWebApi();

            builder.RegisterOptions();

            builder
                .Register<IConfigureOptions<CountryOptions>>((context) =>
                {
                    return new NamedConfigureFromConfigurationOptions<CountryOptions>(
                        Microsoft.Extensions.Options.Options.DefaultName,
                        configurationRoot.GetSection("CountrySettings"));
                }).As<IConfigureOptions<CountryOptions>>().SingleInstance();

            var httpConfig = GlobalConfiguration.Configuration;

            var container = builder.Build();
            httpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
