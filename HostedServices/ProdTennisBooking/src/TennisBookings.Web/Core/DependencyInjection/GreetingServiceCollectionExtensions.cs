using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class GreetingServiceCollectionExtensions
    {
        public static IServiceCollection AddGreetings(this IServiceCollection services)
        {
            services.TryAddSingleton<IGreetingService, GreetingService>();

            return services;
        }
    }
}
