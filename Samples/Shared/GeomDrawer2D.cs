using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace AillieoUtils.Geometries.Sample
{
    public static class GeomDrawer2D
    {
        public static float pointSize = 0.6f;
        public static float lineWidth = 0.3f;

        public static void DrawPoint(Color color, Vector2 position)
        {
            Color back = Handles.color;
            Handles.color = color;
            DrawWorldPoint(position.ToVector3());
            Handles.color = back;
        }

        public static void DrawSquarePoint(Color color, Vector2 position)
        {
            Color back = Handles.color;
            Handles.color = color;
            DrawWorldSquarePoint(position.ToVector3());
            Handles.color = back;
        }

        public static void DrawPoints(Color color, IEnumerable<Vector2> points)
        {
            Color back = Handles.color;
            Handles.color = color;
            foreach (var p in points)
            {
                DrawWorldPoint(p.ToVector3());
            }
            Handles.color = back;
        }

        private static void DrawWorldPoint(Vector3 worldPosition)
        {
            //float worldSize = HandleUtility.GetHandleSize(worldPosition) * pointSize;
            float worldSize = pointSize;
            Handles.DrawSolidDisc(worldPosition, Vector3.up, worldSize);
        }

        private static void DrawWorldSquarePoint(Vector3 worldPosition)
        {
            //float worldSize = HandleUtility.GetHandleSize(worldPosition) * pointSize;
            float worldSize = pointSize;
            Vector2 center = worldPosition.ToVector2();
            Vector2 lb = center - worldSize * 0.5f * Vector2.one;
            Vector2 q0 = lb;
            Vector2 q1 = lb + worldSize * Vector2.right;
            Vector2 q2 = lb + worldSize * Vector2.one;
            Vector2 q3 = lb + worldSize * Vector2.up;
            Handles.DrawSolidRectangleWithOutline(new Vector3[] {
                q0.ToVector3(),
                q1.ToVector3(),
                q2.ToVector3(),
                q3.ToVector3(),
            }, Handles.color, Handles.color);
        }

        public static void DrawLine(Color color, Vector2 p0, Vector2 p1)
        {
            Color back = Handles.color;
            Handles.color = color;
            DrawWideLine(p0, p1);
            Handles.color = back;
        }

        private static void DrawWideLine(Vector2 p0, Vector2 p1)
        {
            DrawWideLine(p0, p1, lineWidth);
        }

        private static void DrawWideLine(Vector2 p0, Vector2 p1, float width)
        {
            Vector2 dir = p1 - p0;
            Vector2 per = Vector2.Perpendicular(dir);
            per.Normalize();
            float halfLineWidth = width / 2;
            Vector2 q0 = p0 + per * halfLineWidth;
            Vector2 q1 = p1 + per * halfLineWidth;
            Vector2 q2 = p1 - per * halfLineWidth;
            Vector2 q3 = p0 - per * halfLineWidth;
            Handles.DrawSolidRectangleWithOutline(new Vector3[] {
                q0.ToVector3(),
                q1.ToVector3(),
                q2.ToVector3(),
                q3.ToVector3(),
            }, Handles.color, Handles.color);
        }

        public static void DrawPolygon(Color color, params Vector2[] vertices)
        {
            Color back = Handles.color;
            Handles.color = color;

            for (int i = 0, len = vertices.Length; i < len; ++i)
            {
                DrawWideLine(vertices[i], vertices[(i + 1) % len]);
            }

            Handles.color = back;
        }

        public static void DrawPolygon(Color color, IEnumerable<Vector2> vertices)
        {
            DrawPolygon(color, vertices.ToArray());
        }

        public static void DrawCircle(Color color, Vector2 center, float radius)
        {
            Color back = Handles.color;
            Handles.color = color;
            Handles.DrawSolidDisc(center.ToVector3(), Vector3.up, radius);
            Handles.color = back;
        }

        private static void DrawHalfEdge (Vector2 pFrom, Vector2 pTo, bool counterclockwise)
        {
            Vector2 dir = pTo - pFrom;
            float shrink = 0.05f;
            pTo = pTo - shrink * dir;
            pFrom = pFrom + shrink * dir;
            Vector2 per = Vector2.Perpendicular(dir);
            per.Normalize();
            if (!counterclockwise)
            {
                per = -per;
            }

            float edgeWidth = lineWidth * 0.5f;
            float offset = lineWidth * 0.5f;
            DrawWideLine(pFrom + per * offset, pTo + per * offset, edgeWidth);

            Vector2 headDir = -dir;
            if (counterclockwise)
            {
                headDir = headDir.Rotate(-30f);
            }
            else
            {
                headDir = headDir.Rotate(30f);
            }

            headDir.Normalize();
            float headSize = lineWidth * 3f;
            Vector2 pHead0 = pTo + per * offset;
            Vector2 pHead1 = pHead0 + headDir * headSize;
            DrawWideLine(pHead0, pHead1, edgeWidth);
        }

        private static void DrawDCEL(DoublyConnectedEdgeList2D decl)
        {
            foreach(var e in decl.edges)
            {
                Vector2 p0 = e.origin.position;
                Vector2 p1 = e.next.origin.position;
                DrawHalfEdge(p0, p1, true);
            }
        }

        public static void DrawDCEL(Color color, DoublyConnectedEdgeList2D decl)
        {
            Color back = Handles.color;
            Handles.color = color;
            DrawDCEL(decl);
            Handles.color = back;
        }
    }
}
