namespace TennisBookings.ScoreProcessor
{
    public class AwsServicesConfiguration
    {
        public string NewScoresQueueUrl { get; set; }
        public string ScoresBucketName { get; set; }
    }
}
