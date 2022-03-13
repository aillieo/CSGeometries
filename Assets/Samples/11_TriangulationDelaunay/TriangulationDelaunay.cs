using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AillieoUtils.Geometries.Sample
{
    [ExecuteAlways]
    public class TriangulationDelaunay : SampleSceneBase
    {
        public Transform[] pHull;

        private Color polygonColor = new Color(1, 1, 1, 1);
        private Color triangleColor = new Color(1, 1, 1, 0.2f);

        private List<Triangle2D> result = new List<Triangle2D>();

        private void OnDrawGizmos()
        {
            PolygonSimple hull = ConvexHull2D.ConvexHull(pHull.Select(t => t.ToVector2()));

            result.Clear();
            if (Triangulator.Triangulate(hull, TriangulationAlgorithm.Delaunay, result))
            {
                foreach (var tri in result)
                {
                    GeomDrawer2D.DrawPolygon(triangleColor, tri.Points);
                }
            }

            GeomDrawer2D.DrawPolygon(polygonColor, hull.verts);
        }
    }
}
