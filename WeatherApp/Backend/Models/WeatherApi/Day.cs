namespace Backend.Models.WeatherApi;
using System.Text.Json.Serialization;

public class Day
{
    [JsonPropertyName("maxtemp_c")]
    public double MaxTempC { get; set; }
    
    [JsonPropertyName("maxtemp_f")]
    public double MaxTempF { get; set; }
    
    [JsonPropertyName("mintemp_c")]
    public double MinTempC { get; set; }
    
    [JsonPropertyName("mintemp_f")]
    public double MinTempF { get; set; }
    
    [JsonPropertyName("avgtemp_c")]
    public double AvgTempC { get; set; }
    
    [JsonPropertyName("avgtemp_f")]
    public double AvgTempF { get; set; }
    
    [JsonPropertyName("maxwind_mph")]
    public double MaxWindMph { get; set; }
    
    [JsonPropertyName("maxwind_kph")]
    public double MaxWindKph { get; set; }
    
    [JsonPropertyName("totalprecip_mm")]
    public double TotalprecipMm { get; set; }
    
    [JsonPropertyName("totalprecip_in")]
    public double TotalprecipIn { get; set; }
    
    [JsonPropertyName("totalsnow_cm")]
    public double TotalsnowCm { get; set; }
    
    [JsonPropertyName("avgvis_km")]
    public double AvgvisKm { get; set; }
    
    [JsonPropertyName("avgvis_miles")]
    public double AvgvisMiles { get; set; }
    
    [JsonPropertyName("avghumidity")]
    public int AvgHumidity { get; set; }
    
    [JsonPropertyName("daily_will_it_rain")]
    public int DailyWillItRain { get; set; }
    
    [JsonPropertyName("daily_chance_of_rain")]
    public int DailyChanceOfRain { get; set; }
    
    [JsonPropertyName("daily_will_it_snow")]
    public int DailyWillItSnow { get; set; }
    
    [JsonPropertyName("daily_chance_of_snow")]
    public int DailyChanceOfSnow { get; set; }
    
    [JsonPropertyName("condition")]
    public required Condition Condition { get; set; }
    
    [JsonPropertyName("uv")]
    public double Uv { get; set; }
}