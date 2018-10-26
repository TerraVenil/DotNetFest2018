using Microsoft.Extensions.Configuration;

namespace DotNetFest2018.Demo5
{
    public static class ConsulConfigurationExtensions
    {
        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder builder, bool reloadOnChange = false)
        {
            builder.Add(new ConsulConfigurationSource(reloadOnChange));

            return builder;
        }
    }
}