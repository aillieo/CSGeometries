using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries.Sample
{
    public class Relation2DPointCircle : MonoBehaviour
    {
        public Vector2 point;

        public Vector2 p0;
        public Vector2 p1;
        public Vector2 p2;

        void OnDrawGizmos()
        {
            Gizmos.DrawLine(p0.ToVector3(), p1.ToVector3());
            Gizmos.DrawLine(p1.ToVector3(), p2.ToVector3());
            Gizmos.DrawLine(p2.ToVector3(), p0.ToVector3());

            Circle2D circle = Circle2D.CreateWithThreePoints(p0, p1, p2);
            Gizmos.DrawWireSphere(circle.center.ToVector3(), circle.radius);

            Relation r = Relations2D.PointCircle(point, p0, p1, p2);
            Color backup = Gizmos.color;
            switch (r)
            {
                case Relation.Coincidence:
                    Gizmos.color = Color.blue;
                    break;
                case Relation.Internal:
                    Gizmos.color = Color.red;
                    break;
                case Relation.External:
                    Gizmos.color = Color.green;
                    break;
            }

            Gizmos.DrawSphere(point.ToVector3(), 2f);

            Gizmos.color = backup;
        }
    }

}
