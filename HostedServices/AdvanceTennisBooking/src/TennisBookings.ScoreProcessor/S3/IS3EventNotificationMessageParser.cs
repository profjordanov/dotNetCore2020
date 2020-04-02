using System.Collections.Generic;
using Amazon.SQS.Model;

namespace TennisBookings.ScoreProcessor.S3
{
    public interface IS3EventNotificationMessageParser
    {
        IReadOnlyCollection<string> Parse(Message message);
    }
}
