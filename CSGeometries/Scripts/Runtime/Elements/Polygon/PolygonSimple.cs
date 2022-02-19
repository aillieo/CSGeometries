using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public class PolygonSimple : Polygon
    {
        // https://en.wikipedia.org/wiki/Shoelace_formula#The_polygon_area_formulas
        public override float Area()
        {
            float sum = 0;
            Vector2 last = default;
            bool first = true;
            foreach (var point in verts)
            {
                if (first)
                {
                    last = point;
                    first = false;
                    continue;
                }

                sum += (last.x * point.y) - (point.x * last.y);

                last = point;
            }

            return 0.5f * sum;
        }

        public override bool Clockwise()
        {
            throw new System.NotImplementedException();
        }

        public override bool Validate()
        {
            if (verts.Count < 3)
            {
                Debug.LogError("not enough verts");
                return false;
            }

            // todo 方向验证

            // todo 检查自相交
            return true;
        }
    }
}
