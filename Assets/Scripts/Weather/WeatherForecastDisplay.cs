using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wildfire
{
    public class WeatherForecastDisplay : MonoBehaviour
    {

        [Header("Current Displays")]
        [SerializeField] Text currentHumidityDisplay;
        [SerializeField] Text currentTemperatureDisplay;
        [SerializeField] Text currentWindDirectionDisplay;
        [SerializeField] Text currentWindSpeedDisplay;

        [Header("Forecasts")]
        [SerializeField] List<Text> humidityDisplays;
        [SerializeField] List<Text> temperatureDisplays;
        [SerializeField] List<Text> windDirectionDisplays;
        [SerializeField] List<Text> windSpeedDisplays;

        private void Start()
        {
            WeatherManager.OnWeatherUpdated += UpdateDisplay;
            Close();
        }

        void UpdateDisplay(WeatherDay currentWeather, List<WeatherDay> fiveDayForecast)
        {
            currentHumidityDisplay.text = WeatherFormat.HumidityDisplay(currentWeather.Humidity);
            currentTemperatureDisplay.text = WeatherFormat.TemperatureDisplay(currentWeather.WindSpeed);
            currentWindDirectionDisplay.text = WeatherFormat.WindDirectionDisplay(currentWeather.WindDirection);
            currentWindSpeedDisplay.text = WeatherFormat.WindSpeedDisplay(currentWeather.WindSpeed);

            for (int i = 0; i < 5; i++)
            {
                humidityDisplays[i].text = WeatherFormat.HumidityDisplay(fiveDayForecast[i].Humidity);
                temperatureDisplays[i].text = WeatherFormat.TemperatureDisplay(fiveDayForecast[i].Temperature);
                windDirectionDisplays[i].text = WeatherFormat.WindDirectionDisplay(fiveDayForecast[i].WindDirection);
                windSpeedDisplays[i].text = WeatherFormat.WindSpeedDisplay(fiveDayForecast[i].WindSpeed);
            }
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}