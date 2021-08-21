using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AillieoUtils.Geometries.DoublyConnectedEdgeList2D;

namespace AillieoUtils.Geometries
{
    public static class DCELHelper
    {
        public static bool Insert(DoublyConnectedEdgeList2D dcel, Vector2 vert)
        {
            foreach (var face in dcel.faces)
            {
                DCELHalfEdge edge = face.edge;
                Relation r = Relations2D.PointTriangle(vert, edge.origin.position, edge.next.origin.position, edge.next.next.origin.position);
                switch (r)
                {
                    case Relation.Coincidence:
                        int index = 0;
                        while (index < 3)
                        {
                            if (Relations2D.PointSegment(vert, edge.origin.position, edge.pair.origin.position) == Relation.Internal)
                            {
                                return InternalSplit(dcel, vert, edge);
                            }
                            edge = edge.next;
                            index++;
                        }
                        break;
                    case Relation.Internal:
                        return InternalInsert(dcel, vert, face);
                }
            }

            return false;
        }

        public static bool SwapEdge(DoublyConnectedEdgeList2D dcel, Vector2 edgePosition)
        {
            throw new NotImplementedException();
        }

        internal static bool InternalSwapEdge(DoublyConnectedEdgeList2D dcel, DCELHalfEdge edge)
        {
            throw new NotImplementedException();
        }

        internal static bool InternalInsert(DoublyConnectedEdgeList2D dcel, Vector2 vert, DCELFace face)
        {
            // 需要新加1个顶点
            // 3组edge
            // 1个face分裂为3个face
            DCELVertex vert0 = new DCELVertex();
            DCELVertex vert1 = new DCELVertex();
            DCELVertex vert2 = new DCELVertex();
            dcel.vertices.Add(vert0);
            dcel.vertices.Add(vert1);
            dcel.vertices.Add(vert2);
            DCELVertex fv0d = new DCELVertex();
            DCELVertex fv1d = new DCELVertex();
            DCELVertex fv2d = new DCELVertex();
            dcel.vertices.Add(fv0d);
            dcel.vertices.Add(fv1d);
            dcel.vertices.Add(fv2d);
            DCELHalfEdge edge0 = new DCELHalfEdge();
            DCELHalfEdge edge0p = new DCELHalfEdge();
            DCELHalfEdge edge1 = new DCELHalfEdge();
            DCELHalfEdge edge1p = new DCELHalfEdge();
            DCELHalfEdge edge2 = new DCELHalfEdge();
            DCELHalfEdge edge2p = new DCELHalfEdge();
            dcel.edges.Add(edge0);
            dcel.edges.Add(edge0p);
            dcel.edges.Add(edge1);
            dcel.edges.Add(edge1p);
            dcel.edges.Add(edge2);
            dcel.edges.Add(edge2p);
            DCELFace face0 = new DCELFace();
            DCELFace face1 = new DCELFace();
            dcel.faces.Add(face0);
            dcel.faces.Add(face1);

            // 涉及到的旧元素
            DCELHalfEdge fe0 = face.edge;
            DCELHalfEdge fe1 = fe0.next;
            DCELHalfEdge fe2 = fe1.next;
            DCELVertex fv0 = fe0.origin;
            DCELVertex fv1 = fe1.origin;
            DCELVertex fv2 = fe2.origin;

            // 关联一下
            vert0.position = vert;
            vert1.position = vert;
            vert2.position = vert;
            fv0d.position = fv0.position;
            fv1d.position = fv1.position;
            fv2d.position = fv2.position;

            vert0.incidentEdge = edge0;
            vert1.incidentEdge = edge1;
            vert2.incidentEdge = edge2;
            fv0d.incidentEdge = edge0p;
            fv1d.incidentEdge = edge1p;
            fv2d.incidentEdge = edge2p;

            edge0.origin = vert0;
            edge1.origin = vert1;
            edge2.origin = vert2;
            edge0p.origin = fv0d;
            edge1p.origin = fv1d;
            edge2p.origin = fv2d;

            fe0.incidentFace = face0;
            fe1.incidentFace = face1;
            edge0.incidentFace = face0;
            edge1.incidentFace = face1;
            edge2.incidentFace = face;
            edge0p.incidentFace = face;
            edge1p.incidentFace = face0;
            edge2p.incidentFace = face1;

            edge0.pair = edge0p;
            edge0p.pair = edge0;
            edge1.pair = edge1p;
            edge1p.pair = edge1;
            edge2.pair = edge2p;
            edge2p.pair = edge2;

            edge0.next = fe0;
            fe0.next = edge1p;
            edge1p.next = edge0;
            fe0.previous = edge0;
            edge1p.previous = fe0;
            edge0.previous = edge1p;

            edge1.next = fe1;
            fe1.next = edge2p;
            edge2p.next = edge1;
            fe1.previous = edge1;
            edge2p.previous = fe1;
            edge1.previous = edge2p;

            edge2.next = fe2;
            fe2.next = edge0p;
            edge0p.next = edge2;
            fe2.previous = edge2;
            edge0p.previous = fe2;
            edge2.previous = edge0p;

            return true;
        }

        private static bool InternalSplit(DoublyConnectedEdgeList2D dcel, Vector2 vert, DCELHalfEdge edge)
        {
            // 需要新加1个顶点
            // 1个edge分裂为2个edge
            // 1个face分裂为2个face

            throw new NotImplementedException();
        }

    }
}
