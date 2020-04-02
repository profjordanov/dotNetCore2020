using System.Threading.Tasks;
using Amazon.SQS.Model;

namespace TennisBookings.ScoreProcessor.Sqs
{
    public interface ISqsMessageDeleter
    {
        Task DeleteMessageAsync(Message message);
    }
}
