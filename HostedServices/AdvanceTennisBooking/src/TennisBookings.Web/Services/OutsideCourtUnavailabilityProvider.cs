using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Domain;

namespace TennisBookings.Web.Services
{
    public class OutsideCourtUnavailabilityProvider : IUnavailabilityProvider
    {
        private readonly ICourtService _courtService;
        private readonly ICollection<int> _outdoorCourtWinterClosedHours;

        private readonly int[] _winterMonths;

        public OutsideCourtUnavailabilityProvider(ICourtService courtService, IOptions<ClubConfiguration> clubConfiguration)
        {
            _courtService = courtService;
            _winterMonths = clubConfiguration.Value.WinterMonths;

            var outdoorCourtWinterClosedHours = new List<int>();

            if (clubConfiguration.Value.WinterCourtStartHour > 0 && clubConfiguration.Value.WinterCourtStartHour > clubConfiguration.Value.OpenHour)
            {
                for (var i = 0; i < clubConfiguration.Value.WinterCourtStartHour; i++)
                {
                    outdoorCourtWinterClosedHours.Add(i);
                }
            }

            if (clubConfiguration.Value.WinterCourtEndHour <= 23 && clubConfiguration.Value.WinterCourtEndHour < clubConfiguration.Value.CloseHour)
            {
                for (var i = clubConfiguration.Value.WinterCourtEndHour; i <= 23; i++)
                {
                    outdoorCourtWinterClosedHours.Add(i);
                }
            }

            _outdoorCourtWinterClosedHours = outdoorCourtWinterClosedHours;
        }

        public async Task<IEnumerable<HourlyUnavailability>> GetHourlyUnavailabilityAsync(DateTime date)
        {
            var isWinter = _winterMonths.Contains(date.Month);

            if (!isWinter)
                return Array.Empty<HourlyUnavailability>();

            var courts = await _courtService.GetOutdoorCourts();
            var outsideCourtIds = courts.Select(c => c.Id).ToHashSet();

            var hourlyUnavailability = _outdoorCourtWinterClosedHours.Select(closedHour => new HourlyUnavailability(closedHour, outsideCourtIds));

            return hourlyUnavailability;
        }

        public async Task<IEnumerable<int>> GetHourlyUnavailabilityAsync(DateTime date, int courtId)
        {
            var courts = await _courtService.GetOutdoorCourts();

            return courts.Select(x => x.Id).Contains(courtId) ? _outdoorCourtWinterClosedHours : Array.Empty<int>();
        }
    }
}
