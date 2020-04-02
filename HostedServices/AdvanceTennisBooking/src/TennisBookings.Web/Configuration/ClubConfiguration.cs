namespace TennisBookings.Web.Configuration
{
    public class ClubConfiguration
    {
        public int PeakStartHour { get; set; }

        public int PeakEndHour { get; set; }

        public int OpenHour { get; set; }

        public int CloseHour { get; set; }

        public int WinterCourtStartHour { get; set; }

        public int WinterCourtEndHour { get; set; }

        public int[] WinterMonths { get; set; }
    }
}
