using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS.Model;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using TennisBookings.ScoreProcessor.BackgroundServices;
using TennisBookings.ScoreProcessor.Processing;
using TennisBookings.ScoreProcessor.Sqs;
using Xunit;

namespace TennisBookings.ScoreProcessor.Tests
{
    public class ScoreProcessingServiceTests
    {
        [Fact]
        public async Task ShouldNotThrowInvalidOperationException_WhenStarted_AndServiceProviderContainsRequiredService()
        {
            var sc = new ServiceCollection();

            sc.AddTransient<IScoreProcessor, FakeScoreProcessor>();

            var sp = sc.BuildServiceProvider();

            var sut = new ScoreProcessingService(NullLogger<ScoreProcessingService>.Instance, Mock.Of<ISqsMessageChannel>(), sp, Mock.Of<IHostApplicationLifetime>());

            Func<Task> act = async () => { await sut.StartAsync(default); };

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task ShouldStopWithoutException_WhenCancelled()
        {
            var sc = new ServiceCollection();

            sc.AddTransient<IScoreProcessor, FakeScoreProcessor>();

            var sp = sc.BuildServiceProvider();
            
            var sut = new ScoreProcessingService(NullLogger<ScoreProcessingService>.Instance, Mock.Of<ISqsMessageChannel>(), sp, Mock.Of<IHostApplicationLifetime>());

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));

            await sut.StartAsync(default);

            Func<Task> act = async () => { await sut.StopAsync(cts.Token); };

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task ShouldCallScoreProcessor_ForEachMessageInChannel()
        {
            var scoreProcessor = new FakeScoreProcessor();

            var sc = new ServiceCollection();
            
            sc.AddTransient<IScoreProcessor>(s => scoreProcessor);

            var sp = sc.BuildServiceProvider();

            var sqsChannel = new SqsMessageChannel(NullLogger<SqsMessageChannel>.Instance);

            var messages = new Message[2];
            messages[0] = new Message();
            messages[1] = new Message();

            await sqsChannel.WriteMessagesAsync(messages);

            var sut = new ScoreProcessingService(NullLogger<ScoreProcessingService>.Instance, sqsChannel, sp, Mock.Of<IHostApplicationLifetime>());
            
            await sut.StartAsync(default);

            await sqsChannel.WriteMessagesAsync(messages);

            scoreProcessor.ExecutionCount.Should().Be(2);
        }

        [Fact]
        public async Task ShouldSwallowExceptions_AndStopApplication()
        {
            var sc = new ServiceCollection();

            var scoreProcessor = new Mock<IScoreProcessor>();
            scoreProcessor.Setup(x => x.ProcessScoresFromMessageAsync(It.IsAny<Message>(),  It.IsAny<CancellationToken>())).Throws<Exception>();

            sc.AddTransient(s => scoreProcessor);

            var sp = sc.BuildServiceProvider();

            var hostAppLifetime = new Mock<IHostApplicationLifetime>();

            var sqsChannel = new SqsMessageChannel(NullLogger<SqsMessageChannel>.Instance);

            var messages = new Message[2];
            messages[0] = new Message();
            messages[1] = new Message();

            await sqsChannel.WriteMessagesAsync(messages);

            var sut = new ScoreProcessingService(NullLogger<ScoreProcessingService>.Instance, sqsChannel, sp, hostAppLifetime.Object);

            await sut.StartAsync(default);

            hostAppLifetime.Verify(x => x.StopApplication(), Times.AtLeastOnce);
        }

        private class FakeScoreProcessor : IScoreProcessor
        {
            public int ExecutionCount;

            public Task ProcessScoresFromMessageAsync(Message message, CancellationToken cancellationToken = default)
            {
                ExecutionCount++;
                return Task.CompletedTask;
            }
        }
    }
}
