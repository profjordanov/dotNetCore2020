using Facade.Entities;

namespace Facade
{
    public interface IWeatherFacade
    {
        WeatherFacadeResults GetTempInCity(string zipCode);
    }
}