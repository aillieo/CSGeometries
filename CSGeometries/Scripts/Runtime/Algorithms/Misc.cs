using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AillieoUtils.Geometries
{
    internal static class Misc
    {
        internal static int PointRaySide(ref Vector2 point, ref Ray2D ray)
        {
            Vector2 rayPoint0 = ray.origin;
            Vector2 rayPoint1 = ray.GetPoint(1);
            return PointRaySide(ref point, ref rayPoint0, ref rayPoint1);
        }

        internal static int PointRaySide(ref Vector2 point, ref Vector2 rayPoint0, ref Vector2 rayPoint1)
        {
            return Clockwise(ref point, ref rayPoint0, ref rayPoint1);
        }

        internal static int Clockwise(ref Vector2 p0, ref Vector2 p1, ref Vector2 p2)
        {
            // | 1    1    1  |
            // | x0   x1   x2 |
            // | y0   y1   y2 |
            float determine = p0.x * p1.y + p2.x * p0.y + p1.x * p2.y - p0.x * p2.y - p2.x * p1.y - p1.x * p0.y;
            return Math.Sign(determine);
        }
    }
}
