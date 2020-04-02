using System;

namespace TennisBookings.ResultsProcessing
{
    public class TennisMatchRow
    {
        public DateTime MatchDate { get; set; }
        public int PlayerOneId { get; set; }
        public int PlayerTwoId { get; set; }

        public int SetOnePlayerOneScore { get; set; }
        public int SetOnePlayerTwoScore { get; set; }

        public int SetTwoPlayerOneScore { get; set; }
        public int SetTwoPlayerTwoScore { get; set; }

        public int SetThreePlayerOneScore { get; set; }
        public int SetThreePlayerTwoScore { get; set; }
    }
}
