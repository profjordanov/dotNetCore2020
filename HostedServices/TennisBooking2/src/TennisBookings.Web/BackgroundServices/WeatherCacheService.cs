using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Core.Caching;
using TennisBookings.Web.Domain;
using TennisBookings.Web.External;

namespace TennisBookings.Web.BackgroundServices
{
    public class WeatherCacheService : BackgroundService
    {
        private readonly IWeatherApiClient _weatherApiClient;
        private readonly IDistributedCache<CurrentWeatherResult> _cache;
        private readonly ILogger<WeatherCacheService> _logger;

        private readonly int _minutesToCache;
        private readonly int _refreshIntervalInSeconds;

        public WeatherCacheService(
            IWeatherApiClient weatherApiClient,
            IDistributedCache<CurrentWeatherResult> cache,
            IOptionsMonitor<ExternalServicesConfig> options,
            ILogger<WeatherCacheService> logger)
        {
            _weatherApiClient = weatherApiClient;
            _cache = cache;
            _logger = logger;
            _minutesToCache = options.Get(ExternalServicesConfig.WeatherApi).MinsToCache;
            _refreshIntervalInSeconds = _minutesToCache > 1 ? (_minutesToCache - 1) * 60 : 30;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var forecast = await _weatherApiClient.GetWeatherForecastAsync(stoppingToken);

                if (forecast is object)
                {
                    var currentWeather = new CurrentWeatherResult { Description = forecast.Weather.Description };

                    var cacheKey = $"current_weather_{DateTime.UtcNow:yyyy_MM_dd}";

                    _logger.LogInformation("Updating weather in cache.");

                    await _cache.SetAsync(cacheKey, currentWeather, _minutesToCache);
                }

                await Task.Delay(TimeSpan.FromSeconds(_refreshIntervalInSeconds), stoppingToken);
            }
        }
    }
}
