using Nithin2003.Models;

namespace Nithin2003.Services.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecastModel>> Find();
    }
}
