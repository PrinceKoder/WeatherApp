using System.Text.Json.Serialization;
namespace Backend.Models.WeatherApi;

public class WeatherApiResponse
{
    [JsonPropertyName("location")]
    public required Location Location { get; set; }
    
    [JsonPropertyName("current")]
    public required Current Current { get; set; }
    
    [JsonPropertyName("forecast")]
    public required Forecast Forecast { get; set; }
}