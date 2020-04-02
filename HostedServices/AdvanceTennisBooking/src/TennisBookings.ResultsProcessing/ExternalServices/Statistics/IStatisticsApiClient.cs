using System.Threading;
using System.Threading.Tasks;

namespace TennisBookings.ResultsProcessing.ExternalServices.Statistics
{
    public interface IStatisticsApiClient
    {
        Task PostResultAsync(TennisMatchResult result, CancellationToken cancellationToken = default);
        Task PostStatisticAsync(PlayerStatistic statistic, CancellationToken cancellationToken = default);
    }
}
