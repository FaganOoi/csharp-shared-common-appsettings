using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SharedSettingsAcrossMultipleProjects.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<TestController> _logger;
        private readonly AppSettingModel _appSettingModel;
        private readonly SharedSettingModel _sharedSettingModel;

        public TestController(
            ILogger<TestController> logger,
            IOptionsMonitor<AppSettingModel> appSettingOptionsMonitor,
            IOptionsMonitor<SharedSettingModel> sharedSettingOptionsMonitor
        )
        {
            _logger = logger;
            _appSettingModel = appSettingOptionsMonitor.CurrentValue;
            _sharedSettingModel = sharedSettingOptionsMonitor.CurrentValue;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("get-settings-models")]
        public IActionResult GetAppSettingModel()
        {
            return Ok(new
            {
                AppSettingModel = _appSettingModel,
                SharedSettingModel = _sharedSettingModel
            });
        }
    }
}