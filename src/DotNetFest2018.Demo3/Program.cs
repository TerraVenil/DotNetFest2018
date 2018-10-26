using System;
using System.Net;
using DotNetFest2018.Demo3.Clients;
using DotNetFest2018.Demo3.Options;
using DotNetFest2018.Demo3.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace DotNetFest2018.Demo3
{
    class Program
    {
        static void Main(string[] args) => WebHost.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<ICountryService, CountryService>();
                services.AddSingleton<ICountryApiClient, CountryApiClient>();

                services.Configure<CountryOptions>(
                    context.Configuration.GetSection("Countries"));

                services.AddSingleton<IConfigureOptions<CountryOptions>, ConfigureCountryOptions>();

                services.AddHttpClient();

                services.PostConfigure<HttpClientFactoryOptions>(options =>
                {
                    options.HandlerLifetime = TimeSpan.FromMinutes(15);
                });
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
            })
            .Configure(app =>
            {
                app.Run(context =>
                {
                    var countryService = context.RequestServices.GetService<ICountryService>();

                    var defaultCountry = countryService.GetDefaultCountry();

                    context.Response.StatusCode = (int)HttpStatusCode.OK;

                    return context.Response.WriteAsync($"Default country is {defaultCountry} \n");
                });
            })
            .Build()
            .Run();
    }
}
