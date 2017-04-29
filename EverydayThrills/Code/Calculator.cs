using System;

namespace EverydayThrills.Code
{
    public static class Calculator
    {
        public static int GetPercentage(float max, float value)
        {
            return (int)((value * 100) / max);
        }

        public static float GetValueFromPercentage(float max, float percent)
        {
            return (max * percent) / 100;
        }

        public static float RadianToDegree(float angle)
        {
            return (float)(angle * (180.0 / Math.PI));
        }

        public static float DegreeToRadian(float angle)
        {
            return (float)(Math.PI * angle / 180.0);
        }
    }
}
