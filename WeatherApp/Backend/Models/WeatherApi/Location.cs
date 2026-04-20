namespace Backend.Models.WeatherApi;
using System.Text.Json.Serialization;

public class Location
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("region")]
    public required string Region { get; set; }
    
    [JsonPropertyName("country")]
    public required string Country { get; set; }
    
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    
    [JsonPropertyName("lon")]
    public double Lon { get; set; }
    
    [JsonPropertyName("tz_id")]
    public required string TzId { get; set; }
    
    [JsonPropertyName("localtime_epoch")]
    public int LocaltimeEpoch { get; set; }
    
    [JsonPropertyName("localtime")]
    public required string Localtime { get; set; }
}