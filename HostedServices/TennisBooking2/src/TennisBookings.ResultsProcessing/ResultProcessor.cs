using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TennisBookings.ResultsProcessing.ExternalServices.Players;
using TennisBookings.ResultsProcessing.ExternalServices.Statistics;

namespace TennisBookings.ResultsProcessing
{
    public class ResultProcessor : IResultProcessor
    {
        private readonly ICsvResultParser _csvParser;
        private readonly ITennisPlayerApiClient _tennisPlayerApiClient;
        private readonly IStatisticsApiClient _statisticsApiClient;
        private readonly ILogger<ResultProcessor> _logger;

        public ResultProcessor(ICsvResultParser csvParser, ITennisPlayerApiClient tennisPlayerApiClient, IStatisticsApiClient statisticsApiClient, ILogger<ResultProcessor> logger)
        {
            _csvParser = csvParser;
            _tennisPlayerApiClient = tennisPlayerApiClient;
            _statisticsApiClient = statisticsApiClient;
            _logger = logger;
        }

        public async Task ProcessAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            var results = _csvParser.ParseResult(stream);

            _logger.LogInformation($"Processing {results.Count} results.");

            // Last chance to cancel. After this point, we allow all results to be calculated and posted, even if cancellation is signaled.
            // We don't want to send results for half of a file.
            cancellationToken.ThrowIfCancellationRequested(); 

            foreach (var result in results)
            {
                // NOTE: We could use a cache to avoid so many API calls in a real production system
                var playerOneTask = _tennisPlayerApiClient.GetPlayerAsync(result.PlayerOneId);
                var playerTwoTask = _tennisPlayerApiClient.GetPlayerAsync(result.PlayerTwoId);

                await Task.WhenAll(playerOneTask, playerTwoTask);                              

                var playerOne = playerOneTask.Result;
                var playerTwo = playerTwoTask.Result;

                if (playerOne is object && playerTwo is object)
                {
                    // calculate match result and player stats

                    await Task.Delay(500); // simulate more intestive processing time and actual generation of results and stats

                    var matchResult = new TennisMatchResult();
                    var playerOneStats = new PlayerStatistic();
                    var playerTwoStats = new PlayerStatistic();

                    // post winner and stats to API

                    var resultTask = _statisticsApiClient.PostResultAsync(matchResult);
                    var statPlayerOneTask = _statisticsApiClient.PostStatisticAsync(playerOneStats);
                    var statPlayerTwoTask = _statisticsApiClient.PostStatisticAsync(playerTwoStats);

                    await Task.WhenAll(resultTask, statPlayerOneTask, statPlayerTwoTask);
                }
            }
        }
    }
}
