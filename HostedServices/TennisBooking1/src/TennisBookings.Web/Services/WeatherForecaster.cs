using System.Threading.Tasks;
using TennisBookings.Web.Domain;
using TennisBookings.Web.External;

namespace TennisBookings.Web.Services
{
    public class WeatherForecaster : IWeatherForecaster
    {
        private readonly IWeatherApiClient _weatherApiClient;

        public WeatherForecaster(IWeatherApiClient weatherApiClient)
        {
            _weatherApiClient = weatherApiClient;
        }

        public bool ForecastEnabled => true;

        public async Task<CurrentWeatherResult> GetCurrentWeatherAsync()
        {
            var currentWeather = await _weatherApiClient.GetWeatherForecastAsync();

            if (currentWeather == null) return null;

            var result = new CurrentWeatherResult
            {
                Description = currentWeather.Weather.Description
            };

            return result;
        }
    }
}
