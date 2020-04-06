using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using MeterReaderWeb.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MeterReaderClient
{
  public class Worker : BackgroundService
  {
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _config;
    private readonly ReadingFactory _factory;
    private readonly ILoggerFactory _loggerFactory;
    private MeterReadingService.MeterReadingServiceClient _client = null;
    private string _token;
    private DateTime _expiration = DateTime.MinValue;

    public Worker(ILogger<Worker> logger,
    IConfiguration config,
    ReadingFactory factory,
    ILoggerFactory loggerFactory)
    {
      _logger = logger;
      _config = config;
      _factory = factory;
      _loggerFactory = loggerFactory;
    }

    protected MeterReadingService.MeterReadingServiceClient Client
    {
      get
      {
        if (_client == null)
        {
          var cert = new X509Certificate2(_config["Service:CertFileName"],
                                          _config["Service:CertPassword"]);

          var handler = new HttpClientHandler();
          handler.ClientCertificates.Add(cert);

          var client = new HttpClient(handler);

          var opt = new GrpcChannelOptions()
          {
            HttpClient = client,
            LoggerFactory = _loggerFactory
          };

          var channel = GrpcChannel.ForAddress(_config["Service:ServerUrl"], opt);
          _client = new MeterReadingService.MeterReadingServiceClient(channel);
        }

        return _client;
      }
    }

    protected bool NeedsLogin() => string.IsNullOrWhiteSpace(_token) || _expiration > DateTime.UtcNow;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      var counter = 0;

      var customerId = _config.GetValue<int>("Service:CustomerId");

      while (!stoppingToken.IsCancellationRequested)
      {
        counter++;

        //if (counter % 10 == 0)
        //{
        //  Console.WriteLine("Sending Diagnostics");
        //  var stream = Client.SendDiagnostics();
        //  for (var x = 0; x < 5; x++)
        //  {
        //    var reading = await _factory.Generate(customerId);
        //    await stream.RequestStream.WriteAsync(reading);
        //  }

        //  await stream.RequestStream.CompleteAsync();
        //}

        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        var pkt = new ReadingPacket()
        {
          Successful = ReadingStatus.Success,
          Notes = "This is our test"
        };

        for (var x = 0; x < 5; ++x)
        {
          pkt.Readings.Add(await _factory.Generate(customerId));
        }

        try
        {
          //if (!NeedsLogin() || await GenerateToken())
          //{
          //  var headers = new Metadata();
          //  headers.Add("Authorization", $"Bearer {_token}");

          var result = await Client.AddReadingAsync(pkt); //headers: headers);
            if (result.Success == ReadingStatus.Success)
          {
            _logger.LogInformation("Successfully sent");
          }
          else
          {
            _logger.LogInformation("Failed to send");
          }
          //}
        }
        catch (RpcException ex)
        {
          if (ex.StatusCode == StatusCode.OutOfRange)
          {
            _logger.LogError($"{ex.Trailers}");
          }
          _logger.LogError($"Exception Thrown: {ex}");
        }

        await Task.Delay(_config.GetValue<int>("Service:DelayInterval"), stoppingToken);
      }
    }

    private async Task<bool> GenerateToken()
    {
      var request = new TokenRequest()
      {
        Username = _config["Service:Username"],
        Password = _config["Service:Password"]
      };

      var response = await Client.CreateTokenAsync(request);

      if (response.Success)
      {
        _token = response.Token;
        _expiration = response.Expiration.ToDateTime();

        return true;
      }

      return false;
    }
  }
}
