namespace TennisBookings.Web.Services
{
    public class ProfanityChecker : IProfanityChecker
    {
        public bool ContainsProfanity(string input)
        {
            return string.IsNullOrEmpty(input) ? false : input.ToLowerInvariant().Contains("darn");
        }
    }
}
