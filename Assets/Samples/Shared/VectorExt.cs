using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AillieoUtils.GeometriesSample
{
    public static class VectorExt
    {
        public static Vector3 ToVector3(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0, vector2.y);
        }
    }
}
