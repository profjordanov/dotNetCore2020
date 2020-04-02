using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TennisBookings.Web.External.Models;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;

namespace TennisBookings.Web.External
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherApiClient> _logger;

        public WeatherApiClient(HttpClient httpClient, IOptionsMonitor<ExternalServicesConfig> options, ILogger<WeatherApiClient> logger)
        {
            var externalServiceConfig = options.Get(ExternalServicesConfig.WeatherApi);

            httpClient.BaseAddress = new Uri(externalServiceConfig.Url);

            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<WeatherApiResult> GetWeatherForecastAsync(CancellationToken cancellationToken = default)
        {
            const string path = "api/currentWeather/Brighton";

            try
            {
                var response = await _httpClient.GetAsync(path, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await response.Content.ReadAsAsync<WeatherApiResult>(cancellationToken);

                return content;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Failed to get weather data from API");
            }

            return null;
        }
    }
}
