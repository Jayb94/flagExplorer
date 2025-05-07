using FlagExplorer.Domain.Models;

namespace FlagExplorer.Domain.InfrastructureContracts
{
    public interface ICountryInfoProvider
    {
        public Task<IEnumerable<Country>?> RetrieveCountriesList();

        public Task<CountryDetails> RetrieveCountryDetails(string name);
    }
}
