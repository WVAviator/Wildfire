using UnityEngine;

namespace Wildfire
{
    public static class CompassDirection
    {
        public static string GetClosestDirection(float compassDirection)
        {
            string result = "N";
            float lowestDifference = 361;

            if (Mathf.Abs(360 - compassDirection) < lowestDifference) { result = "N"; lowestDifference = Mathf.Abs(360 - compassDirection); }
            if (Mathf.Abs(337.5f - compassDirection) < lowestDifference) { result = "NNW"; lowestDifference = Mathf.Abs(337.5f - compassDirection); }
            if (Mathf.Abs(315 - compassDirection) < lowestDifference) { result = "NW"; lowestDifference = Mathf.Abs(315 - compassDirection); }
            if (Mathf.Abs(292.5f - compassDirection) < lowestDifference) { result = "WNW"; lowestDifference = Mathf.Abs(292.5f - compassDirection); }
            if (Mathf.Abs(270 - compassDirection) < lowestDifference) { result = "W"; lowestDifference = Mathf.Abs(270 - compassDirection); }
            if (Mathf.Abs(247.5f - compassDirection) < lowestDifference) { result = "WSW"; lowestDifference = Mathf.Abs(247.5f - compassDirection); }
            if (Mathf.Abs(225 - compassDirection) < lowestDifference) { result = "SW"; lowestDifference = Mathf.Abs(225 - compassDirection); }
            if (Mathf.Abs(202.5f - compassDirection) < lowestDifference) { result = "SSW"; lowestDifference = Mathf.Abs(202.5f - compassDirection); }
            if (Mathf.Abs(180 - compassDirection) < lowestDifference) { result = "S"; lowestDifference = Mathf.Abs(180 - compassDirection); }
            if (Mathf.Abs(157.5f - compassDirection) < lowestDifference) { result = "SSE"; lowestDifference = Mathf.Abs(157.5f - compassDirection); }
            if (Mathf.Abs(135 - compassDirection) < lowestDifference) { result = "SE"; lowestDifference = Mathf.Abs(135 - compassDirection); }
            if (Mathf.Abs(112.5f - compassDirection) < lowestDifference) { result = "ESE"; lowestDifference = Mathf.Abs(112.5f - compassDirection); }
            if (Mathf.Abs(90 - compassDirection) < lowestDifference) { result = "E"; lowestDifference = Mathf.Abs(90 - compassDirection); }
            if (Mathf.Abs(67.5f - compassDirection) < lowestDifference) { result = "ENE"; lowestDifference = Mathf.Abs(67.5f - compassDirection); }
            if (Mathf.Abs(45 - compassDirection) < lowestDifference) { result = "NE"; lowestDifference = Mathf.Abs(45 - compassDirection); }
            if (Mathf.Abs(22.5f - compassDirection) < lowestDifference) { result = "NNE"; lowestDifference = Mathf.Abs(22.5f - compassDirection); }
            if (Mathf.Abs(0 - compassDirection) < lowestDifference) { result = "N"; lowestDifference = Mathf.Abs(0 - compassDirection); }
            return result;
        }
    }
}