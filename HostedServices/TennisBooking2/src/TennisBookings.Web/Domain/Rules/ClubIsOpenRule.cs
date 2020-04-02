using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Domain.Rules
{
    public class ClubIsOpenRule : ISingletonCourtBookingRule
    {
        private readonly ClubConfiguration _clubConfiguration;

        public ClubIsOpenRule(IOptions<ClubConfiguration> clubConfiguration)
        {
            _clubConfiguration = clubConfiguration.Value;
        }

        public Task<bool> CompliesWithRuleAsync(CourtBooking booking)
        {
            var startHourPasses = booking.StartDateTime.Hour >= _clubConfiguration.OpenHour;
            var endHourPasses = booking.EndDateTime.Hour <= _clubConfiguration.CloseHour;

            return Task.FromResult(startHourPasses && endHourPasses);
        }

        public string ErrorMessage => "Can't make a booking when the club is closed";
    }
}
