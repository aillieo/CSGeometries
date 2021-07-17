using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils
{
    public struct AABB2D
    {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;

        public float Area()
        {
            return (xMax - xMin) * (yMax - yMin);
        }

        public static AABB2D Union(AABB2D lh, AABB2D rh)
        {
            return new AABB2D() {
                xMin = Mathf.Min(lh.xMin, rh.xMin),
                xMax = Mathf.Max(lh.xMax, rh.xMax),
                yMin = Mathf.Min(lh.yMin, rh.yMin),
                yMax = Mathf.Max(lh.yMax, rh.yMax),
            };
        }
    }
}
