using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Domain.Rules;
using Xunit;

namespace TennisBookings.Web.Tests
{
    public class ClubIsOpenRuleTests
    {
        [Fact]
        public async Task CompliesWithRuleAsync_ReturnsTrue_WhenValuesAreValid()
        {
            var options = Options.Create(new ClubConfiguration { OpenHour = 9, CloseHour = 22 });
            var sut = new ClubIsOpenRule(options);

            var result = await sut.CompliesWithRuleAsync(new Data.CourtBooking()
            {
                StartDateTime = new DateTime(2019, 01, 01, 10, 00, 00),
                EndDateTime = new DateTime(2019, 01, 01, 12, 00, 00)
            });

            Assert.True(result);
        }

        [Fact]
        public async Task CompliesWithRuleAsync_ReturnsTrue_WhenBookingIncludesOpenAndCloseHours()
        {
            var options = Options.Create(new ClubConfiguration { OpenHour = 9, CloseHour = 22 });

            var sut = new ClubIsOpenRule(options);

            var result = await sut.CompliesWithRuleAsync(new Data.CourtBooking()
            {
                StartDateTime = new DateTime(2019, 01, 01, 09, 00, 00),
                EndDateTime = new DateTime(2019, 01, 01, 22, 00, 00)
            });

            Assert.True(result);
        }

        [Fact]
        public async Task CompliesWithRuleAsync_ReturnsFalse_WhenBookingStartsBeforeClubIsOpen()
        {
            var options = Options.Create(new ClubConfiguration { OpenHour = 9, CloseHour = 22 });

            var sut = new ClubIsOpenRule(options);

            var result = await sut.CompliesWithRuleAsync(new Data.CourtBooking()
            {
                StartDateTime = new DateTime(2019, 01, 01, 08, 00, 00), // this is too early
                EndDateTime = new DateTime(2019, 01, 01, 10, 00, 00)
            });

            Assert.False(result);
        }

        // this test uses moq
        [Fact]
        public async Task CompliesWithRuleAsync_ReturnsFalse_WhenBookingEndsAfterClubIsClosed()
        {
            var mockOptions = new Mock<IOptions<ClubConfiguration>>();
            mockOptions.SetupGet(x => x.Value)
                .Returns(new ClubConfiguration { OpenHour = 9, CloseHour = 22 });

            var sut = new ClubIsOpenRule(mockOptions.Object);

            var result = await sut.CompliesWithRuleAsync(new Data.CourtBooking()
            {
                StartDateTime = new DateTime(2019, 01, 01, 21, 00, 00),
                EndDateTime = new DateTime(2019, 01, 01, 23, 00, 00) // this is too late
            });

            Assert.False(result);
        }
    }
}
