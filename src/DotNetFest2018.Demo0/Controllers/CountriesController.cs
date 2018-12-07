using DotNetFest2018.Demo0.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DotNetFest2018.Demo0.Controllers
{
    public class CountriesController : ApiController
    {
        private readonly CountryOptions _countryOptions;

        public CountriesController(IOptions<CountryOptions> countryOptions)
        {
            _countryOptions = countryOptions.Value;
        }

        [HttpGet]
        public object Get()
        {
            return _countryOptions.Countries;
        }
    }
}
