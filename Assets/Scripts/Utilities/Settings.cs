using UnityEngine;

namespace Wildfire
{
    [CreateAssetMenu(menuName = "Game Settings", fileName = "Settings", order = 0)]
    public class Settings : ScriptableObject
    {
        [Header("Fire Settings")]
        
        [Header("Fire Strength")]
        [Tooltip("All newly spawned fires will start out at this value. Fires will burn this much fuel per turn.")]
        public float StartingFireStrength = 4;
        [Tooltip("This is the maximum strength a fire can reach. Fires will burn this much fuel per turn.")]
        public float MaximumFireStrength = 20;
        
        [Header("Fire Appearance")]
        [Tooltip("This is the number of ground fires that will be visible on a burning tile.")]
        public int BrushFireDensity = 10;

        [Header("Fire Spread and Growth")] 
        [Tooltip("This is the maximum angle a fire can spread from a burning tile. Increasing this will make fires more likely to spread in the opposite direction of the wind.")]
        public int MaximumWindAngleDifference = 100;
        [Tooltip("This is how much having a unit present on a tile reduces the chance of fire spreading to that tile.")]
        public float PatrollingFactor = 0.5f;
        [Tooltip("This is the maximum increase in fire strength allowed per turn.")]
        public float MaximumFireGrowthPerTurn = 5;
        [Tooltip("This is the percentage minimum increase in fire strength per turn - growth factors below this will actually reduce a fire's strength naturally.")]
        public float MinimumGrowthFactor = 0.1f;

        [Space(10)]
        [Header("Weather Settings")]
        
        [Header("Wind Settings")]
        [Tooltip("This is the maximum angle difference between the prevailing wind direction and actual wind direction allowed.")]
        public int MaximumPrevailingWindDifference = 150;
        [Tooltip("This is the maximum wind speed. Raising this will increase the range of possible wind speeds.")]
        public int MaximumWindSpeed = 15;
        [Tooltip("This is the minimum wind speed. If the wind is zero fire will not spread.")]
        public int MinimumWindSpeed = 3;

        [Header("Humidity")]
        [Tooltip("This is the maximum difference between the prevailing humidity and actual humidity.")]
        public float MaximumHumidityDifference = 0.4f;
        
        [Header("Temperature")]
        public int minimumTemperature = 77;
        public int maximumTemperature = 99;
        
        [Space(10)]
        [Header("Map Navigation Settings")]
        
        [Header("Zoom Settings")]
        public float minimumZoomDistance = 1.5f;
        public float maximumZoomDistance = 4.5f;
        public float zoomSensitivity = 1;

        [Header("Drag Settings")]
        public float horizontalMapClamp = 0;
        public float southMapClamp = 4;
        public float northMapClamp = -4;
        public float minimumDragDistance = 8;
        
        [Header("Camera Settings")]
        public int cameraAngle = 50;





    }
}