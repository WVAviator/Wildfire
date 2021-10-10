using System.Collections.Generic;
using UnityEngine;

namespace Wildfire
{
    public static class UnitNameGenerator
    {
        static readonly List<string> ExistingNames = new List<string>();
        public static string GenerateUnitName(string unitType)
        {
            string result = GetRandomName(unitType);
            while (ExistingNames.Contains(result))
            {
                result = GetRandomName(unitType);
            }
            ExistingNames.Add(result);
            return result;

        }

        static string GetRandomName(string unitType)
        {
            return unitType + " " + GetRandomPhonetic();
        }
        static string GetRandomPhonetic()
        {
            return GetPhoneticAlphabet(Random.Range(0, 26));
        }

        static string GetPhoneticAlphabet(int a)
        {

            if (a == 0) return "Alpha";
            if (a == 1) return "Bravo";
            if (a == 2) return "Charlie";
            if (a == 3) return "Delta";
            if (a == 4) return "Echo";
            if (a == 5) return "Foxtrot";
            if (a == 6) return "Golf";
            if (a == 7) return "Hotel";
            if (a == 8) return "Indigo";
            if (a == 9) return "Juliet";
            if (a == 10) return "Kilo";
            if (a == 11) return "Lima";
            if (a == 12) return "Mike";
            if (a == 13) return "November";
            if (a == 14) return "Oscar";
            if (a == 15) return "Papa";
            if (a == 16) return "Quebec";
            if (a == 17) return "Romeo";
            if (a == 18) return "Sierra";
            if (a == 19) return "Tango";
            if (a == 20) return "Uniform";
            if (a == 21) return "Victor";
            if (a == 22) return "Whiskey";
            if (a == 23) return "X-Ray";
            if (a == 24) return "Yankee";
            if (a == 25) return "Zulu";

            return "Exotic";
        }
    }
}