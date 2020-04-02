using System;

namespace TennisStats.Api.Models
{
    public class MatchResultInputModel
    {
        public DateTime Date { get; set; }
        public int PlayerOneId { get; set; }
        public int PlayerTwoId { get; set; }
        public int WinnerId { get; set; }
        public string ResultSummary { get; set; }
    }
}
