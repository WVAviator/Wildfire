using UnityEngine;

namespace Wildfire
{
    public static class FireFactor
    {
        
        static int maxWindAngleDifference = 100;
        static float defaultPatrollingFactor = 0.50f;
        static int maxFireGrowthPerTurn = 5;
        static float minimumGrowthFactor = 0.10f;

        public static float GetFireSpreadFactor(HexTile from, HexTile to)
        {
            float difficultyFactor = DifficultyFactor();
            float windDirectionFactor = WindDirectionFactor(from, to);
            float windSpeedFactor = WindSpeedFactor();
            float flammability = FlammabilityFactor(to);
            float humidityFactor = HumidityFactor();
            float patrollingFactor = PatrollingFactor(to);
            float fireStrengthFactor = FireStrengthFactor(from);
            float retardantFactor = RetardantFactor(to);
            float firelineFactor = FirelineFactor(from, to);

            float spreadFactor =  difficultyFactor 
                                  * windDirectionFactor 
                                  * windSpeedFactor 
                                  * flammability 
                                  * humidityFactor 
                                  * patrollingFactor 
                                  * fireStrengthFactor
                                  * retardantFactor
                                  * firelineFactor;

            if (spreadFactor > 1) spreadFactor = 1;

            return spreadFactor;
        }
        
        public static float GetFireGrowthFactor(HexTile hex)
        {
            float difficultyFactor = DifficultyFactor();
            float windSpeedFactor = WindSpeedFactor(); //Wind stokes the fire
            float flammability = FlammabilityFactor(hex);
            float humidityFactor = HumidityFactor();
            float retardantFactor = RetardantFactor(hex);

            float growthFactor = difficultyFactor
                                 * windSpeedFactor
                                 * flammability
                                 * humidityFactor
                                 * retardantFactor;
        
            if (growthFactor > 1) growthFactor = 1;

            growthFactor -= minimumGrowthFactor; //Shift the value to the left so that negative numbers are possible
            return growthFactor;
        }

        public static float GetFireGrowthPerTurn(HexTile hex)
        {
            return GetFireGrowthFactor(hex) * maxFireGrowthPerTurn;
        }
        
        static float WindDirectionFactor(HexTile from, HexTile to)
        {
            //Wind direction factor - based on the difference in wind direction and tile direction
            //A difference in angle greater than the max wind angle difference is a zero percent chance to spread
            //0 difference in angle is 100%
            int headingBetweenTiles = HexTileMap.Instance.GetHeadingBetween(from, to);
            int windDirection = WeatherManager.Instance.GetCurrentWindDirection();

            //This math corrects for angles that cross over the 0/360 degree mark
            float angleDifference = Mathf.Min(Mathf.Abs(windDirection - headingBetweenTiles), 360 - Mathf.Abs(windDirection - headingBetweenTiles));

            float windDirectionFactor = Mathf.InverseLerp(maxWindAngleDifference, 0, angleDifference);
            return windDirectionFactor;
        }

        static float WindSpeedFactor()
        {
            float windSpeedFactor = Mathf.InverseLerp(0, WeatherManager.Instance.GetMaxWindSpeed(), WeatherManager.Instance.GetCurrentWindSpeed());
            return windSpeedFactor;
        }

        static float FlammabilityFactor(HexTile hex)
        {
            //Flammability - the base value for calculating spread chance based on the type of tile
            float flammability = WorldTile.FindWorldTile(hex).GetFlammability();
            return flammability;
        }

        static float HumidityFactor()
        {
            //Humidity Factor - the precent chance to spread is just the inverse of humidity
            float humidityFactor = 1 - WeatherManager.Instance.GetCurrentHumidity();
            return humidityFactor;
        }

        static float PatrollingFactor(HexTile hex)
        {
            //Patrolling Factor - reduce flammability by the default patrolling factor on a tile that a unit is currently on
            if (hex.GetComponentInChildren<Unit>() != null) return defaultPatrollingFactor;
            return 1f;
        }

        static float FireStrengthFactor(HexTile hex)
        {
            //Fire strength factor - stronger fires increase the chance of spread
            float fireStrengthFactor = Mathf.InverseLerp(0, 20, hex.GetComponentInChildren<Fire>().GetFireStrength());
            return fireStrengthFactor;
        }

        static float RetardantFactor(HexTile hex)
        {
            return 1f; //TODO: Code for retardant factor
        }

        static float FirelineFactor(HexTile from, HexTile to)
        {
            return 1f; //TODO: Code for fireline factor
        }

        static float DifficultyFactor()
        {
            return 3f; //TODO: Preferences option for player to adjust difficulty and code for how that affects spread and growth factors
        }
    }
}