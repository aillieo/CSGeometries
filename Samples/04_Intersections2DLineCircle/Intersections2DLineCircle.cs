using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries.Sample
{
    public class Intersections2DLineCircle : SampleSceneBase
    {
        public Transform p0;
        public Transform p1;

        public Transform center;
        public float radius;

        void OnDrawGizmos()
        {
            GeomDrawer2D.DrawCircle(Color.gray, center.ToVector2(), radius);
            GeomDrawer2D.DrawLine(Color.white, p0.ToVector2(), p1.ToVector2());

            foreach (var p in Intersections2D.LineCircle(p0.ToVector2(), p1.ToVector2(), center.ToVector2(), radius))
            {
                GeomDrawer2D.DrawPoint(Color.red, p);
            }
        }
    }

}
