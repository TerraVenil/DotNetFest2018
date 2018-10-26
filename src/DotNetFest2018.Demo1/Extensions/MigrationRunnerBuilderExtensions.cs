using System;
using FluentMigrator.Runner.Processors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FluentMigrator.Runner
{
    public static class MigrationRunnerBuilderExtensions
    {
        public static IMigrationRunnerBuilder WithGlobalConnectionString(
            this IMigrationRunnerBuilder builder, Func<IServiceProvider, string> configureConnectionString)
        {
            builder.Services
                .AddSingleton<IConfigureOptions<ProcessorOptions>>(s =>
                {
                    Console.WriteLine($"Connection string {configureConnectionString(s)}");

                    return new ConfigureNamedOptions<ProcessorOptions>(Options.DefaultName,
                        opt => opt.ConnectionString = configureConnectionString(s));
                });

            return builder;
        }
    }
}