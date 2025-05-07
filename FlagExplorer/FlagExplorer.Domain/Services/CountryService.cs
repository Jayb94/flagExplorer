using FlagExplorer.Domain.Configuration;
using FlagExplorer.Domain.InfrastructureContracts;
using FlagExplorer.Domain.Models;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlagExplorer.Domain.Services
{
    public class CountryService : ICountryService
    {
        private ICountryInfoProvider _countryInfoProvider;

        public CountryService(ICountryInfoProvider countryInfoProvider)
        {
            _countryInfoProvider = countryInfoProvider;
        }

        public async Task<Response<IEnumerable<Country>>> GetCountriesList()
        {
            IEnumerable<Country>? countriesList;

            try
            {
                countriesList = await _countryInfoProvider.RetrieveCountriesList();
            }
            catch(Exception ex)
            {
                return Response<IEnumerable<Country>>.Failure(new SystemErrorResponse(ex));
            }

            return Response<IEnumerable<Country>>.Success(countriesList);
        }

        public async Task<Response<CountryDetails>> GetCountryDetails(string name)
        {
           CountryDetails? countryDetails;

            try
            {
                countryDetails = await _countryInfoProvider.RetrieveCountryDetails(name);
            }
            catch (Exception ex)
            {
                return Response<CountryDetails>.Failure(new SystemErrorResponse(ex));
            }

            return Response<CountryDetails>.Success(countryDetails);
        }
    }
}
