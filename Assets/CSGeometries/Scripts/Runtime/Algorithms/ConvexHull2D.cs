using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public static class ConvexHull2D
    {
        public static PolygonSimple ConvexHull(IEnumerable<Vector2> points)
        {
            // 这几个创建List的函数未来优化一下
            List<Vector2> sorted = points.OrderBy(p => p.x).ThenBy(p => p.y).ToList();
            Vector2 p1 = sorted.First();
            Vector2 p2 = sorted.Last();
            List<Vector2> top = new List<Vector2>();
            List<Vector2> bottom = new List<Vector2>();
            top.Add(p1);
            bottom.Add(p1);
            for (int i = 1, count = sorted.Count; i < count; i++)
            {
                Vector2 current = sorted[i];
                if (i == count - 1 || Misc.Clockwise(ref p1, ref current, ref p2) == 1)
                {
                    while (top.Count >= 2 && Misc.Clockwise(top[top.Count - 2], top[top.Count - 1], current) == -1)
                    {
                        top.RemoveAt(top.Count - 1);
                    }
                    top.Add(current);
                }
                if (i == count - 1 || Misc.Clockwise(ref p1, ref current, ref p2) == -1)
                {
                    while (bottom.Count >= 2 && Misc.Clockwise(bottom[bottom.Count - 2], bottom[bottom.Count - 1], current) == 1)
                    {
                        bottom.RemoveAt(bottom.Count - 1);
                    }
                    bottom.Add(current);
                }
            }

            PolygonSimple polygon = new PolygonSimple();
            for (int i = 0; i < top.Count; i++)
            {
                polygon.verts.Add(top[i]);
            }
            for (int i = bottom.Count - 2; i > 0; i--)
            {
                polygon.verts.Add(bottom[i]);
            }

            return polygon;
        }
    }
}
