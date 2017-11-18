using System;

namespace Engine.Utils
{
    public class MathUtils
    {
        public static float Deg2Rad(float angle)
        {
            return (float) (angle * Math.PI / 180.0);
        }

        public static float Rad2Deg(float angle)
        {
            return (float) (angle * 180.0f / Math.PI);
        }
    }
}