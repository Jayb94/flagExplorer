using FlagExplorer.API.Controllers;
using FlagExplorer.Domain.Configuration;
using FlagExplorer.Domain.Models;
using FlagExplorer.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FlagExplorer.Tests.ControllerTests
{
    public class CountryControllerTests
    {
        private CountryController _countryController;
        private readonly Mock<ICountryService> _countryService = new Mock<ICountryService>();
        private readonly Mock<ILogger<CountryController>> _logger = new Mock<ILogger<CountryController>>();

        public CountryControllerTests() 
        {
            _countryController = new CountryController(_logger.Object, _countryService.Object);
        }

        [Fact]
        public async Task Countries_GivenValidRequest_ShouldReturnCountriesList()
        {
            // Arrange
            var countriesList = new List<Country>()
            {
                new Country() { Flag = "http://localhost/sa.png", Name = "South Africa" },
                new Country() { Flag = "http://localhost/argentina.png", Name = "Argentina" },
                new Country() { Flag = "http://localhost/brazil.png", Name = "Brazil" }
            };
            _countryService.Setup(x => x.GetCountriesList()).ReturnsAsync(Response<IEnumerable<Country>>.Success(countriesList));

            // Act
            var result = await _countryController.Countries();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Response<IEnumerable<Country>>>(result);
        }

        [Fact]
        public async Task CountryDetails_GivenValidRequest_ShouldReturnCountryInfo()
        {
            // Arrange
            var countryName = "South Africa";
            var countryDetails = new CountryDetails() { Flag = "http://localhost/sa.png", Name = "South Africa" };

            _countryService.Setup(x => x.GetCountryDetails(It.IsAny<string>())).ReturnsAsync(Response<CountryDetails>.Success(countryDetails));

            // Act
            var result = await _countryController.CountryDetails(countryName);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Response<CountryDetails>>(result);
        }

        [Fact]
        public async Task CountryDetails_GivenInvalidCountryNameRequest_ShouldReturnNull()
        {
            // Arrange
            var countryName = "QQQQQQQQQ";

            // Act
            var result = await _countryController.CountryDetails(countryName);

            // Assert
            Assert.Null(result);
        }
    }
}
