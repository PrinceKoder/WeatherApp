namespace Backend.Models.WeatherApi;
using System.Text.Json.Serialization;

public class Astro
{
    [JsonPropertyName("sunrise")]
    public required string Sunrise { get; set; }
    
    [JsonPropertyName("sunset")]
    public required string Sunset { get; set; }
    
    [JsonPropertyName("moonrise")]
    public required string Moonrise { get; set; }
    
    [JsonPropertyName("moonset")]
    public required string Moonset { get; set; }
    
    [JsonPropertyName("moon_phase")]
    public required string MoonPhase { get; set; }
    
    [JsonPropertyName("moon_illumination")]
    public int MoonIllumination { get; set; }
    
    [JsonPropertyName("is_moon_up")]
    public int IsMoonUp { get; set; }
    
    [JsonPropertyName("is_sun_up")]
    public int IsSunUp { get; set; }
}