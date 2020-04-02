using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Amazon.SQS.Model;

namespace TennisBookings.ScoreProcessor.Sqs
{
    public interface ISqsMessageChannel
    {
        ChannelReader<Message> Reader { get; }
        Task WriteMessagesAsync(IList<Message> messages, CancellationToken cancellationToken = default);
        void CompleteWriter(Exception ex = null);
        bool TryCompleteWriter(Exception ex = null);
    }
}
