namespace Backend.Models.WeatherApi;
using System.Text.Json.Serialization;

public class ForecastDay
{
   [JsonPropertyName("date")]
    public required string Date { get; set; }
   
   [JsonPropertyName("date_epoch")]
    public int DateEpoch { get; set; }
   
   [JsonPropertyName("day")]
    public required Day Day { get; set; }
   
   [JsonPropertyName("astro")]
    public required Astro Astro { get; set; }
   
   [JsonPropertyName("hour")]
    public required Hour[] Hour { get; set; }
}