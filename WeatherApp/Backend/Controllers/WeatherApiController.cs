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
    public async Task<IActionResult> GetWeatherApiData()
    {
        var weatherApiData = await _weatherService.GetWeatherApiDataAsync();
        return Ok(weatherApiData);
    }
}