using System.Reflection;
using MAlab3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MAlab3.Controllers
{
    [ApiController]
    [Route("SERVICE1/[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", 
            "Scorching", "/SERVICE1"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly RabbitMqService _rabbitMqService;
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, RabbitMqService rabbitMqService, 
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet(Name = "SERVICE1GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var correlationId = HttpContext.Request.Headers["X-Correlation-ID"].FirstOrDefault() 
                                ?? Guid.NewGuid().ToString();
            
            using (_logger.BeginScope("CorrelationID: {CorrelationId}", correlationId))
            {
                _logger.LogInformation("WeatherForecastController Get - this is a nice " +
                                       "message to test the logs", DateTime.UtcNow);
            }

            // Вызов метода из Service2 с использованием HttpClient
            var responseFromService2 = await GetWeatherDataFromService2();

            return Ok(responseFromService2);
        }

        private async Task<string> GetWeatherDataFromService2()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
        
                var response = await client.GetAsync("http://microservice2/SERVICE2/WeatherForecast");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return "Failed to get data from Service2.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get data from Service2: {ErrorMessage}", ex.Message);
                return "An error occurred while getting data from Service2.";
            }
        }


        [HttpPost("SendMessage")]
        public IActionResult SendMessage()
        {
            try
            {
                _rabbitMqService.Publish("test-queue", "This is a test message.");
                _logger.LogInformation("Это информационное сообщение.");
                return Ok("Message sent to RabbitMQ.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send message: {ErrorMessage}", ex.Message);
                return BadRequest($"Failed to send message: {ex.Message}");
            }
        }
    }
}
