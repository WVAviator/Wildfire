using System.Collections.Generic;
using UnityEngine;

namespace Wildfire
{
    public class WindZoneManager : MonoBehaviour
    {
        void Start()
        {
            WeatherManager.OnWeatherUpdated += UpdateWindZone;
        }
    
        void UpdateWindZone(WeatherDay currentWeather, List<WeatherDay> forecast)
        {
            WindZone windZone = GetComponent<WindZone>();

            windZone.transform.rotation = Quaternion.Euler(0, currentWeather.WindDirection, 0);
            windZone.windMain = (0.3f / 15) * (float)currentWeather.WindSpeed; //TODO: No hardcoding/ need to reference some kind of preferences
        }
    }
}