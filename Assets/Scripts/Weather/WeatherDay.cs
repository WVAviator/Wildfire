using System;

namespace Wildfire
{
    public class WeatherDay
    {
        public readonly float Humidity;

        public readonly int WindSpeed;

        public readonly int WindDirection;

        public readonly int Temperature;

        public WeatherDay()
        {
            GenerateRandomWeather();
        }

        public WeatherDay(float humidity, int windSpeed, int windDirection, int temperature)
        {
            Humidity = humidity;
            WindSpeed = windSpeed;
            WindDirection = windDirection;
            Temperature = temperature;
        }

        private static void GenerateRandomWeather()
        {
            throw new NotImplementedException();
        }

    }
}