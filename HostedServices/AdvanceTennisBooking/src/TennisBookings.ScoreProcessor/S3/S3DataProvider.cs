using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TennisBookings.ScoreProcessor.S3
{
    public class S3DataProvider : IS3DataProvider
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly AwsServicesConfiguration _awsServicesConfiguration;
        private readonly ILogger<S3DataProvider> _logger;

        public S3DataProvider(IAmazonS3 amazonS3, IOptions<AwsServicesConfiguration> awsServicesConfiguration, ILogger<S3DataProvider> logger)
        {
            _amazonS3 = amazonS3;
            _awsServicesConfiguration = awsServicesConfiguration.Value;
            _logger = logger;
        }

        public async Task<Stream> GetStreamAsync(string objectKey, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = _awsServicesConfiguration.ScoresBucketName,
                    Key = objectKey
                };

                using var response = await _amazonS3.GetObjectAsync(request, cancellationToken);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    var stream = new MemoryStream();

                    await using var responseStream = response.ResponseStream;

                    responseStream.CopyTo(stream);

                    stream.Position = 0;

                    return stream;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occurred while reading the S3 object into the memory stream.");
            }

            return Stream.Null;
        }
    }
}
