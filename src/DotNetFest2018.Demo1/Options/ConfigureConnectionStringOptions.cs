using DotNetFest2018.Demo1.Services;
using FluentMigrator.Runner.Processors;
using Microsoft.Extensions.Options;

namespace DotNetFest2018.Demo1.Options
{
    public class ConfigureConnectionStringOptions : IPostConfigureOptions<ProcessorOptions>
    {
        private readonly IConnectionStringService _connectonStringService;

        public ConfigureConnectionStringOptions(IConnectionStringService connectonStringService)
        {
            _connectonStringService = connectonStringService;
        }

        public void PostConfigure(string name, ProcessorOptions options)
        {
            options.ConnectionString = _connectonStringService.Get();
        }
    }
}