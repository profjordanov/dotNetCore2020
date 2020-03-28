namespace BallOfMud.Services
{
    public class BigClassFacade
    {
        private readonly BigClass _bigClass;

        public BigClassFacade()
        {
            _bigClass = new BigClass();
            _bigClass.SetValueI(0);
        }

        public void IncreaseBy(int numberToAdd)
        {
            _bigClass.AddToI(numberToAdd);
        }

        public void DecreaseBy(int numberToSubtract)
        {
            _bigClass.AddToI(-numberToSubtract);
        }

        public int GetCurrentValue()
        {
            return _bigClass.GetValueA();
        } 
    }
}