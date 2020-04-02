using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS.Model;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using TennisBookings.ScoreProcessor.BackgroundServices;
using TennisBookings.ScoreProcessor.Sqs;
using Xunit;

namespace TennisBookings.ScoreProcessor.Tests
{
    public class QueueReadingServiceTests
    {
        [Fact]
        public async Task ShouldSwallowExceptions_AndCompleteWriter()
        {
            // Arrange

            var sqsChannel = new Mock<ISqsMessageChannel>();

            var sqsMessageQueue = new Mock<ISqsMessageQueue>();
            sqsMessageQueue.Setup(x => x.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("My exception"), TimeSpan.FromMilliseconds(50));
            
            using var sut = new QueueReadingService(NullLogger<QueueReadingService>.Instance, 
                sqsMessageQueue.Object, Options.Create(new AwsServicesConfiguration
                {
                    NewScoresQueueUrl = "https://www.example.com"
                }), sqsChannel.Object);

            // Act

            await sut.StartAsync(default);

            await Task.Delay(100);
            
            // Assert

            sqsChannel.Verify(x => x.CompleteWriter(It.Is<Exception>(e => e is object 
                                                    && e.Message == "My exception")), Times.Once);

            var cts = new CancellationTokenSource(0);
            await sut.StopAsync(cts.Token);
        }

        [Fact]
        public async Task ShouldStopWithoutException_WhenCancelled()
        {
            var sqsChannel = new SqsMessageChannel(NullLogger<SqsMessageChannel>.Instance);

            var sqsMessageQueue = new Mock<ISqsMessageQueue>();
            sqsMessageQueue.Setup(x => x.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ReceiveMessageResponse 
                { 
                    HttpStatusCode = System.Net.HttpStatusCode.OK, Messages = new List<Message>() 
                }, TimeSpan.FromMilliseconds(50));

            var sut = new QueueReadingService(NullLogger<QueueReadingService>.Instance, sqsMessageQueue.Object, Options.Create(new AwsServicesConfiguration { NewScoresQueueUrl = "https://www.example.com" }), sqsChannel);

            await sut.StartAsync(default);

            await Task.Delay(100);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));

            Func<Task> act = async () => { await sut.StopAsync(cts.Token); };

            await act.Should().NotThrowAsync();
        }
    }
}
