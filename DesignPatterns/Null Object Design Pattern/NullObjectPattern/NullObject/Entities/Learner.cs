using System.Diagnostics.CodeAnalysis;

namespace NullObject.Entities
{
    public class Learner : ILearner
    {
        public Learner(int id, string userName, int coursesCompleted)
        {
            Id = id;
            UserName = userName;
            CoursesCompleted = coursesCompleted;
        }

        public int Id { get; private set; }

        public string UserName { get; private set; }

        public int CoursesCompleted { get; private set; }
    }
}