using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    internal class TriangulationContext
    {
        public bool success = false;

        public Polygon root;
        public List<Vector2> verts;
        public List<int> triangles;
        public TriangulationAlgorithm algorithm;

        public IEnumerable<Triangle2D> GetTriangles()
        {
            if (!success)
            {
                throw new Exception();
            }

            for (int i = 0; i < triangles.Count;)
            {
                yield return new Triangle2D()
                {
                    p0 = verts[triangles[i++]],
                    p1 = verts[triangles[i++]],
                    p2 = verts[triangles[i++]],
                };
            }
        }
    }
}
