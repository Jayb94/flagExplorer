using FlagExplorer.Domain.Configuration;
using FlagExplorer.Domain.InfrastructureContracts;
using FlagExplorer.Domain.Models;
using FlagExplorer.Domain.Services;
using Moq;
using Xunit;

namespace FlagExplorer.Tests.ServiceTests
{
    public class CountryServiceTests
    {
        private readonly Mock<ICountryInfoProvider> _countryInfoProvider = new Mock<ICountryInfoProvider>();
        private readonly ICountryService _countryService;

        public CountryServiceTests() 
        {
            _countryService = new CountryService(_countryInfoProvider.Object);
        }

        [Fact]
        public async Task RetrieveCountriesList_GivenValidRequest_ShouldReturnCountriesList()
        {
            // Arrange
            var countriesList = new List<Country>()
            {
                new Country() { Flag = "http://localhost/sa.png", Name = "South Africa" },
                new Country() { Flag = "http://localhost/argentina.png", Name = "Argentina" },
                new Country() { Flag = "http://localhost/brazil.png", Name = "Brazil" }
            };
            _countryInfoProvider.Setup(x => x.RetrieveCountriesList()).ReturnsAsync(countriesList);

            // Act
            var result = await _countryService.GetCountriesList();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Response<IEnumerable<Country>>>(result);
        }

        [Fact]
        public async Task GetCountryDetails_GivenValidRequest_ShouldReturnCountryInfo()
        {
            // Arrange
            var countryName = "South Africa";
            var countryDetails = new CountryDetails() { Flag = "http://localhost/sa.png", Name = "South Africa" };
            _countryInfoProvider.Setup(x => x.RetrieveCountryDetails(countryName)).ReturnsAsync(countryDetails);

            // Act
            var result = await _countryService.GetCountryDetails(countryName);

            // Assert
            var response = result.Content;

            Assert.NotNull(result);
            Assert.Equal(countryName, response?.Name);
            Assert.IsType<Response<CountryDetails>>(result);
        }

        [Fact]
        public async Task GetCountryDetails_GivenProviderIsDown_ShouldThrowAnException()
        {
            // Arrange
            var countryName = "South Africa";
            _countryInfoProvider.Setup(x => x.RetrieveCountryDetails(countryName)).Throws(new Exception("Could not connect to host"));

            // Act
            var result = await _countryService.GetCountryDetails(countryName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Could not connect to host", result.ErrorResponse?.ErrorCode);
        }
    }
}
