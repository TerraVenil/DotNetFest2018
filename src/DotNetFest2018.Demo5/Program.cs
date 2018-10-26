using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace DotNetFest2018.Demo5
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddConsul(reloadOnChange: true)
                .Build();

            var section = configuration.GetSection("Logging:MinimumLevel");

            Console.WriteLine($"Begin waiting notification changes on key {ConsulConstants.MinimumLevelKey}.");

            ChangeToken.OnChange(section.GetReloadToken, () =>
            {
                Console.WriteLine("IConfigurationRoot rebuilded.");

                Console.WriteLine($"New value is {configuration[ConsulConstants.MinimumLevelKey]}.");
            });

            Console.ReadLine();
        }
    }
}
