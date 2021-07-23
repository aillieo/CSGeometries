using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries.Sample
{
    public class Intersections2DPointCircle : MonoBehaviour
    {
        public Vector2 p0;
        public Vector2 p1;

        public Vector2 center;
        public float radius;

        void OnDrawGizmos()
        {
            Gizmos.DrawLine(p0.ToVector3(), p1.ToVector3());
            Gizmos.DrawWireSphere(center.ToVector3(), radius);

            Color backup = Gizmos.color;
            Gizmos.color = Color.red;

            foreach (var p in Intersections2D.LineCircle(p0, p1, center, radius))
            {
                Gizmos.DrawSphere(p.ToVector3(), 2f);
            }

            Gizmos.color = backup;
        }
    }

}
