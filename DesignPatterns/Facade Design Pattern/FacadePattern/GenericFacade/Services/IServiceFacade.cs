using System;

namespace GenericFacade.Services
{
    public interface IServiceFacade
    {
        Tuple<int, double, string> CallFacade();
    }
}