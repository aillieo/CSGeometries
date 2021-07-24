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

        internal static int Clockwise(Vector2 p0, Vector2 p1, Vector2 p2)
        {
            return Clockwise(ref p0, ref p1, ref p2);
        }

        internal static float Determination3x3(ref float a00, ref float a01, ref float a02, ref float a10, ref float a11, ref float a12, ref float a20, ref float a21, ref float a22)
        {
            // | a00   a01   a02 |
            // | a10   a11   a12 |
            // | a20   a21   a22 |

            return a00 * a11 * a22
                + a01 * a12 * a20
                + a02 * a10 * a21
                - a02 * a11 * a20
                - a01 * a10 * a22
                - a00 * a12 * a21;
        }

        internal static float Determination3x3(float a00, float a01, float a02, float a10, float a11, float a12, float a20, float a21, float a22)
        {
            return Determination3x3(ref a00, ref a01, ref a02, ref a10, ref a11, ref a12, ref a20, ref a21, ref a22);
        }
    }
}
