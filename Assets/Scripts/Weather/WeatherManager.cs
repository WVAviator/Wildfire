using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Wildfire
{
    public class WeatherManager : MonoBehaviour
    {
        [Header("Wind")]
        [SerializeField] int prevailingWinds = 90;
        [SerializeField] int maximumPrevailingWindDifference = 150;
        [SerializeField] int maxWindSpeed = 15;
        [SerializeField] int minWindSpeed = 3;

        [Header("Humidity")]
        [SerializeField] float prevailingHumidity = 0.5f;
        [SerializeField] float maximumHumidityDifference = 0.4f;

        [Header("Temperature")]
        [SerializeField] int minimumTemperature = 77;
        [SerializeField] int maximumTemperature = 99;


        WeatherDay currentWeather;
        readonly List<WeatherDay> fiveDayForecast = new List<WeatherDay>();
        public static WeatherManager Instance;

        void Awake()
        {
            Instance = this;
            InitializeForecast();
            UpdateWeather();
        }

        private void Start()
        {
            GameManager.OnAdvanceNextTurn += UpdateWeather;
        }
        void InitializeForecast()
        {
            for (int i = 0; i < 5; i++)
            {
                fiveDayForecast.Add(RandomWeatherDay());
            }
        }
        public int GetCurrentWindDirection()
        {
            return currentWeather.WindDirection;
        }

        public float GetCurrentHumidity()
        {
            return currentWeather.Humidity;
        }

        public int GetCurrentWindSpeed()
        {
            return currentWeather.WindSpeed;
        }

        public float GetMaxWindSpeed()
        {
            return maxWindSpeed;
        }
        
        public static event Action<WeatherDay, List<WeatherDay>> OnWeatherUpdated = delegate { };

        void UpdateWeather()
        {
            //Move the day 0 forecast to today and add a new day to the forecast
            currentWeather = fiveDayForecast[0];
            fiveDayForecast.RemoveAt(0);
            fiveDayForecast.Add(RandomWeatherDay());

            //Call the weather updated event subscribed to by various UI elements and the wind zone
            OnWeatherUpdated?.Invoke(currentWeather, fiveDayForecast);
        }

        int GetRandomTemperature()
        {
            return Random.Range(minimumTemperature, maximumTemperature + 1);
        }
        int GetRandomWindDirection()
        {
            float rnd = Random.Range(0f, 1f);
            rnd *= rnd; //This ensures values closer to one are exponentially more rare. A value of 1 would be the max prevailing wind difference
            int unsignedAngle = Mathf.RoundToInt(rnd * maximumPrevailingWindDifference);
            int signedAngle = unsignedAngle;
            if (Random.Range(0, 2) == 1) signedAngle *= -1;
            int resultAngle = signedAngle + prevailingWinds;
            if (resultAngle > 360) resultAngle -= 360;
            else if (resultAngle < 0) resultAngle += 360;
            return resultAngle;
        }
        float GetRandomHumidity()
        {
            float rnd = Random.Range(0f, 1f);
            rnd *= rnd; //This ensure values closer to one are exponentially more rare. A value of one would be the maximum humidity difference
            float diff = maximumHumidityDifference * rnd * (Random.Range(0, 2) == 1 ? 1 : -1);
            float result = prevailingHumidity - diff;
            if (result < 0) result = 0;
            else if (result > 1) result = 1;
            return result;
        }
        int GetRandomWindSpeed()
        {
            int speed = 0;
            float rnd = Random.Range(0f, 1f);

            speed = (int)Mathf.Round(rnd * maxWindSpeed);
            if (speed < minWindSpeed) speed = minWindSpeed;
            return speed;
        }
        WeatherDay RandomWeatherDay()
        {
            return new WeatherDay(GetRandomHumidity(), GetRandomWindSpeed(), GetRandomWindDirection(), GetRandomTemperature());
        }


    }
}