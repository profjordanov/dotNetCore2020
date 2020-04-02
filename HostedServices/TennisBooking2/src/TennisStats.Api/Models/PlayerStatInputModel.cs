namespace TennisStats.Api.Models
{
    public class PlayerStatInputModel
    {
        public int PlayerId { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
    }
}
