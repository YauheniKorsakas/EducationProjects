using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.Web.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger) {
            _logger = logger;
        }

        [HttpGet("Get")]
        public ActionResult<IEnumerable<WeatherForecast>> Get() {
            var rng = new Random();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetData")]
        public async IAsyncEnumerable<string> GetData() {
            await foreach (var item in GetDataAsync()) {
                yield return item;
            }
        }

        private async IAsyncEnumerable<string> GetDataAsync() {
            foreach (var item in new string[] { "First", "Second" }) {
                await Task.Delay(1000);
                yield return item;
            }
        }
    }
}