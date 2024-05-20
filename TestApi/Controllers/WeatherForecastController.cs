using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IConfiguration _configuration;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            var forecast = forecasts.First();

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            string query = "INSERT INTO Forecast (Date, TemperatureC) VALUES (@date, @temperature)";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@date", forecast.Date);
                    command.Parameters.AddWithValue("@temperature", forecast.TemperatureC);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occurred: " + ex.Message);
                }
            }

            return forecasts;
        }
    }
}
