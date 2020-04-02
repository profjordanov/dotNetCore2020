using System.Threading;
using System.Threading.Tasks;

namespace TennisBookings.ResultsProcessing.ExternalServices.Players
{
    public interface ITennisPlayerApiClient
    {
        Task<TennisPlayer> GetPlayerAsync(int id, CancellationToken cancellationToken = default);
    }
}
