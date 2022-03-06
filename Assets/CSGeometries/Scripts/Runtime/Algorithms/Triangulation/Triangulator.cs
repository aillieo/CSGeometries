using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public static class Triangulator
    {
        public static bool Triangulate(Polygon polygon, TriangulationAlgorithm algorithm, List<Triangle2D> triangles)
        {
            TriangulationContext context = new TriangulationContext();

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

        private static void Triangulate(TriangulationContext context)
        {
            switch (context.algorithm)
            {
                case TriangulationAlgorithm.EarClipping:
                    TriangulatorEarClipping.Triangulate(context);
                    break;
                case TriangulationAlgorithm.Delaunay:
                    TriangulatorDelaunay.Triangulate(context);
                    break;
                case TriangulationAlgorithm.ConstrainedDelaunay:
                    TriangulatorConstrainedDelaunay.Triangulate(context);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
