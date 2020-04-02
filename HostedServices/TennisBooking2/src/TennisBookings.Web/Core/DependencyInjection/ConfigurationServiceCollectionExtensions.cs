using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TennisBookings.Web.Configuration;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {            
            services.Configure<ClubConfiguration>(config.GetSection("ClubSettings"));
            services.Configure<BookingConfiguration>(config.GetSection("CourtBookings"));
            services.Configure<MembershipConfiguration>(config.GetSection("Membership"));

            return services;
        }
    }
}
