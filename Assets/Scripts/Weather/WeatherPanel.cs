using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wildfire
{
    public class WeatherPanel : MonoBehaviour
    {

        public Text temperatureText;
        public Text windSpeedText;
        public Text windDirectionText;
        public Text humidityText;

        void Start()
        {
            WeatherManager.OnWeatherUpdated += UpdateWeatherPanel;
        }

        void UpdateWeatherPanel(WeatherDay current, List<WeatherDay> forecast)
        {
            windSpeedText.text = WeatherFormat.WindSpeedDisplay(current.WindSpeed);
            windDirectionText.text = WeatherFormat.WindDirectionDisplay(current.WindDirection);
            humidityText.text = WeatherFormat.HumidityDisplay(current.Humidity);
            temperatureText.text = WeatherFormat.TemperatureDisplay(current.Temperature);
        }
    }
}