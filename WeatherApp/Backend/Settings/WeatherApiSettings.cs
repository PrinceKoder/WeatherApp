namespace Backend.Settings;

public class WeatherApiSettings
{
    public required string Url { get; set; }
    public required string ApiKey { get; set; }
    public required string CityName { get; set; }
    public required int ForecastDays { get; set; }
    public required string Lang { get; set; }
}