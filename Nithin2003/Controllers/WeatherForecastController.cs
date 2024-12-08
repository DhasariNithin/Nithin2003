using Microsoft.AspNetCore.Mvc;
using Nithin2003.Services.Interfaces;



namespace Nithin2003.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly IWeatherForecastService _service;

        public WeatherForecastController(IWeatherForecastService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IActionResult> WeatherForecastIndex()
        {
            var products = await _service.Find();
            return View(products);
        }
    }
}


