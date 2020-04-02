using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;

namespace TennisBookings.ScoreProcessor.Sqs
{
    public sealed class SqsMessageDeleter : ISqsMessageDeleter
    {
        private readonly IAmazonSQS _amazonSqs;
        private readonly string _queueUrl;

        public SqsMessageDeleter(IAmazonSQS amazonSqs, IOptions<AwsServicesConfiguration> options)
        {
            _amazonSqs = amazonSqs;
            _queueUrl = options.Value.NewScoresQueueUrl;
        }

        public async Task DeleteMessageAsync(Message message)
        {
            var sqsDeleteRequest = new DeleteMessageRequest
            {
                ReceiptHandle = message.ReceiptHandle,
                QueueUrl = _queueUrl
            };

            await _amazonSqs.DeleteMessageAsync(sqsDeleteRequest);
        }
    }
}
