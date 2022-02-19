using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries.Sample
{
    [ExecuteAlways]
    public class BresenhamsLineSample : SampleSceneBase
    {
        public Transform p0;
        public Transform p1;

        private void OnDrawGizmos()
        {
            Vector2 p0v2 = p0.ToVector2();
            Vector2 p1v2 = p1.ToVector2();

            GeomDrawer2D.DrawLine(Color.white, p0v2, p1v2);

            Vector2Int p0v2i = new Vector2Int(Mathf.RoundToInt(p0v2.x), Mathf.RoundToInt(p0v2.y));
            Vector2Int p1v2i = new Vector2Int(Mathf.RoundToInt(p1v2.x), Mathf.RoundToInt(p1v2.y));

            foreach (var p in BresenhamsLine.Intersect(p0v2i, p1v2i))
            {
                GeomDrawer2D.DrawSquarePoint(Color.red, p);
            }
        }
    }

}
