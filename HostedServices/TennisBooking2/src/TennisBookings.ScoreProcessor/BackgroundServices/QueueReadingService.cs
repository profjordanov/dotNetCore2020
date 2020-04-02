using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TennisBookings.ScoreProcessor.Logging;
using TennisBookings.ScoreProcessor.Sqs;

namespace TennisBookings.ScoreProcessor.BackgroundServices
{
    public class QueueReadingService : BackgroundService
    {
        private readonly ILogger<QueueReadingService> _logger;
        private readonly ISqsMessageQueue _sqsMessageQueue;
        private readonly ISqsMessageChannel _sqsMessageChannel;
        private readonly string _queueUrl;

        public long ReceivesAttempted { get; private set; }
        public long MessagesReceived { get; private set; }

        public QueueReadingService(
            ILogger<QueueReadingService> logger,
            ISqsMessageQueue sqsMessageQueue,
            IOptions<AwsServicesConfiguration> options,
            ISqsMessageChannel sqsMessageChannel)
        {
            _logger = logger;
            _sqsMessageQueue = sqsMessageQueue;
            _sqsMessageChannel = sqsMessageChannel;
            _queueUrl = options.Value.NewScoresQueueUrl;

            _logger.LogInformation($"Reading from {_queueUrl}");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Started queue reading service.");

            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = _queueUrl,
                MaxNumberOfMessages = 10,
                WaitTimeSeconds = 5
            };

            while (!stoppingToken.IsCancellationRequested)
            {
                ReceivesAttempted++;

                var receiveMessageResponse =
                    await _sqsMessageQueue.ReceiveMessageAsync(receiveMessageRequest, stoppingToken);

                if (receiveMessageResponse.HttpStatusCode == HttpStatusCode.OK &&
                    receiveMessageResponse.Messages.Any())
                {
                    MessagesReceived += receiveMessageResponse.Messages.Count;

                    _logger.LogInformation("Received {MessageCount} messages from the queue.",
                        receiveMessageResponse.Messages.Count);

                    await _sqsMessageChannel.WriteMessagesAsync(receiveMessageResponse.Messages, stoppingToken);
                }
                else if (receiveMessageResponse.HttpStatusCode == HttpStatusCode.OK)
                {
                    _logger.LogInformation("No messages received. Attempting receive again in 1 minute.",
                        receiveMessageResponse.Messages.Count);

                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
                else if (receiveMessageResponse.HttpStatusCode != HttpStatusCode.OK)
                {
                    _logger.LogError("Unsuccessful response from AWS SQS.");
                }
            }

            _sqsMessageChannel.TryCompleteWriter();
        }
    }
}
