using System;
using System.IO;
using System.Net;
using DotNetFest2018.Demo4.Options;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DotNetFest2018.Demo4
{
    class Program
    {
        static void Main(string[] args) => WebHost.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.Configure<CurrencyOptions>(
                    context.Configuration.GetSection("Currencies"));
            })
            .UseContentRoot(AppDomain.CurrentDomain.BaseDirectory)
            .Configure(app =>
            {
                app.Run(context =>
                {
                    var currencyOptions = context.RequestServices.GetService<IOptionsSnapshot<CurrencyOptions>>();

                    var defaultCurrency = currencyOptions.Value.DefaultCurrency;

                    context.Response.StatusCode = (int)HttpStatusCode.OK;

                    return context.Response.WriteAsync($"Default currency is {defaultCurrency} \n");
                });
            })
            .Build()
            .Run();
    }
}
