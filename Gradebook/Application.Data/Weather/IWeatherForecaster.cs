namespace AppCore.Data
{
    public interface IWeatherForecaster
    {
        WeatherResult GetCurrentWeather();
    }
}