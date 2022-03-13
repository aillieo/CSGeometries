using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AillieoUtils.Geometries.DoublyConnectedEdgeList2D;

namespace AillieoUtils.Geometries
{
    public static class TriangulatorDelaunay
    {
        internal static void Triangulate(TriangulationContext context)
        {
            if (!(context.root is PolygonSimple))
            {
                throw new Exception();
            }

            List<Vector2> verts = new List<Vector2>();
            verts.AddRange(context.root.verts);

            // 顶点索引数组
            List<int> vertIndex = new List<int>();
            vertIndex.AddRange(Enumerable.Range(0, verts.Count));
            // 待处理的三角形
            Queue<int> trianglesToProcess = new Queue<int>();
            // 处理完成的三角形
            List<int> triangles = new List<int>();

            // randomize:
            //int n = vertIndex.Count;
            //while (n-- > 1)
            //{
            //    int r = UnityEngine.Random.Range(0, n + 1);
            //    int value = vertIndex[r];
            //    vertIndex[r] = vertIndex[n];
            //    vertIndex[n] = value;
            //}

            // 取 super triangle
            float xMin = float.PositiveInfinity;
            float xMax = float.NegativeInfinity;
            float yMin = float.PositiveInfinity;
            float yMax = float.NegativeInfinity;
            foreach (var v in verts)
            {
                xMin = Mathf.Min(v.x, xMin);
                xMax = Mathf.Max(v.x, xMax);
                yMin = Mathf.Min(v.y, yMin);
                yMax = Mathf.Max(v.y, yMax);
            }

            float dx = xMax - xMin;
            float dy = yMax - yMin;
            float dr = Mathf.Max(dx, dy);

            Vector2 s0 = new Vector2(xMin + 0.5f * dx, yMax + dr * 10f);
            Vector2 s1 = new Vector2(xMin - dr * 10f, yMin - dr);
            Vector2 s2 = new Vector2(xMax + dr * 10f, yMin - dr);

            DoublyConnectedEdgeList2D dcel = new DoublyConnectedEdgeList2D(new Triangle2D(s0, s1, s2));

            foreach (var vi in vertIndex)
            {
                Vector2 vert = verts[vi];
                DCELHelper.Insert(dcel, vert);
                // todo 判断需要反转的边

            }

            Dictionary<Vector2, int> vertLookup = new Dictionary<Vector2, int>();
            int i = 0;
            foreach(var v in verts)
            {
                vertLookup[v] = i++;
            }

            foreach (var f in dcel.faces)
            {
                Vector2 v0 = f.edge.origin.position;
                Vector2 v1 = f.edge.next.origin.position;
                Vector2 v2 = f.edge.previous.origin.position;
                if (vertLookup.TryGetValue(v0, out int i0) && vertLookup.TryGetValue(v1, out int i1) && vertLookup.TryGetValue(v2, out int i2))
                {
                    triangles.Add(i0);
                    triangles.Add(i1);
                    triangles.Add(i2);
                }
            }

            context.verts = verts;
            context.triangles = triangles;
            context.success = true;
        }
    }
}
