namespace Backend.Models.Dto;
using System.Text.Json.Serialization;

public class WeatherResponseDto
{
    [JsonPropertyName("current")]
    public required CurrentWeatherDto Current { get; set; }
    
    [JsonPropertyName("forecast")]
    public required ForecastDto Forecast { get; set; }
}