using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries.Sample
{
    public class Relation2DPointCircle : SampleSceneBase
    {
        public Transform point;

        public Transform p0;
        public Transform p1;
        public Transform p2;

        void OnDrawGizmos()
        {
            Circle2D circle = Circle2D.CreateWithThreePoints(p0.ToVector2(), p1.ToVector2(), p2.ToVector2());
            GeomDrawer2D.DrawCircle(Color.gray, circle.center, circle.radius);

            GeomDrawer2D.DrawPolygon(Color.white, p0.ToVector2(), p1.ToVector2(), p2.ToVector2());

            Relation r = Relations2D.PointCircle(point.ToVector2(), p0.ToVector2(), p1.ToVector2(), p2.ToVector2());
            Color color = Color.white;
            switch (r)
            {
                case Relation.Coincidence:
                    color = Color.blue;
                    break;
                case Relation.Internal:
                    color = Color.red;
                    break;
                case Relation.External:
                    color = Color.green;
                    break;
            }

            GeomDrawer2D.DrawPoint(color, point.ToVector2());
        }
    }
}
