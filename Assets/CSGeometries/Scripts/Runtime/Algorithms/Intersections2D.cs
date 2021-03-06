using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public static class Intersections2D
    {
        public static IEnumerable<Vector2> LineLine(Vector2 p0, Vector2 p1, Vector2 q0, Vector2 q1)
        {
            float d = (p0.x - p1.x) * (q0.y - q1.y) - (p0.y - p1.y) * (q0.x - q1.x);
            if (d == 0)
            {
                yield break;
            }

            float d1 = p0.x * p1.y - p0.y * p1.x;
            float d2 = q0.x * q1.y - q0.y * q1.x;

            Vector2 s = new Vector2(
                (d1 * (q0.x - q1.x) - (p0.x - p1.x) * d2)/d,
                (d1 * (q0.y - q1.y) - (p0.y - p1.y) * d2)/d);
            yield return s;
        }

        public static IEnumerable<Vector2> LineCircle(Vector2 p0, Vector2 p1, Vector2 center, float radius)
        {
            IEnumerable<Vector2> results = LineCircle(p0 - center, p1- center, radius);
            foreach (var s in results)
            {
                yield return s + center;
            }
        }

        public static IEnumerable<Vector2> LineCircle(Vector2 p0, Vector2 p1, float radius)
        {
            float dx = p1.x - p0.x;
            float dy = p1.y - p0.y;
            float dr = Mathf.Sqrt(dx * dx + dy * dy);
            float dr2 = dr * dr;
            //
            //      | x0  x1 |
            //  d = |        |
            //      | y0  y1 |   
            // 
            float d = p0.x * p1.y - p1.x * p0.y;
            float det = radius * radius * dr2 - d * d;
            if (det < 0)
            {
                yield break;
            }
            else if (det == 0)
            {
                Vector2 s0 = new Vector2(
                    d * dy / dr2,
                    - d * dx / dr2);
                yield return s0;
            }
            else
            {
                float sqrtdet = Mathf.Sqrt(det);
                Vector2 s0 = new Vector2(
                    (d * dy - dx * sqrtdet) / dr2,
                    (-d * dx - dy * sqrtdet) / dr2);
                yield return s0;

                Vector2 s1 = new Vector2(
                    (d * dy + dx * sqrtdet) / dr2,
                    (-d * dx + dy * sqrtdet) / dr2);
                yield return s1;
            }
        }
    }
}
