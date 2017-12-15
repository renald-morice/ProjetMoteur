using System;
using System.Security.AccessControl;
using Jitter.LinearMath;
using System.Numerics;
using OpenTK;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = System.Numerics.Vector3;

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
        
        public static float DegAngleFromQuaternion(float w)
        {
            var result = MathUtils.Rad2Deg((float) Math.Acos(w)) * 2;
			
            return result;
        }

        public static float RadAngleFromQuaternion(float w)
        {
            var result = ((float) Math.Acos(w)) * 2;
			
            return result;
        }

        public static Vector3 Rotate(Vector3 v, Quaternion q)
        {
            return Vector3.Transform(v, q);
        }
    }
}