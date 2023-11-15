using MALAB3_1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MALAB3_1.Controllers
{
    [ApiController]
    [Route("SERVICE2/[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching", "/SERVICE2"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly RabbitMqService _rabbitMqService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, RabbitMqService rabbitMqService)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
        }

        [HttpGet(Name = "SERVICE2GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            // Получение сообщения из RabbitMQ
            var message = _rabbitMqService.ReceiveMessages();

            // Вывод сообщения в лог
            _logger.LogInformation($"Received message from RabbitMQ: {message}");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}