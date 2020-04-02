using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Domain;

namespace TennisBookings.Web.Services
{
    public class ClubClosedUnavailabilityProvider : IUnavailabilityProvider
    {
        private readonly ICourtService _courtService;
        private readonly ICollection<int> _closedHours;

        public ClubClosedUnavailabilityProvider(ICourtService courtService, IOptions<ClubConfiguration> clubConfiguration)
        {
            _courtService = courtService;

            var closedHours = new List<int>();

            if (clubConfiguration.Value.OpenHour > 0)
            {
                for (var i = 0; i < clubConfiguration.Value.OpenHour; i++)
                {
                    closedHours.Add(i);
                }
            }

            if (clubConfiguration.Value.CloseHour <= 23)
            {
                for (var i = clubConfiguration.Value.CloseHour; i <= 23; i++)
                {
                    closedHours.Add(i);
                }
            }

            _closedHours = closedHours;
        }

        public async Task<IEnumerable<HourlyUnavailability>> GetHourlyUnavailabilityAsync(DateTime date)
        {
            var courtIds = await _courtService.GetCourtIds();

            var hourlyUnavailability = _closedHours.Select(closedHour => new HourlyUnavailability(closedHour, courtIds));

            return hourlyUnavailability;
        }

        public Task<IEnumerable<int>> GetHourlyUnavailabilityAsync(DateTime date, int courtId)
        {
            return Task.FromResult(_closedHours.AsEnumerable());
        }
    }
}
