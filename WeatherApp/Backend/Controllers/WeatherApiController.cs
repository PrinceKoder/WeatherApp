using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherApiController : ControllerBase
{
    private readonly IConfiguration _config;
    
    public WeatherApiController(IConfiguration config)
    {
        _config = config;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetWeatherApiData()
    {
        var key = _config["WeatherApi:ApiKey"]
                  ?? throw new InvalidOperationException("API key не задан");

        var handler = new HttpClientHandler
        {
            AutomaticDecompression = System.Net.DecompressionMethods.All
        };
        using var client = new HttpClient(handler);
        //using var client = new HttpClient();
        //client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
        //client.DefaultRequestHeaders.Add("Accept", "application/json; charset=utf-8");
        //client.DefaultRequestHeaders.Add("accept-language", "ru,en-US;q=0.9,en;q=0.8,ko;q=0.7");
        //client.DefaultRequestHeaders.Add("Host", "api.weatherapi.com");
        //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br, zstd");
        //client.DefaultRequestHeaders.Add("Connection", "keep-alive");

        var url = $"https://api.weatherapi.com/v1/forecast.json" +
                  $"?key={key}&q=Moscow&days=4&lang=ru";
       
        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, "WeatherAPI вернул ошибку");

        var content = await response.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }
}