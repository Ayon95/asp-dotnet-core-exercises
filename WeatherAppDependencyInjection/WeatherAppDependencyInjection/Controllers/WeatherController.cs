using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace WeatherAppDependencyInjection.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View(_weatherService.GetWeatherDetails());
        }
        [Route("/weather/{cityCode?}")]
        public IActionResult Details(string cityCode)
        {
            if (string.IsNullOrEmpty(cityCode))
            {
                return View();
            }
            return View(_weatherService.GetWeatherByCityCode(cityCode));
        }
    }
}
