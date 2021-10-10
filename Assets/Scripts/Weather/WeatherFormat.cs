using UnityEngine;

namespace Wildfire
{
    public static class WeatherFormat
    {
        public static string HumidityDisplay(float humidity)
        {
            return Mathf.RoundToInt(humidity * 100) + "%";
        }

        public static string TemperatureDisplay(int temp)
        {
            return temp + "° F";
        }

        public static string WindDirectionDisplay(int dir)
        {
            return dir + "° " + CompassDirection.GetClosestDirection(dir);
        }

        public static string WindSpeedDisplay(int wSpeed)
        {
            return wSpeed + " mph";
        }
    }
}