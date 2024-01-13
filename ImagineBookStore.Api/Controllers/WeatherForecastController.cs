using ImagineBookStore.Api;
using ImagineBookStore.Core.Models;
using ImagineBookStore.Core.Models.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ImagineBookStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : BaseController
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            throw new Exception("Error occurred.", new NullReferenceException(nameof(result)));

            var res = new SuccessResult(result);
            return ProcessResponse(res);
        }

        [HttpPost]
        public IActionResult Post(TestModel model)
        {
            return ProcessResponse(new SuccessResult());
        }
    }
}