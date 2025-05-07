using FlagExplorer.Domain.Configuration;
using FlagExplorer.Domain.Models;
using FlagExplorer.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlagExplorer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : Controller
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ICountryService _countryService;

        public CountryController(ILogger<CountryController> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        [HttpGet("countries")]
        public async Task<Response<IEnumerable<Country>>> Countries()
        {
            var response = await _countryService.GetCountriesList();

            return response;
        }

        [HttpGet("countries/{name}")]
        public async Task<Response<CountryDetails>> CountryDetails(string name)
        {
            var response = await _countryService.GetCountryDetails(name);

            return response;
        }
    }
}
