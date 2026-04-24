using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherApiController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherApiController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWeatherApiData(int mode, CancellationToken cancellationToken)
    {
        if (mode == 1)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken).ConfigureAwait(false);
        }
        else 
        if (mode == 2)
        {
            return Problem("Тестовая ошибка для отладки");
        }

        var weatherApiData = await _weatherService.GetWeatherApiDataAsync(cancellationToken);
        return Ok(weatherApiData);
    }
}