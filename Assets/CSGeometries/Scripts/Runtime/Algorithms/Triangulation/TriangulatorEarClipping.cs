using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public static class TriangulatorEarClipping
    {
        internal static void Triangulate(TriangulationContext context)
        {
            if (!context.root.Validate())
            {
                throw new Exception("invalid polygon");
            }

            if (context.algorithm != TriangulationAlgorithm.EarClipping)
            {
                throw new NotImplementedException();
            }

            // ear clipping O(n^2)
            // ref :
            // https://www.geometrictools.com/Documentation/TriangulationByEarClipping.pdf
            // https://www.flipcode.com/archives/Efficient_Polygon_Triangulation.shtml

            Queue<Polygon> polygonQueue = new Queue<Polygon>();
            List<int> triangles = context.triangles;
            context.verts = new List<Vector2>();

            polygonQueue.Enqueue(context.root);

            while (polygonQueue.Count > 0)
            {
                Polygon first = polygonQueue.Dequeue();

                List<Vector2> curPolygonVerts = GetCurPolygonVerts(first, polygonQueue);

                int n = curPolygonVerts.Count;
                int vn = context.verts.Count;

                List<int> V = new List<int>(n);
                for (int v = 0; v < n; v++)
                {
                    V.Add(v);
                }

                int count = 2 * V.Count;
                for (int v = V.Count - 1; V.Count > 2;)
                {
                    if (count-- <= 0)
                    {
                        // 没有可切的了？
                        throw new Exception("invalid polygon: can not find an ear to clip");
                    }

                    // 后移 u-v-w 确定一个三角形 超出重置为 0
                    int u = v;
                    if (u >= V.Count)
                    {
                        u = 0;
                    }

                    v = u + 1;
                    if (v >= V.Count)
                    {
                        v = 0;
                    }

                    int w = v + 1;
                    if (w >= V.Count)
                    {
                        w = 0;
                    }

                    if (Snip(curPolygonVerts, u, v, w, V))
                    {
                        // 切耳： u-v-w 确定的三角形
                        triangles.Add(vn + V[u]);
                        triangles.Add(vn + V[v]);
                        triangles.Add(vn + V[w]);

                        // 移除v 其余前移
                        V.RemoveAt(v);

                        count = 2 * V.Count;
                    }
                }

                context.verts.AddRange(curPolygonVerts);
            }

            context.success = true;
        }

        private static List<Vector2> GetCurPolygonVerts(Polygon hull, Queue<Polygon> queueToFill)
        {
            List<Vector2> verts = new List<Vector2>();
            verts.AddRange(hull.verts);

            if (hull is PolygonComposite composite)
            {
                int nh = composite.holes.Count;
                List<(int, float)> HS = new List<(int, float)>(nh);
                for (int h = 0; h < nh; ++h)
                {
                    HS.Add((h, composite.holes[h].verts.Max(v => v.x)));
                }

                HS.Sort((h1, h2) => h2.Item2.CompareTo(h1.Item2));

                foreach (var hole in HS)
                {
                    Polygon polygonHole = composite.holes[hole.Item1];
                    if (polygonHole is PolygonComposite holeComposite)
                    {
                        foreach (var innerPolygon in holeComposite.holes)
                        {
                            queueToFill.Enqueue(innerPolygon);
                        }
                    }
                    RemoveHole(verts, polygonHole);
                }
            }

            return verts;
        }

        private static void RemoveHole(List<Vector2> verts, Polygon hole)
        {
            int h = hole.verts.Count;
            List<int> H = new List<int>(h);
            for (int i = 0; i < h; ++i)
            {
                H.Add(i);
            }

            // x从大到小
            H.Sort((i, j) => hole.verts[j].x.CompareTo(hole.verts[i].x));
            int indexOfM = H[0];

            Vector2 right = Vector2.right;

            Vector2 M = hole.verts[indexOfM];

            Vector2 I = default;
            float xLeftMost = float.PositiveInfinity;
            int indexOfI = -1;
            int indexOfE0 = -1, indexOfE1 = -1;
            for (int j = 0, m = verts.Count; j < m; ++j)
            {
                // 从 holeVert 发出ray 监测与edge相交
                Vector2 edge0 = verts[j];
                Vector2 edge1 = verts[(j + 1) % m];

                if (edge0.x < M.x && edge1.x < M.x)
                {
                    continue;
                }

                float dy0 = edge0.y - M.y;
                float dy1 = edge1.y - M.y;

                if (dy0 * dy1 > 0)
                {
                    continue;
                }

                IEnumerable<Vector2> inter = Intersections2D.LineLine(M, M + Vector2.right, edge0, edge1);
                if (!inter.Any())
                {
                    throw new Exception();
                }

                // 交点I 可能有多个 选出来最左的
                Vector2 newI = inter.First();

                if (newI.x > xLeftMost)
                {
                    continue;
                }

                // 更新
                I = newI;
                xLeftMost = I.x;
                indexOfE0 = j;
                indexOfE1 = (j + 1) % m;

                if (I == edge0)
                {
                    indexOfI = indexOfE0;
                }
                else if (I == edge1)
                {
                    indexOfI = indexOfE1;
                }
                else
                {
                    indexOfI = -1;
                }
            }

            if (float.IsPositiveInfinity(xLeftMost))
            {
                throw new Exception();
            }

            if (indexOfI >= 0)
            {
                // 最理想情况 I 在hull上
                CreateBridge(hole.verts, indexOfM, verts, indexOfI);
                return;
            }

            Vector2 p0 = verts[indexOfE0];
            Vector2 p1 = verts[indexOfE1];
            int indexOfP = p0.x > p1.x ? indexOfE0 : indexOfE1;
            Vector2 P = verts[indexOfP];

            bool clockwise = Misc.Clockwise(ref M, ref I, ref P) > 0;

            // 检查 M I P 内是否有 reflex
            float minAngle = float.PositiveInfinity;
            int indexOfR = -1;

            for (int j = 0, m = verts.Count; j < m; ++j)
            {
                Vector2 point = verts[j];
                if (point == P)
                {
                    continue;
                }

                bool inside = false;

                if (clockwise)
                {
                    inside = Relations2D.PointInsideTriangle(ref point, ref M, ref I, ref P);
                }
                else
                {
                    inside = Relations2D.PointInsideTriangle(ref point, ref M, ref P, ref I);
                }

                if (inside)
                {
                    float angle = Vector2.Angle(Vector2.right, point - M);
                    if (angle < minAngle)
                    {
                        minAngle = angle;
                        indexOfR = j;
                    }
                }
            }

            if (float.IsPositiveInfinity(minAngle))
            {
                // 无 reflex
                CreateBridge(hole.verts, indexOfM, verts, indexOfP);
                return;
            }
            else
            {
                // 有 reflex
                CreateBridge(hole.verts, indexOfM, verts, indexOfR);
            }
        }

        private static void CreateBridge(List<Vector2> hole, int v, List<Vector2> hull, int u)
        {
            int m = hole.Count;
            hull.Insert(u, hull[u]);
            hull.Insert(u + 1, hole[v]);
            hull.InsertRange(u + 1, Enumerable.Range(0, m).Select(k => hole[(k + v + m) % m]));
        }

        private static bool Snip(List<Vector2> verts, int u, int v, int w, List<int> V)
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
            for (p = 0; p < V.Count; p++)
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
