using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries.Sample
{
    [ExecuteAlways]
    public class TriangulationEarClipping : SampleSceneBase
    {
        public Transform[] pHull;
        public Transform[] pHole0;
        public Transform[] pHole1;
        public Transform[] pHole2;

        private PolygonComposite hull = new PolygonComposite();
        private Polygon hole0 = new PolygonSimple();
        private PolygonComposite hole1 = new PolygonComposite();
        private Polygon hole2 = new PolygonSimple();

        private Color polygonColor = new Color(1, 1, 1, 1);
        private Color triangleColor = new Color(1, 1, 1, 0.2f);

        private List<Triangle2D> result = new List<Triangle2D>();

        protected override void OnEnable()
        {
            base.OnEnable();

            hull.holes.Clear();
            hull.holes.Add(hole0);
            hull.holes.Add(hole1);
            hole1.holes.Clear();
            hole1.holes.Add(hole2);
        }

        private void OnDrawGizmos()
        {
            hull.verts.Clear();
            foreach (var t in pHull)
            {
                hull.verts.Add(t.ToVector2());
            }

            hole0.verts.Clear();
            foreach (var t in pHole0)
            {
                hole0.verts.Add(t.ToVector2());
            }

            hole1.verts.Clear();
            foreach (var t in pHole1)
            {
                hole1.verts.Add(t.ToVector2());
            }

            hole2.verts.Clear();
            foreach (var t in pHole2)
            {
                hole2.verts.Add(t.ToVector2());
            }

            result.Clear();
            if (Triangulator.Triangulate(hull, TriangulationAlgorithm.EarClipping, result))
            {
                foreach (var tri in result)
                {
                    GeomDrawer2D.DrawPolygon(triangleColor, tri.Points);
                }
            }

            DrawPolygonRecursive(hull);
        }

        private void DrawPolygonRecursive(Polygon toDraw, int depth = 0)
        {
            GeomDrawer2D.DrawPolygon(polygonColor, toDraw.verts);
            if (toDraw is PolygonComposite composite)
            {
                depth++;
                foreach (var hole in composite.holes)
                {
                    DrawPolygonRecursive(hole, depth);
                }
            }
        }
    }

}
