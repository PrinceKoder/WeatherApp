namespace Backend.Settings;

public class WeatherApiSettings
{
    public string Url { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string CityName { get; set; } = string.Empty;
    public int ForecastDays { get; set; } = 1;
    public string Lang { get; set; } = "ru";
}