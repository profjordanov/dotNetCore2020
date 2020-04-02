using Microsoft.Extensions.Configuration;

namespace TennisBookings.Web.Core
{
    public static class ConfigurationExtensions
    {
        public static bool IsWeatherForecastEnabled(this IConfiguration config) => config.GetValue<bool>("Features:WeatherForecasting:EnableWeatherForecast");
    }
}
