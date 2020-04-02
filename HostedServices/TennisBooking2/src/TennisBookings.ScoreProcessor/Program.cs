using System;
using Amazon.S3;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TennisBookings.ResultsProcessing;
using TennisBookings.ScoreProcessor.BackgroundServices;
using TennisBookings.ScoreProcessor.Processing;
using TennisBookings.ScoreProcessor.S3;
using TennisBookings.ScoreProcessor.Sqs;

namespace TennisBookings.ScoreProcessor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)                
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<AwsServicesConfiguration>(hostContext.Configuration.GetSection("AWS"));

                    services.AddAWSService<IAmazonSQS>();
                    services.AddAWSService<IAmazonS3>();

                    var useLocalStack = hostContext.Configuration.GetValue<bool>("UseLocalStack");

                    if (hostContext.HostingEnvironment.IsDevelopment() && useLocalStack)
                    {
                        services.AddSingleton<IAmazonSQS>(sp =>
                        {
                            var s3Client = new AmazonSQSClient(new AmazonSQSConfig
                            {
                                ServiceURL = "http://localhost:4576"
                            });

                            return s3Client;
                        });
                        
                        services.AddSingleton<IAmazonS3>(sp =>
                        {
                            var s3Client = new AmazonS3Client(new AmazonS3Config
                            {
                                ServiceURL = "http://localhost:4572",
                                ForcePathStyle = true,
                            });

                            return s3Client;
                        });
                    }

                    services.AddTransient<IScoreProcessor, AwsScoreProcessor>();
                    services.AddSingleton<IS3EventNotificationMessageParser, S3EventNotificationMessageParser>();
                    services.AddSingleton<IS3DataProvider, S3DataProvider>();
                    services.AddTennisPlayerApiClient(options => options.BaseAddress = hostContext.Configuration.GetSection("ExternalServices:TennisPlayersApi")["Url"]);
                    services.AddStatisticsApiClient(options => options.BaseAddress = hostContext.Configuration.GetSection("ExternalServices:StatisticsApi")["Url"]);
                    services.AddResultProcessing();

                    services.AddSingleton<ISqsMessageChannel, SqsMessageChannel>();
                    services.AddSingleton<ISqsMessageDeleter, SqsMessageDeleter>();
                    services.AddSingleton<ISqsMessageQueue, SqsMessageQueue>();

                    services.AddHostedService<QueueReadingService>();
                    services.AddHostedService<ScoreProcessingService>();
                });
    }
}
