using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public static class Distances2D
    {
        public static float PointTriangle(Vector2 point, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            return 0;
        }

        public static float PointTriangle(Vector2 point, Triangle2D triangle)
        {
            return PointTriangle(point, triangle.p0, triangle.p1, triangle.p2);
        }
    }
}

