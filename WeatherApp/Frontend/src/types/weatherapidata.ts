export interface WeatherApiData {
  //location: Location,
  current: Current,
  forecast: Forecast
}

export interface Location {
    "name": string,
    "region": string,
    "country": string,
    "localtime": string
}
export interface Current extends GeneralWeatherData{
  "last_updated": string,
}
export interface GeneralWeatherData{
  "temp_c": number,
  "feelslike_c": number,
  "condition": Condition,
  "wind_kph": number,
  "humidity": number,
  "cloud": number
}
export interface Condition{
  "text": string,
  "icon": string,
  "code": number
}
export interface Forecast {
  "forecastday": Forecastday[]
}
export interface Forecastday {
  "date": string,
  "day": Day,
  "hour": Hour[]
}
export interface Day{
  "maxtemp_c": number,
  "mintemp_c": number,
  "avgtemp_c": number,
  "maxwind_kph": number,
  "avghumidity": number,
  "daily_chance_of_rain": number,
  "condition": Condition,
}
export interface Hour extends GeneralWeatherData{
  "time": string,
}
