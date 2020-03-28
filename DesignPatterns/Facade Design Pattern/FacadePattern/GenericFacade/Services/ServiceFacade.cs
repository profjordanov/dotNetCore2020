using System;

namespace GenericFacade.Services
{
    public class ServiceFacade : IServiceFacade
    {
        readonly ServiceA _serviceA = new ServiceA();
        readonly ServiceB _serviceB = new ServiceB();
        readonly ServiceC _serviceC = new ServiceC();
        
        public Tuple<int, double, string> CallFacade()
        {
            int SAResult = _serviceA.Method2();
            string SBResult = _serviceB.Method2();
            double SCResult = _serviceC.Method1();
            
            return new Tuple<int, double, string>(SAResult, SCResult, SBResult);
        }
    }
}