using FlagExplorer.Domain.InfrastructureContracts;
using FlagExplorer.Domain.Models;
using FlagExplorer.Infrastructure.Dtos;
using FlagExplorer.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FlagExplorer.Infrastructure.Providers
{
    public class CountryInfoProvider : ICountryInfoProvider
    {
        private HttpClient _httpClient;
        private CountryInfoProviderOptions _countryInfoProviderOptions;

        public CountryInfoProvider(HttpClient httpClient, IOptions<CountryInfoProviderOptions> countryInfoProviderOptions)
        {
            _countryInfoProviderOptions = countryInfoProviderOptions.Value;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Country>?> RetrieveCountriesList()
        {
            IEnumerable<Country>? countriesList = null;
            var response = await _httpClient.GetAsync(_countryInfoProviderOptions.BaseAddress + _countryInfoProviderOptions.GetAllEndpoint);

            var responseStr = await response.Content.ReadAsStringAsync();

            if(response.IsSuccessStatusCode)
            {
                var responseObj = JsonSerializer.Deserialize<IEnumerable<CountryDto>>(responseStr);

                countriesList = responseObj?.Select(x => new Country().ToDomain(x.Name.Official, x.Flags.Png)).ToList();
            }

            return countriesList;
        }

        public async Task<CountryDetails?> RetrieveCountryDetails(string name)
        {
            CountryDetails countryDetails = null;
            var uri = string.Format(_countryInfoProviderOptions.GetByNameEndpoint, name);

            var response = await _httpClient.GetAsync(_countryInfoProviderOptions.BaseAddress + uri);

            var responseStr = await response.Content.ReadAsStringAsync();

            if(response.IsSuccessStatusCode)
            {
                var responseObj = JsonSerializer.Deserialize<IEnumerable<CountryDetailsDto>>(responseStr);

                countryDetails = responseObj?.Select(x => new CountryDetails().ToDomain(x.Name.Official, x.Flags.Png, string.Join(", ", x.Capital), x.Population)).FirstOrDefault();
            }     

            return countryDetails;
        }
    }
}
