using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.ResultsProcessing.ExternalServices.Players;
using TennisBookings.ResultsProcessing.ExternalServices.Statistics;

namespace TennisBookings.ResultsProcessing
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTennisPlayerApiClient(this IServiceCollection services, Action<TennisPlayerApiClientOptions> setupAction)
        {
            services.AddHttpClient<ITennisPlayerApiClient, TennisPlayerApiClient>();

            services.AddOptions();
            services.Configure(setupAction);

            return services;
        }

        public static IServiceCollection AddStatisticsApiClient(this IServiceCollection services, Action<StatisticsApiClientOptions> setupAction)
        {
            services.AddHttpClient<IStatisticsApiClient, StatisticsApiClient>();

            services.AddOptions();
            services.Configure(setupAction);

            return services;
        }

        public static IServiceCollection AddResultProcessing(this IServiceCollection services)
        {
            services.TryAddSingleton<ICsvResultParser, CsvResultParser>();
            services.TryAddScoped<IResultProcessor, ResultProcessor>();

            return services;
        }
    }
}
