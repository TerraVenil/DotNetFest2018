using DotNetFest2018.Demo3.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace DotNetFest2018.Demo3.Options
{
    public class ConfigureCountryOptions : IConfigureOptions<CountryOptions>
    {
        private readonly ICountryApiClient _countryApiClient;
        private readonly ILogger _logger;

        public ConfigureCountryOptions(ICountryApiClient countryApiClient, ILogger<ConfigureCountryOptions> logger)
        {
            _countryApiClient = countryApiClient;
            _logger = logger;
        }

        public void Configure(CountryOptions options)
        {
            try
            {
                options.Countries = _countryApiClient.GetCountries();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while get countries.");
            }
        }
    }
}