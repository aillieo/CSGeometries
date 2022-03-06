using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public static class Distances2D
    {
        public static float PointTriangle(Vector2 point, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            throw new NotImplementedException();
        }

        public static float PointTriangle(Vector2 point, Triangle2D triangle)
        {
            return PointTriangle(point, triangle.p0, triangle.p1, triangle.p2);
        }

        public static float PointLine(Vector2 point, Vector2 p0, Vector2 p1)
        {
            float length = Vector2.SqrMagnitude(p1 - p0);
            float t = Vector2.Dot(point - p1, p0 - p1) / length;
            Vector2 projection = p1 + t * (p0 - p1);
            return Vector2.Distance(point, projection);
        }

        public static float PointSegment(Vector2 point, Vector2 p0, Vector2 p1)
        {
            float length = Vector2.SqrMagnitude(p1 - p0);
            float t = Vector2.Dot(point - p1, p0 - p1) / length;
            t = Mathf.Clamp01(t);
            Vector2 projection = p1 + t * (p0 - p1);
            return Vector2.Distance(point, projection);
        }
    }
}
