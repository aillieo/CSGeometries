using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries.Sample
{
    [ExecuteAlways]
    public class ConvexHull2DSample : MonoBehaviour
    {
        public Vector2[] points;

        private void OnEnable()
        {
            if (points == null || points.Length == 0)
            {
                int count = 10;
                points = new Vector2[count];
                for (int i = 0; i < count; ++i)
                {
                    points[i] = new Vector2(Random.Range(-20f, 20f), Random.Range(-20f, 20f));
                }
            }
        }

        private void OnDrawGizmos()
        {
            Color backup = Gizmos.color;

            Gizmos.color = Color.white;

            Polygon polygon = ConvexHull2D.ConvexHull(points);
            for (int i = 0, count = polygon.verts.Count; i < count; ++i)
            {
                Gizmos.DrawLine(
                    polygon.verts[i].ToVector3(),
                    polygon.verts[(i + 1) % count].ToVector3());
            }

            Gizmos.color = Color.red;

            foreach (var p in points)
            {
                Gizmos.DrawSphere(p.ToVector3(), 1f);
            }

            Gizmos.color = backup;
        }
    }

}
