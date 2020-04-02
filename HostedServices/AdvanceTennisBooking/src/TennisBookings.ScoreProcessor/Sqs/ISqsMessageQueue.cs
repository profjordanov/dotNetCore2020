using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS.Model;

namespace TennisBookings.ScoreProcessor.Sqs
{
    public interface ISqsMessageQueue
    {
        Task<ReceiveMessageResponse> ReceiveMessageAsync(ReceiveMessageRequest request, CancellationToken cancellationToken = default);
    }
}
