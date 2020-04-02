using Microsoft.Extensions.Options;

namespace TennisBookings.Web.Configuration
{
    public class ExternalServicesConfigurationValidation : IValidateOptions<ExternalServicesConfig>
    {
        private readonly WeatherForecastingConfiguration _weatherConfig;

        public ExternalServicesConfigurationValidation(IOptions<WeatherForecastingConfiguration> weatherConfig)
        {
            _weatherConfig = weatherConfig.Value;            
        }

        public ValidateOptionsResult Validate(string name, ExternalServicesConfig options)
        {
            switch (name)
            {
                case ExternalServicesConfig.WeatherApi:
                    var result = ValidateWeatherApiConfig(_weatherConfig.EnableWeatherForecast, options);
                    if (result.Failed)
                        return result;
                    break;

                case ExternalServicesConfig.ProductsApi:
                    if (string.IsNullOrEmpty(options.Url))
                    {
                        return ValidateOptionsResult.Fail("A URL for the product API is required.");
                    }
                    if (options.MinsToCache < 1)
                    {
                        return ValidateOptionsResult.Fail("The products caching must be at least 1 minute.");
                    }
                    break;

                default:
                    return ValidateOptionsResult.Skip;
            }

            return ValidateOptionsResult.Success;
        }

        public static ValidateOptionsResult ValidateWeatherApiConfig(bool weatherForecastEnabled, ExternalServicesConfig options)
        {
            if (weatherForecastEnabled && string.IsNullOrEmpty(options.Url))
            {
                return ValidateOptionsResult.Fail("A URL for the weather API is required when weather forecasting is enabled.");
            }
            if (options.MinsToCache < 10)
            {
                return ValidateOptionsResult.Fail("The weather forecast cachine must be at least 10 minutes.");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
