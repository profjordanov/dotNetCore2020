namespace BallOfMud.Services
{
    public class BigClass
    {
        private int _i;


        public int GetValueA()
        {
            // some work
            return _i;
        }

        public string GetValueB()
        {
            // some work
            return "Ball of Mud";
        }

        public void SetValueI(int i)
        {
            _i = i;
        }

        public void IncrementI()
        {
            _i++;
        }

        public void DoSomething()
        {
            // decrement _i
            _i--;
        }

        public int AddToI(int addMe)
        {
            _i +=  addMe;
            return _i;
        }

        public void UnrelatedMethod()
        {
            // do something unrelated
        }

        public void AddedThisMethodLater()
        {
            // calls a db for a number
            int theNumber = 12;
            _i += theNumber;
        }
        
        public void DecrememntI()
        {
            _i--;
        }
    }
}