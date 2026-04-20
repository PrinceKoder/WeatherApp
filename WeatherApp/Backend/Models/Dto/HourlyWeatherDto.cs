using System.Text.Json.Serialization;

namespace Backend.Models.Dto;

public class HourlyWeatherDto
{
    [JsonPropertyName("time")] 
    public required string Time { get; set; }
    
    [JsonPropertyName("temp_c")]
    public double TempC { get; set; }
    
    [JsonPropertyName("feelslike_c")]
    public double FeelsLikeC { get; set; }
    
    [JsonPropertyName("condition")]
    public required ConditionDto Condition { get; set; }
    
    [JsonPropertyName("wind_kph")]
    public double WindKph { get; set; }
    
    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }
    
    [JsonPropertyName("cloud")]
    public int Cloud { get; set; }
}