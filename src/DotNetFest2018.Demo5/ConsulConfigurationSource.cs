using Microsoft.Extensions.Configuration;

namespace DotNetFest2018.Demo5
{
    public class ConsulConfigurationSource : IConfigurationSource
    {
        private readonly bool _reloadOnChange;

        public ConsulConfigurationSource(bool reloadOnChange)
        {
            _reloadOnChange = reloadOnChange;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ConsulConfigurationProvider(_reloadOnChange);
        }
    }
}