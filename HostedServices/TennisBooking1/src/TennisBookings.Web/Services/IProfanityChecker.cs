namespace TennisBookings.Web.Services
{
    public interface IProfanityChecker
    {
        bool ContainsProfanity(string input);
    }
}
