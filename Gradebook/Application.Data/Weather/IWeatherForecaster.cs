namespace Application.Data
{
    public interface IWeatherForecaster
    {
        WeatherResult GetCurrentWeather();
    }
}