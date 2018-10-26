using System.Linq;
using DotNetFest2018.Demo3.Options;
using Microsoft.Extensions.Options;

namespace DotNetFest2018.Demo3.Services
{
    public class CountryService : ICountryService
    {
        private readonly CountryOptions _countryValue;

        public CountryService(IOptions<CountryOptions> countryOptions)
        {
            _countryValue = countryOptions.Value;
        }

        public string GetDefaultCountry() => _countryValue.Countries.Any()
            ? _countryValue.Countries.First()
            : _countryValue.DefaultCountry;
    }
}