using System.Text.Json.Serialization;

namespace Backend.Models.Dto;

public class DayDto
{
    [JsonPropertyName("maxtemp_c")]
    public double MaxTempC { get; set; }
    
    [JsonPropertyName("mintemp_c")]
    public double MinTempC { get; set; }
    
    [JsonPropertyName("avgtemp_c")]
    public double AvgTempC { get; set; }
    
    [JsonPropertyName("maxwind_kph")]
    public double MaxWindKph { get; set; }
    
    [JsonPropertyName("avghumidity")]
    public int AvgHumidity { get; set; }
    
    [JsonPropertyName("daily_chance_of_rain")]
    public int DailyChanceOfRain { get; set; }
    
    [JsonPropertyName("condition")]
    public required ConditionDto Condition { get; set; }
}