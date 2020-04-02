using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Domain.Rules
{
    /// <summary>
    /// A rule which prevents a single booking being longer than the configured max booking.
    /// </summary>
    public class MaxBookingLengthRule : ISingletonCourtBookingRule
    {
        private readonly BookingConfiguration _bookingConfiguration;

        public MaxBookingLengthRule(IOptions<BookingConfiguration> bookingConfiguration)
        {
            _bookingConfiguration = bookingConfiguration.Value;
        }

        public Task<bool> CompliesWithRuleAsync(CourtBooking booking)
        {
            var bookingLength = booking.EndDateTime - booking.StartDateTime;

            var compliesWithRule = bookingLength <= TimeSpan.FromHours(_bookingConfiguration.MaxRegularBookingLengthInHours);

            return Task.FromResult(compliesWithRule);
        }

        public string ErrorMessage => "Booking is longer than allowed booking length";
    }
}
