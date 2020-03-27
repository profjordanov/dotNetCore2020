namespace NullObject.Entities
{
    public class NullLearner : ILearner
    {
        public int Id { get; } = -1;

        public string UserName => "Just Browsing";

        public int CoursesCompleted { get; } = 0;

        
    }
}