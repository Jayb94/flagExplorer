using FlagExplorer.Domain.InfrastructureContracts;
using FlagExplorer.Domain.Models;
using FlagExplorer.Infrastructure.Dtos;
using FlagExplorer.Infrastructure.Options;
using FlagExplorer.Infrastructure.Providers;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using Xunit;

namespace FlagExplorer.Tests.InfrastructureTests
{
    public class CountryInfoProviderTests
    {
        private readonly IOptions<CountryInfoProviderOptions> _options;
        private readonly HttpClient _httpClient;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly ICountryInfoProvider _countryInfoProvider;

        public CountryInfoProviderTests() 
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _options = CreateCountryInfoProviderOptions();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri(_options.Value.BaseAddress)
            };
            _countryInfoProvider = new CountryInfoProvider(_httpClient, _options);
        }

        [Fact]
        public async Task RetrieveCountriesList_GivenValidRequest_ShouldReturnCountriesList()
        {
            // Arrange
            var countriesList = new List<CountryDto>()
            {
                new CountryDto() { Flags = new FlagsDto(){ Png = "http://localhost/sa.png" }, Name = new NameDto() { Official = "South Africa" } },
                new CountryDto() { Flags = new FlagsDto(){ Png = "http://localhost/brazil.png" }, Name = new NameDto() { Official = "Brazil" } },
                new CountryDto() { Flags = new FlagsDto(){ Png = "http://localhost/botswana.png" }, Name = new NameDto() { Official = "Botswana" } }
            };

            _httpMessageHandlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
               "SendAsync",
               ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsolutePath.Contains("/all")),
               ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(new HttpResponseMessage()
           {
               Content = new StringContent(JsonSerializer.Serialize(countriesList)),
               StatusCode = HttpStatusCode.OK
           });

            // Act
            var result = await _countryInfoProvider.RetrieveCountriesList();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Country>>(result);
        }

        [Fact]
        public async Task RetrieveCountriesList_GivenProviderApiIsDown_ShouldReturnNull()
        {
            // Arrange
            _httpMessageHandlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
               "SendAsync",
               ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsolutePath.Contains("/all")),
               ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(new HttpResponseMessage()
           {
               Content = new StringContent(""),
               StatusCode = HttpStatusCode.InternalServerError
           });

            // Act
            var result = await _countryInfoProvider.RetrieveCountriesList();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task RetrieveCountryDetails_GivenValidRequest_ShouldReturnCountryDetails()
        {
            // Arrange
            var countryName = "South Africa";
            var countryDetails = new List<CountryDetailsDto>() { new CountryDetailsDto() { Capital = new[] { "Pretoria" }, Flags = new FlagsDto() { Png = "http://localhost/sa.png" }, Name = new NameDto() { Official = "South Africa" }, Population = 60000000 } };

            _httpMessageHandlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
               "SendAsync",
               ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsolutePath.Contains("/name/")),
               ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(new HttpResponseMessage()
           {
               Content = new StringContent(JsonSerializer.Serialize(countryDetails)),
               StatusCode = HttpStatusCode.OK
           });

            // Act
            var result = await _countryInfoProvider.RetrieveCountryDetails(countryName);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CountryDetails>(result);
        }

        private OptionsWrapper<CountryInfoProviderOptions> CreateCountryInfoProviderOptions()
        {
            return new(new CountryInfoProviderOptions
            {
                BaseAddress = "http://localhost:22000",
                GetAllEndpoint = "/all",
                GetByNameEndpoint = "/name/{0}"
            });
        }
    }
}
