using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AillieoUtils.Geometries.Sample
{
    public static class VectorExt
    {
        public static Vector3 ToVector3(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0, vector2.y);
        }

        public static Vector2 ToVector2(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }

        public static Vector3 ToVector3(this Transform transform)
        {
            return transform.localPosition;
        }

        public static Vector2 ToVector2(this Transform transform)
        {
            return transform.ToVector3().ToVector2();
        }

        public static Vector2 Rotate(this Vector2 vector2, float angle)
        {
            // [[ cos -sin ]
            //  [ sin  cos ]]

            angle = angle * Mathf.Deg2Rad;
            return new Vector2(
                    (vector2.x * Mathf.Cos(angle)) - (vector2.y * Mathf.Sin(angle)),
                    (vector2.x * Mathf.Sin(angle)) + (vector2.y * Mathf.Cos(angle)));
        }
    }
}
