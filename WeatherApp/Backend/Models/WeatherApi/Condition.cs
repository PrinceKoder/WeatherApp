namespace Backend.Models.WeatherApi;
using System.Text.Json.Serialization;

public class Condition
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
    
    [JsonPropertyName("icon")]
    public required string Icon { get; set; }
    
    [JsonPropertyName("code")]
    public int Code { get; set; }
}