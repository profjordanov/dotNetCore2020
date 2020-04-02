using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS.Model;

namespace TennisBookings.ScoreProcessor.Processing
{
    public interface IScoreProcessor
    {
        Task ProcessScoresFromMessageAsync(Message message, CancellationToken cancellationToken = default);
    }
}
