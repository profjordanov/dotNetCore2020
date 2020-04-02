using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS.Model;
using TennisBookings.ResultsProcessing;
using TennisBookings.ScoreProcessor.S3;
using TennisBookings.ScoreProcessor.Sqs;

namespace TennisBookings.ScoreProcessor.Processing
{
    public class AwsScoreProcessor : IScoreProcessor
    {
        private readonly IS3DataProvider _s3DataProvider;
        private readonly ISqsMessageDeleter _sqsMessageDeleter;
        private readonly IS3EventNotificationMessageParser _s3EventNotificationMessageParser;
        private readonly IResultProcessor _resultProcessor;

        public AwsScoreProcessor(IS3EventNotificationMessageParser s3EventNotificationMessageParser, IS3DataProvider s3DataProvider, ISqsMessageDeleter sqsMessageDeleter, IResultProcessor resultProcessor)
        {
            _s3DataProvider = s3DataProvider;
            _sqsMessageDeleter = sqsMessageDeleter;
            _resultProcessor = resultProcessor;
            _s3EventNotificationMessageParser = s3EventNotificationMessageParser;
        }

        public async Task ProcessScoresFromMessageAsync(Message message, CancellationToken cancellationToken = default)
        {
            var objectKeys = _s3EventNotificationMessageParser.Parse(message);

            foreach (var objectKey in objectKeys)
            {
                // future - SQS is at least once delivery, If the downstream processor is not idempotent
                // this processor should be. It could store hashes of the messages/files that have successfully processed.
                // Skipping those so there is not duplicate of scores and stats

                // load from S3 stream
                await using var dataStream = await _s3DataProvider.GetStreamAsync(objectKey, cancellationToken);

                // process the data
                await _resultProcessor.ProcessAsync(dataStream, cancellationToken);
            }

            // delete the SQS message
            // we don't pass cancellation as we want to ensure we delete the message if we have completely processed it, before we shutdown
            await _sqsMessageDeleter.DeleteMessageAsync(message);
        }
    }
}
