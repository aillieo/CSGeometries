using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries.Sample
{
    [ExecuteAlways]
    public class DCELSample : SampleSceneBase
    {
        public Transform p0;
        public Transform p1;
        public Transform p2;

        private void OnDrawGizmos()
        {
            Triangle2D triangle = new Triangle2D();
            triangle.p0 = p0.ToVector2();
            triangle.p1 = p1.ToVector2();
            triangle.p2 = p2.ToVector2();

            GeomDrawer2D.DrawPolygon(Color.red, triangle.Points);

            DoublyConnectedEdgeList2D dcel = new DoublyConnectedEdgeList2D(triangle);
            GeomDrawer2D.DrawDCEL(Color.white, dcel);
        }
    }

}
