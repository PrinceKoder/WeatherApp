namespace Backend.Models.WeatherApi;
using System.Text.Json.Serialization;

public class Forecast
{
    [JsonPropertyName("forecastday")]
    public required ForecastDay[] Forecastday { get; set; }
}