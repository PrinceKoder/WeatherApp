using System.Text.Json.Serialization;
namespace Backend.Models.WeatherApi;

public class WeatherApiResponse
{
    [JsonPropertyName("location")]
    public required Location Location { get; init; }
    
    [JsonPropertyName("current")]
    public required Current Current { get; init; }
    
    [JsonPropertyName("forecast")]
    public required Forecast Forecast { get; init; }
}