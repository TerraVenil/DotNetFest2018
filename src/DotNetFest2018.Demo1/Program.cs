using System;
using System.Reflection;
using DotNetFest2018.Demo1.Options;
using DotNetFest2018.Demo1.Services;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DotNetFest2018.Demo1
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider();

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var scopedServiceProvider = scope.ServiceProvider;

                var runner = scopedServiceProvider.GetRequiredService<IMigrationRunner>();

                runner.MigrateUp();
            }

            Console.ReadLine();
        }

        private static IServiceProvider CreateServiceProvider()
        {
            var connectionString = "Data Source=test.db";

            var services = new ServiceCollection();

            services.AddSingleton<IConnectionStringService, ConnectionStringService>();

            return services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider();
        }
    }
}
