namespace NullObject.Entities
{
    public interface ILearner
    {
        int Id { get; }
        string UserName { get; }
        int CoursesCompleted { get; }
    }
}