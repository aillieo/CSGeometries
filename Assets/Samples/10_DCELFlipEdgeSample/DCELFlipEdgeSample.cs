using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AillieoUtils.Geometries.DoublyConnectedEdgeList2D;

namespace AillieoUtils.Geometries.Sample
{
    [ExecuteAlways]
    public class DCELFlipEdgeSample : SampleSceneBase
    {
        public Transform p0;
        public Transform p1;
        public Transform p2;
        public Transform p3;

        private DoublyConnectedEdgeList2D dcel;
        private Dictionary<DCELVertex, Transform> vertMapping = new Dictionary<DCELVertex, Transform>();
        private float timer;

        private void OnDrawGizmos()
        {
            if (dcel == null)
            {
                CreateDCELFromQuad();
            }

            GeomDrawer2D.DrawDCEL(Color.white, dcel);

            timer += Time.deltaTime;
            if (timer > 2f)
            {
                timer -= 2f;

                if(DCELHelper.FlipEdge(dcel, 0.5f * (p0.ToVector2() + p2.ToVector2()), 1f)
                    || DCELHelper.FlipEdge(dcel, 0.5f * (p1.ToVector2() + p3.ToVector2()), 1f))
                {
                    vertMapping.Clear();
                }
            }

            if (vertMapping.Count == 0)
            {
                RecreateMapping();
            }

            foreach (var pair in vertMapping)
            {
                pair.Key.position = pair.Value.ToVector2();
            }
        }

        private void CreateDCELFromQuad()
        {
            dcel = new DoublyConnectedEdgeList2D();

            DCELFace face0 = new DCELFace();
            DCELFace face1 = new DCELFace();

            DCELHalfEdge edge0 = new DCELHalfEdge();
            DCELHalfEdge edge1 = new DCELHalfEdge();
            DCELHalfEdge edge2 = new DCELHalfEdge();
            DCELHalfEdge edge3 = new DCELHalfEdge();
            DCELHalfEdge edge4 = new DCELHalfEdge();
            DCELHalfEdge edge5 = new DCELHalfEdge();

            DCELVertex vert0 = new DCELVertex();
            DCELVertex vert1 = new DCELVertex();
            DCELVertex vert2 = new DCELVertex();
            DCELVertex vert3 = new DCELVertex();
            DCELVertex vert4 = new DCELVertex();
            DCELVertex vert5 = new DCELVertex();

            dcel.faces.Add(face0);
            dcel.faces.Add(face1);

            dcel.edges.Add(edge0);
            dcel.edges.Add(edge1);
            dcel.edges.Add(edge2);
            dcel.edges.Add(edge3);
            dcel.edges.Add(edge4);
            dcel.edges.Add(edge5);

            dcel.vertices.Add(vert0);
            dcel.vertices.Add(vert1);
            dcel.vertices.Add(vert2);
            dcel.vertices.Add(vert3);
            dcel.vertices.Add(vert4);
            dcel.vertices.Add(vert5);

            face0.edge = edge0;
            face1.edge = edge3;

            edge0.incidentFace = face0;
            edge1.incidentFace = face0;
            edge2.incidentFace = face0;
            edge3.incidentFace = face1;
            edge4.incidentFace = face1;
            edge5.incidentFace = face1;

            edge0.next = edge1;
            edge1.next = edge2;
            edge2.next = edge0;
            edge3.next = edge5;
            edge4.next = edge3;
            edge5.next = edge4;

            edge0.previous = edge2;
            edge1.previous = edge0;
            edge2.previous = edge1;
            edge3.previous = edge4;
            edge4.previous = edge5;
            edge5.previous = edge3;

            edge0.origin = vert0;
            edge1.origin = vert1;
            edge2.origin = vert2;
            edge3.origin = vert3;
            edge4.origin = vert4;
            edge5.origin = vert5;

            edge0.pair = edge3;
            edge3.pair = edge0;

            vert0.incidentEdge = edge0;
            vert1.incidentEdge = edge1;
            vert2.incidentEdge = edge2;
            vert3.incidentEdge = edge3;
            vert4.incidentEdge = edge4;
            vert5.incidentEdge = edge5;

            vert0.position = p2.ToVector2();
            vert1.position = p0.ToVector2();
            vert2.position = p1.ToVector2();
            vert3.position = p0.ToVector2();
            vert4.position = p3.ToVector2();
            vert5.position = p2.ToVector2();
        }

        private void RecreateMapping()
        {
            int idx = 0;

            foreach (var vert in dcel.vertices)
            {
                Transform best = null;
                float min = float.PositiveInfinity;
                float newDist = 0;

                // 0
                newDist = Vector2.SqrMagnitude(p0.ToVector2() - vert.position);
                if (newDist < min)
                {
                    min = newDist;
                    best = p0;
                }

                // 1
                newDist = Vector2.SqrMagnitude(p1.ToVector2() - vert.position);
                if (newDist < min)
                {
                    min = newDist;
                    best = p1;
                }

                // 2
                newDist = Vector2.SqrMagnitude(p2.ToVector2() - vert.position);
                if (newDist < min)
                {
                    min = newDist;
                    best = p2;
                }

                // 3
                newDist = Vector2.SqrMagnitude(p3.ToVector2() - vert.position);
                if (newDist < min)
                {
                    min = newDist;
                    best = p3;
                }

                vertMapping.Add(vert, best);
            }
        }
    }
}
