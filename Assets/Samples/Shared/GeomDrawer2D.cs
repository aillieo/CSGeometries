using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace AillieoUtils.Geometries.Sample
{
    public static class GeomDrawer2D
    {
        public static float pointSize = 0.15f;
        public static float lineWidth = 0.3f;

        public static void DrawPoint(Color color, Vector2 position)
        {
            Color back = Handles.color;
            Handles.color = color;
            DrawWorldPoint(position.ToVector3());
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
            float worldSize = HandleUtility.GetHandleSize(worldPosition) * pointSize;
            Handles.DrawSolidDisc(worldPosition, Vector3.up, worldSize);
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
            Vector2 dir = p1 - p0;
            Vector2 per = Vector2.Perpendicular(dir);
            per.Normalize();
            float halfLineWidth = lineWidth / 2;
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
    }
}
