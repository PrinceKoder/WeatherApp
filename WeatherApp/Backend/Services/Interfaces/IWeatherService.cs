using Backend.Models.Dto;

namespace Backend.Services.Interfaces;

public interface IWeatherService
{
    public Task<WeatherResponseDto> GetWeatherApiDataAsync(CancellationToken cancellationToken);
}