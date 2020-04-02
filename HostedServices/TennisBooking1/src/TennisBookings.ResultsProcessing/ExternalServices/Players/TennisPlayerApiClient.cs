using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TennisBookings.ResultsProcessing.ExternalServices.Players
{
    public class TennisPlayerApiClient : ITennisPlayerApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TennisPlayerApiClient> _logger;

        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public TennisPlayerApiClient(HttpClient httpClient, ILogger<TennisPlayerApiClient> logger, IOptions<TennisPlayerApiClientOptions> apiOptions)
        {
            _httpClient = httpClient;
            _logger = logger;

            var endpointAddress = apiOptions?.Value?.BaseAddress ?? string.Empty;

            if (string.IsNullOrEmpty(endpointAddress))
                throw new Exception("Invalid API base address");

            _httpClient.BaseAddress = new Uri(endpointAddress);
        }

        public async Task<TennisPlayer> GetPlayerAsync(int id, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Players/{id}");

            using var response = await _httpClient.SendAsync(request, cancellationToken);

            TennisPlayer player = null;

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();

                try
                {
                    player = await JsonSerializer.DeserializeAsync<TennisPlayer>(contentStream, jsonOptions);
                }
                catch
                {
                    _logger.LogWarning("Failed to deserialise the tennis player.");
                }
            }

            return player;
        }
    }
}
