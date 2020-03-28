using System;
using BallOfMud.Services;

namespace BallOfMud
{
    class Program
    {
        static void Main(string[] args)
        {
            BigClassFacade bigClass = new BigClassFacade();
            
            bigClass.IncreaseBy(50);
            bigClass.DecreaseBy(20);
            
            Console.WriteLine($"Final Number : {bigClass.GetCurrentValue()}");
        }
    }
}