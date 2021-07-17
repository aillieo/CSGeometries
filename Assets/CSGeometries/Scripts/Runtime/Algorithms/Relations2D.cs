using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static AillieoUtils.Geometries.Misc;

namespace AillieoUtils.Geometries
{
    public static class Relations2D
    {
        public static Relation PointTriangle(Vector2 point, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            int side0 = PointRaySide(ref point, ref p0, ref p1);
            if (side0 == 0 && InRange(ref point, ref p0, ref p1))
            {
                return Relation.Coincidence;
            }

            int side1 = PointRaySide(ref point, ref p1, ref p2);
            if (side1 == 0 && InRange(ref point, ref p1, ref p2))
            {
                return Relation.Coincidence;
            }
            if (side0 != side1)
            {
                return Relation.External;
            }

            int side2 = PointRaySide(ref point, ref p2, ref p0);
            if (side2 == 0 && InRange(ref point, ref p2, ref p0))
            {
                return Relation.Coincidence;
            }
            if (side0 != side2)
            {
                return Relation.External;
            }

            return Relation.Internal;
        }

        private static bool InRange(ref Vector2 point, ref Vector2 p0, ref Vector2 p1)
        {
            if (p0.x < p1.x)
            {
                return p0.x <= point.x && point.x <= p1.x;
            }
            if (p0.x > p1.x)
            {
                return p0.x >= point.x && point.x >= p1.x;
            }
            if (p0.y < p1.y)
            {
                return p0.y <= point.y && point.y <= p1.y;
            }
            if (p0.y > p1.y)
            {
                return p0.y >= point.y && point.y >= p1.y;
            }

            return false;
        }

        public static Relation PointTriangle(Vector2 point, Triangle2D triangle)
        {
            return PointTriangle(point, triangle.p0, triangle.p1, triangle.p2);
        }
    }
}

