using Backend.Models.Dto;
using Backend.Models.WeatherApi;

namespace Backend.Services.Interfaces;

public interface IWeatherService
{
    public Task<WeatherResponseDto> GetWeatherApiDataAsync();
}