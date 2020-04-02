using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Core.Caching;
using TennisBookings.Web.Domain;

namespace TennisBookings.Web.Services
{
    public class CachedWeatherForecaster : IWeatherForecaster
    {
        private readonly IWeatherForecaster _weatherForecaster;
        private readonly IDistributedCache<CurrentWeatherResult> _cache;
        private readonly int _minsToCache;

        public bool ForecastEnabled => _weatherForecaster.ForecastEnabled;

        public CachedWeatherForecaster(IWeatherForecaster weatherForecaster, 
            IDistributedCache<CurrentWeatherResult> cache, IOptionsMonitor<ExternalServicesConfig> options)
        {
            _weatherForecaster = weatherForecaster;
            _cache = cache;
            _minsToCache = options.Get(ExternalServicesConfig.WeatherApi).MinsToCache;
        }

        public async Task<CurrentWeatherResult> GetCurrentWeatherAsync()
        {
            var cacheKey = $"current_weather_{DateTime.UtcNow:yyyy_MM_dd}";

            var (isCached, forecast) = await _cache.TryGetValueAsync(cacheKey);

            if (isCached)
                return forecast;

            var result = await _weatherForecaster.GetCurrentWeatherAsync();

            if (result != null)
                await _cache.SetAsync(cacheKey, result, _minsToCache);

            return result;
        }
    }
}
