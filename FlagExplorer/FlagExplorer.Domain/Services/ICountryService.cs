using FlagExplorer.Domain.Configuration;
using FlagExplorer.Domain.Models;

namespace FlagExplorer.Domain.Services
{
    public interface ICountryService
    {
        public Task<Response<IEnumerable<Country>>> GetCountriesList();

        public Task<Response<CountryDetails>> GetCountryDetails(string name);
    }
}
