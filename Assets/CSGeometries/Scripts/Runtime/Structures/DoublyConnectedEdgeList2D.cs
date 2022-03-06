using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public class DoublyConnectedEdgeList2D
    {
        public class DCELVertex
        {
            public Vector2 position;
            public DCELHalfEdge incidentEdge;
        }

        public class DCELHalfEdge
        {
            public DCELVertex origin;
            public DCELHalfEdge pair;
            public DCELHalfEdge previous;
            public DCELHalfEdge next;
            public DCELFace incidentFace;
        }

        public class DCELFace
        {
            public DCELHalfEdge edge;
        }

        public readonly List<DCELFace> faces = new List<DCELFace>();
        public readonly List<DCELHalfEdge> edges = new List<DCELHalfEdge>();
        public readonly List<DCELVertex> vertices = new List<DCELVertex>();

        public DoublyConnectedEdgeList2D()
        {
        }

        public DoublyConnectedEdgeList2D(Triangle2D triangle)
        {
            DCELFace face = new DCELFace();
            faces.Add(face);

            for (int i = 0; i < 3; ++i)
            {
                DCELVertex vert = new DCELVertex();
                DCELHalfEdge edge = new DCELHalfEdge();

                vertices.Add(vert);
                edges.Add(edge);

                vert.position = triangle[i];
                vert.incidentEdge = edge;
                edge.origin = vert;
                edge.incidentFace = face;
            }

            for (int i = 0; i < 3; ++ i)
            {
                edges[i].next = edges[(i + 1) % 3];
                edges[i].previous = edges[(i - 1 + 3)%3];
            }

            face.edge = edges[0];
        }
    }
}
