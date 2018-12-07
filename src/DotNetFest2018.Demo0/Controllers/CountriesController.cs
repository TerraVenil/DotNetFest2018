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
        public CountriesController()
        {
        }

        [HttpGet]
        public object Get()
        {
            return new object[] { (code: "UK", name: "United Kingdom"), (code: "USA", name: "United States") };
        }
    }
}
