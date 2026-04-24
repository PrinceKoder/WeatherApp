using Microsoft.Extensions.Options;

namespace Backend.Settings.Validators;

public class WeatherApiSettingsValidator : IValidateOptions<WeatherApiSettings>
{
    public ValidateOptionsResult Validate(string? name, WeatherApiSettings? weatherApiSettings)
    {
        if (weatherApiSettings == null)
        {
            return ValidateOptionsResult.Fail("Экземпляр настроек апи равен null");
        }

        if (weatherApiSettings.Url == string.Empty)
        {
            return ValidateOptionsResult.Fail("Экземпляр настроек не содержит ссылку на апи");
            
        } if (weatherApiSettings.ApiKey == string.Empty)
        {
            return ValidateOptionsResult.Fail("Экземпляр настроек не содержит ключа апи");
        }
        
        if (weatherApiSettings.CityName == string.Empty)
        {
            return ValidateOptionsResult.Fail("Экземпляр настроек не содержит название города для прогноза");
        }
        
        return ValidateOptionsResult.Success;
    }
}