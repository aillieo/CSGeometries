using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries.Sample
{
    public class Intersections2DLineLine : SampleSceneBase
    {
        public Transform p0;
        public Transform p1;

        public Transform q0;
        public Transform q1;

        void OnDrawGizmos()
        {
            GeomDrawer2D.DrawLine(Color.white, p0.ToVector2(), p1.ToVector2());
            GeomDrawer2D.DrawLine(Color.white, q0.ToVector2(), q1.ToVector2());

            foreach (var p in Intersections2D.LineLine(p0.ToVector2(), p1.ToVector2(), q0.ToVector2(), q1.ToVector2()))
            {
                GeomDrawer2D.DrawPoint(Color.red, p);
            }
        }
    }

}
