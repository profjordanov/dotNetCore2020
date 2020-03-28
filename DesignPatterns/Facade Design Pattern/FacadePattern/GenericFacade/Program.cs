using System;
using GenericFacade.Services;

namespace GenericFacade
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceFacade facade = new ServiceFacade();

            Tuple<int, double, string> result = facade.CallFacade();
            
            Console.WriteLine(result.Item1 + " - " + result.Item2 + " - " + result.Item3);
        }
    }
}