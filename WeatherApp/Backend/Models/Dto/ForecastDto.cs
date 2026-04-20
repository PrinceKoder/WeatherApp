using System.Text.Json.Serialization;

namespace Backend.Models.Dto;

public class ForecastDto
{
    [JsonPropertyName("forecastday")]
    public required ForecastDayDto[] ForecastDay { get; set; }
}