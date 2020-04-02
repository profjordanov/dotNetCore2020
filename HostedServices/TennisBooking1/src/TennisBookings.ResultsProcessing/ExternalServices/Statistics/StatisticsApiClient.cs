using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TennisBookings.ResultsProcessing.ExternalServices.Statistics
{
    public class StatisticsApiClient : IStatisticsApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StatisticsApiClient> _logger;

        public StatisticsApiClient(HttpClient httpClient, ILogger<StatisticsApiClient> logger, IOptions<StatisticsApiClientOptions> apiOptions)
        {
            _httpClient = httpClient;
            _logger = logger;

            var endpointAddress = apiOptions?.Value?.BaseAddress ?? string.Empty;

            if (string.IsNullOrEmpty(endpointAddress))
                throw new Exception("Invalid API base address");

            _httpClient.BaseAddress = new Uri(endpointAddress);
        }

        public async Task PostResultAsync(TennisMatchResult result, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "Results")
            {
                Content = new StringContent(JsonSerializer.Serialize(result), Encoding.UTF8, "application/json")
            };

            using var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to send match result.");
            }
        }

        public async Task PostStatisticAsync(PlayerStatistic statistic, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "Stats")
            {
                Content = new StringContent(JsonSerializer.Serialize(statistic), Encoding.UTF8, "application/json")
            };

            using var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to send player statistic.");
            }
        }
    }
}
