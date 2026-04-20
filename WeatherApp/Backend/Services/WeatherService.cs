using System.Text.Json;
using Backend.Models.Dto;
using Backend.Models.WeatherApi;
using Backend.Services.Interfaces;

namespace Backend.Services;

public class WeatherService : IWeatherService
{
    private readonly IConfiguration _config;
    private readonly IHttpClientFactory _factory;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(IConfiguration config, IHttpClientFactory factory, ILogger<WeatherService> logger)
    {
        _config = config;
        _factory = factory;
        _logger = logger;
    }

    public async Task<WeatherResponseDto> GetWeatherApiDataAsync()
    {
        var key = _config["WeatherApi:ApiKey"]
                  ?? throw new InvalidOperationException("API key не задан");

        var client = _factory.CreateClient("weatherapi");

        var url = $"https://api.weatherapi.com/v1/forecast.json" +
                  $"?key={key}&q=Moscow&days=4&lang=ru";

        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogCritical("Ошибка [{ResponseStatusCode}] : {ResponseReasonPhrase}", response.StatusCode,
                response.ReasonPhrase);
            throw new HttpRequestException($"Сервер WeatherApi вернул ошибку: {response.StatusCode}");
        }

        var content = await response.Content.ReadAsStringAsync();
        try
        {
            var weatherApiResponse = JsonSerializer.Deserialize<WeatherApiResponse>(content);

            return MapToDto(weatherApiResponse!);
        }
        catch (JsonException ex)
        {
            _logger.LogCritical(ex, "Ошибка десериализации ответа: {Content}", content);
            throw new InvalidOperationException("Не удалось десериализовать ответ");
        }
        catch (Exception ex)
        {
            _logger.LogCritical("Не удалось сделать маппинг классов: {ExMessage}", ex.Message);
            throw new InvalidOperationException($"Не удалось сделать маппинг классов");
        }
    }

    private WeatherResponseDto MapToDto(WeatherApiResponse response)
    {
        ArgumentNullException.ThrowIfNull(response);

        return new WeatherResponseDto
        {
            Current = MapCurrent(response.Current),
            Forecast = new ForecastDto
            {
                ForecastDay = response.Forecast.Forecastday.Select(MapForecastDay)
                    .ToArray()
            }
        };
    }

    private CurrentWeatherDto MapCurrent(Current current)
    {
        return new CurrentWeatherDto
        {
            LastUpdated = current.LastUpdated,
            TempC = current.TempC,
            FeelsLikeC = current.FeelslikeC,
            Humidity = current.Humidity,
            WindKph = current.WindKph,
            Cloud = current.Cloud,
            Condition = MapCondition(current.Condition)
        };
    }

    private HourlyWeatherDto MapHour(Hour hour)
    {
        return new HourlyWeatherDto
        {
            Time = hour.Time,
            TempC = hour.TempC,
            FeelsLikeC = hour.FeelsLikeC,
            Humidity = hour.Humidity,
            WindKph = hour.WindKph,
            Cloud = hour.Cloud,
            Condition = MapCondition(hour.Condition)
        };
    }

    private ForecastDayDto MapForecastDay(ForecastDay forecastDay)
    {
        return new ForecastDayDto
        {
            Date = forecastDay.Date,
            Day = new DayDto
            {
                MaxTempC = forecastDay.Day.MaxTempC,
                MinTempC = forecastDay.Day.MinTempC,
                AvgTempC = forecastDay.Day.AvgTempC,
                MaxWindKph = forecastDay.Day.MaxWindKph,
                AvgHumidity = forecastDay.Day.AvgHumidity,
                DailyChanceOfRain = forecastDay.Day.DailyChanceOfRain,
                Condition = MapCondition(forecastDay.Day.Condition)
            },
            Hour = forecastDay.Hour.Select(MapHour).ToArray()
        };
    }

    private ConditionDto MapCondition(Condition condition)
    {
        return new ConditionDto
        {
            Text = condition.Text,
            Icon = condition.Icon
        };
    }
}