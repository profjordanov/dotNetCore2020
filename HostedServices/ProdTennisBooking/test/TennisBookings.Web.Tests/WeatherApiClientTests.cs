using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.External;
using TennisBookings.Web.External.Models;
using Xunit;

namespace TennisBookings.Web.Tests
{
    public class WeatherApiClientTests
    {
        [Fact]
        public async Task GetWeatherForecastAsync_ReturnsNull_WhenAnExceptionOccurs()
        {
            var config = new ExternalServicesConfig { Url = "http://www.example.com", MinsToCache = 0 };

            var mockOptions = new Mock<IOptionsMonitor<ExternalServicesConfig>>();
            mockOptions.Setup(x => x.Get(It.IsAny<string>())).Returns(config);

            var client = new HttpClient(new ExceptionHandler());

            var sut = new WeatherApiClient(client, mockOptions.Object, NullLogger<WeatherApiClient>.Instance);

            var result = await sut.GetWeatherForecastAsync();

            Assert.Null(result);
        }

        [Fact]
        public async Task GetWeatherForecastAsync_ReturnsWeatherApiResult_WhenHttpRequestSucceeds()
        {
            var config = new ExternalServicesConfig { Url = "http://www.example.com", MinsToCache = 0 };

            var mockOptions = new Mock<IOptionsMonitor<ExternalServicesConfig>>();
            mockOptions.Setup(x => x.Get(It.IsAny<string>())).Returns(config);

            var client = new HttpClient(new SuccessHandler());

            var sut = new WeatherApiClient(client, mockOptions.Object, NullLogger<WeatherApiClient>.Instance);

            var result = await sut.GetWeatherForecastAsync();

            Assert.IsType<WeatherApiResult>(result);
            Assert.Equal("London", result.City);
        }

        private class ExceptionHandler : HttpClientHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                throw new HttpRequestException();
            }
        }

        private class SuccessHandler : HttpClientHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var result = new WeatherApiResult
                {
                    City = "London"
                };

                var contentString = JsonConvert.SerializeObject(result);

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(contentString, Encoding.UTF8, "application/json")
                };

                return Task.FromResult(response);
            }
        }
    }
}
