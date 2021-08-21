using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries.Sample
{
    [ExecuteAlways]
    public class DCELInsertSample : SampleSceneBase
    {
        public Transform p0;
        public Transform p1;
        public Transform p2;

        public Transform point;

        private void OnDrawGizmos()
        {
            Triangle2D triangle = new Triangle2D();
            triangle.p0 = p0.ToVector2();
            triangle.p1 = p1.ToVector2();
            triangle.p2 = p2.ToVector2();

            GeomDrawer2D.DrawPolygon(Color.red, triangle.Points);
            GeomDrawer2D.DrawPoint(Color.red, point.ToVector2());

            DoublyConnectedEdgeList2D dcel = new DoublyConnectedEdgeList2D(triangle);
            DCELHelper.Insert(dcel, point.ToVector2());

            GeomDrawer2D.DrawDCEL(Color.white, dcel);
        }
    }

}
