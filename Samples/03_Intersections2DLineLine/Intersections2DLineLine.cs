using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries.Sample
{
    public class Intersections2DLineLine : MonoBehaviour
    {
        public Vector2 p0;
        public Vector2 p1;

        public Vector2 q0;
        public Vector2 q1;

        void OnDrawGizmos()
        {
            Gizmos.DrawLine(p0.ToVector3(), p1.ToVector3());
            Gizmos.DrawLine(q0.ToVector3(), q1.ToVector3());

            Color backup = Gizmos.color;
            Gizmos.color = Color.red;

            foreach (var p in Intersections2D.LineLine(p0, p1, q0, q1))
            {
                Gizmos.DrawSphere(p.ToVector3(), 2f);
            }

            Gizmos.color = backup;
        }
    }

}
