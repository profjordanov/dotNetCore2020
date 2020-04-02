namespace TennisBookings.Web.Configuration
{
    public class ExternalServicesConfig
    {
        public const string WeatherApi = "WeatherApi";
        public const string ProductsApi = "ProductsApi";
        public const string TennisPlayersApi = "TennisPlayersApi";

        public string Url { get; set; }
        public int MinsToCache { get; set; }
        public string ApiKey { get; set; }
    }
}
