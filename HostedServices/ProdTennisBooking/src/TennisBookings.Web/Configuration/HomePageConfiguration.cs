
using System.ComponentModel.DataAnnotations;

namespace TennisBookings.Web.Configuration
{
    public class HomePageConfiguration
    {
        public bool EnableGreeting { get; set; }
        public bool EnableWeatherForecast { get; set; }
        public string ForecastSectionTitle { get; set; }
    }
}
