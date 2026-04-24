using Microsoft.Extensions.Options;

namespace Backend.Settings.Validators;

public class WeatherApiSettingsValidator : IValidateOptions<WeatherApiSettings>
{
    public ValidateOptionsResult Validate(string? name, WeatherApiSettings? options)
    {
        if (options == null)
        {
            return ValidateOptionsResult.Fail("Экземпляр настроек апи равен null");
        }

        if (options.Url == string.Empty)
        {
            return ValidateOptionsResult.Fail("Экземпляр настроек не содержит ссылку на апи");
            
        } if (options.ApiKey == string.Empty)
        {
            return ValidateOptionsResult.Fail("Экземпляр настроек не содержит ключа апи");
        }
        
        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (options.CityName == string.Empty)
        {
            return ValidateOptionsResult.Fail("Экземпляр настроек не содержит название города для прогноза");
        }
        
        return ValidateOptionsResult.Success;
    }
}