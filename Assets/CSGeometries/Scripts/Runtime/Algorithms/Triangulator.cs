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

            public IEnumerable<Triangle2D> GetTriangles()
            {
                return null;
            }
        }

        public static bool Triangulate(Polygon polygon, List<Triangle2D> triangles)
        {
            TriangulateContext context = new TriangulateContext();
            if (polygon is PolygonSimple simple)
            {
                Triangulate(simple, context);
            }
            else if (polygon is PolygonComposite composite)
            {
                Triangulate(composite, context);
            }
            else
            {
                throw new Exception();
            }

            if (context.success)
            {
                triangles.Clear();
                triangles.AddRange(context.GetTriangles());
                return true;
            }

            return false;
        }

        private static void Triangulate(PolygonSimple polygonSimple, TriangulateContext context)
        {

        }

        private static void Triangulate(PolygonComposite polygonComposite, TriangulateContext context)
        {

        }
    }
}
