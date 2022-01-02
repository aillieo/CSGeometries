using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public static class Triangulator
    {
        private class TriangulateContext
        {
            public bool success = false;

            public Polygon root;
            public List<int> triangles;
            public TriangulationAlgorithm algorithm;

            public IEnumerable<Triangle2D> GetTriangles()
            {
                if (!(root is PolygonSimple))
                {
                    throw new NotImplementedException();
                }

                for (int i = 0; i < triangles.Count;)
                {
                    yield return new Triangle2D()
                    {
                        p0 = root.verts[triangles[i++]],
                        p1 = root.verts[triangles[i++]],
                        p2 = root.verts[triangles[i++]],
                    };
                }
            }
        }

        public static bool Triangulate(Polygon polygon, TriangulationAlgorithm algorithm, List<Triangle2D> triangles)
        {
            TriangulateContext context = new TriangulateContext();

            context.algorithm = algorithm;
            context.root = polygon;
            context.triangles = new List<int>();

            try
            {
                Triangulate(context);
            }
            catch (Exception e)
            {
                context.success = false;
                context.triangles.Clear();
                UnityEngine.Debug.LogError(e);
            }

            if (context.success)
            {
                triangles.Clear();
                triangles.AddRange(context.GetTriangles());
                return true;
            }

            return false;
        }

        private static void Triangulate(TriangulateContext context)
        {
            if (!context.root.Validate())
            {
                throw new Exception("invalid polygon");
            }

            if (context.algorithm != TriangulationAlgorithm.EarClipping)
            {
                throw new NotImplementedException();
            }

            if (!(context.root is PolygonSimple))
            {
                throw new NotImplementedException();
            }

            // ear clipping O(n^2)
            // ref :
            // https://www.geometrictools.com/Documentation/TriangulationByEarClipping.pdf
            // https://www.flipcode.com/archives/Efficient_Polygon_Triangulation.shtml

            var polygonVerts = context.root.verts;
            var triangles = context.triangles;

            int n = polygonVerts.Count;

            int[] V = new int[n];
            for (int v = 0; v < n; v++)
            {
                V[v] = v;
            }

            int nv = n; // 剩余顶点数
            int count = 2 * nv;
            for (int m = 0, v = nv - 1; nv > 2;)
            {
                if (count-- <= 0)
                {
                    // 没有可切的了？
                    throw new Exception("invalid polygon: can not find an ear to clip");
                }

                // 后移 u-v-w 确定一个三角形 超出重置为 0
                int u = v;
                if (u >= nv)
                {
                    u = 0;
                }

                v = u + 1;
                if (v >= nv)
                {
                    v = 0;
                }

                int w = v + 1;
                if (w >= nv)
                {
                    w = 0;
                }

                if (Snip(polygonVerts, u, v, w, nv, V))
                {
                    // 切耳： u-v-w 确定的三角形
                    triangles.Add(V[u]);
                    triangles.Add(V[v]);
                    triangles.Add(V[w]);
                    m++;

                    // 移除v 其余前移
                    for (int s = v, t = v + 1; t < nv; s++, t++)
                    {
                        V[s] = V[t];
                    }

                    nv--;
                    count = 2 * nv;
                }
            }

            context.success = true;
        }

        private static bool Snip(List<Vector2> verts, int u, int v, int w, int n, int[] V)
        {
            int p;
            Vector2 A = verts[V[u]];
            Vector2 B = verts[V[v]];
            Vector2 C = verts[V[w]];
            //if (Relations2D.PointsCollinear(ref A, ref B, ref C))
            if (Misc.Clockwise(ref A, ref B, ref C) <= 0)
            {
                return false;
            }
            Vector2 P;
            for (p = 0; p < n; p++)
            {
                if (p == u || p == v || p == w)
                {
                    continue;
                }
                
                P = verts[V[p]];
                if (Relations2D.PointInsideTriangle(ref P, ref A, ref B, ref C))
                {
                    return false;
                }
            }
            return true;

        }
    }
}
