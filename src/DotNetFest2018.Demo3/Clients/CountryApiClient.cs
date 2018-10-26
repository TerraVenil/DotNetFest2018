using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace DotNetFest2018.Demo3.Clients
{
    public class CountryApiClient : ICountryApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CountryApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public string[] GetCountries() =>
            JsonConvert.DeserializeObject<Dictionary<string, string>>(
                    _httpClientFactory
                        .CreateClient()
                        .GetAsync("http://country.io/names.json")
                        .GetAwaiter()
                        .GetResult()
                        .Content
                        .ReadAsStringAsync()
                        .GetAwaiter()
                        .GetResult())
                .Select(x => x.Key)
                .ToArray();
    }
}