using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using TennisBookings.Web.Configuration;

namespace TennisBookings.Web
{
    public class ValidateOptionsService : IHostedService
    {
        private readonly ILogger<ValidateOptionsService> _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IOptions<HomePageConfiguration> _homePageConfig;
        private readonly IOptionsMonitor<ExternalServicesConfig> _externalServicesConfig;

        public ValidateOptionsService(
            ILogger<ValidateOptionsService> logger, 
            IHostApplicationLifetime appLifetime, 
            IOptions<HomePageConfiguration> homePageConfig,
            IOptionsMonitor<ExternalServicesConfig> externalServicesConfig)
        {
            _logger = logger;            
            _appLifetime = appLifetime;
            _homePageConfig = homePageConfig;
            _externalServicesConfig = externalServicesConfig;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _ = _homePageConfig.Value; // accessing this triggers validation
                _ = _externalServicesConfig.Get(ExternalServicesConfig.WeatherApi);
            }
            catch (OptionsValidationException ex)
            {
                _logger.LogError("One or more options validation checks failed.");

                foreach (var failure in ex.Failures)
                {
                    _logger.LogError(failure);
                }

                _appLifetime.StopApplication(); // stop the app now
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask; // nothing to do
        }
    }
}
