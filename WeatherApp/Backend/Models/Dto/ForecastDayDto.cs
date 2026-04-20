using System.Text.Json.Serialization;

namespace Backend.Models.Dto;

public class ForecastDayDto
{
    [JsonPropertyName("date")]
    public required string Date { get; set; }
   
    [JsonPropertyName("date_epoch")]
    public int DateEpoch { get; set; }
   
    [JsonPropertyName("day")]
    public required DayDto Day { get; set; }
  
    [JsonPropertyName("hour")]
    public required HourlyWeatherDto[] Hour { get; set; }
}