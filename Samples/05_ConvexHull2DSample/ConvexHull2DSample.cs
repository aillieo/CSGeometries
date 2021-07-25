using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AillieoUtils.Geometries.Sample
{
    [ExecuteAlways]
    public class ConvexHull2DSample : SampleSceneBase
    {
        public Transform[] points;

        private void OnDrawGizmos()
        {
            Polygon polygon = ConvexHull2D.ConvexHull(points.Select(t => t.ToVector2()));
            for (int i = 0, count = polygon.verts.Count; i < count; ++i)
            {
                GeomDrawer2D.DrawPolygon(
                    Color.white,
                    polygon.verts);
            }

            foreach (var p in points)
            {
                GeomDrawer2D.DrawPoint(Color.red, p.ToVector2());
            }
        }
    }

}
