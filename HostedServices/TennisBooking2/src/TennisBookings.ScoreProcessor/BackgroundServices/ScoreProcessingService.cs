using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TennisBookings.ScoreProcessor.Processing;
using TennisBookings.ScoreProcessor.Sqs;

namespace TennisBookings.ScoreProcessor.BackgroundServices
{
    public class ScoreProcessingService : BackgroundService
    {
        private readonly ILogger<ScoreProcessingService> _logger;
        private readonly ISqsMessageChannel _sqsMessageChannel;
        private readonly IServiceProvider _serviceProvider;
        public ScoreProcessingService(
            ILogger<ScoreProcessingService> logger,
            ISqsMessageChannel sqsMessageChannel,
            IServiceProvider serviceProvider
           )
        {
            _logger = logger;
            _sqsMessageChannel = sqsMessageChannel;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var message in _sqsMessageChannel.Reader.ReadAllAsync(stoppingToken))
            {        
                _logger.LogInformation("Read message to process from channel.");

                using var scope = _serviceProvider.CreateScope();

                var scoreProcessor = scope.ServiceProvider.GetRequiredService<IScoreProcessor>();

                await scoreProcessor.ProcessScoresFromMessageAsync(message, stoppingToken);
            }
        }
    }
}
