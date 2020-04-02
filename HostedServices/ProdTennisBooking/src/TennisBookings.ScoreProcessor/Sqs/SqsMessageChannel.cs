using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;

namespace TennisBookings.ScoreProcessor.Sqs
{
    public class SqsMessageChannel : ISqsMessageChannel
    {
        private readonly ILogger<SqsMessageChannel> _logger;
        private const int MaxMessagesInChannel = 250;

        private readonly Channel<Message> _channel;

        public SqsMessageChannel(ILogger<SqsMessageChannel> logger)
        {
            _logger = logger;
            _channel = Channel.CreateBounded<Message>(new BoundedChannelOptions(MaxMessagesInChannel)
            {
                SingleWriter = true, 
                SingleReader = true
            });
        }

        public ChannelReader<Message> Reader => _channel.Reader;
        
        public async Task WriteMessagesAsync(IList<Message> messages, CancellationToken cancellationToken = default)
        {
            var index = 0;

            while (index < messages.Count && await _channel.Writer.WaitToWriteAsync(cancellationToken))
            {
                while (index < messages.Count && _channel.Writer.TryWrite(messages[index]))
                {
                    Log.ChannelMessageWritten(_logger, messages[index].MessageId);

                    index++;
                }
            }
        }

        public void CompleteWriter(Exception ex = null) => _channel.Writer.Complete(ex);

        public bool TryCompleteWriter(Exception ex = null) => _channel.Writer.TryComplete(ex);

        internal static class EventIds
        {
            public static readonly EventId ChannelMessageWritten = new EventId(100, "ChannelMessageWritten");
        }

        private static class Log
        {
            private static readonly Action<ILogger, string, Exception> _channelMessageWritten = LoggerMessage.Define<string>(
                LogLevel.Debug,
                EventIds.ChannelMessageWritten,
                "Message with ID '{MessageId} was written to the channel.");

            public static void ChannelMessageWritten(ILogger logger, string messageId)
            {
                _channelMessageWritten(logger, messageId, null);
            }
        }
    }
}

