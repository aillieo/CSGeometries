using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static AillieoUtils.Geometries.Misc;

namespace AillieoUtils.Geometries
{
    public static class Relations2D
    {
        public static Relation PointTriangle(Vector2 point, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            int side0 = PointRaySide(ref point, ref p0, ref p1);
            if (side0 == 0 && InRange(ref point, ref p0, ref p1))
            {
                return Relation.Coincidence;
            }

            int side1 = PointRaySide(ref point, ref p1, ref p2);
            if (side1 == 0 && InRange(ref point, ref p1, ref p2))
            {
                return Relation.Coincidence;
            }
            if (side0 != side1)
            {
                return Relation.External;
            }

            int side2 = PointRaySide(ref point, ref p2, ref p0);
            if (side2 == 0 && InRange(ref point, ref p2, ref p0))
            {
                return Relation.Coincidence;
            }
            if (side0 != side2)
            {
                return Relation.External;
            }

            return Relation.Internal;
        }

        // 判断 point 是否在 p0和p1构成的AABB内
        private static bool InRange(ref Vector2 point, ref Vector2 p0, ref Vector2 p1)
        {
            if (p0.x < p1.x)
            {
                return p0.x <= point.x && point.x <= p1.x;
            }
            if (p0.x > p1.x)
            {
                return p0.x >= point.x && point.x >= p1.x;
            }
            if (p0.y < p1.y)
            {
                return p0.y <= point.y && point.y <= p1.y;
            }
            if (p0.y > p1.y)
            {
                return p0.y >= point.y && point.y >= p1.y;
            }

            return false;
        }

        public static Relation PointTriangle(Vector2 point, Triangle2D triangle)
        {
            return PointTriangle(point, triangle.p0, triangle.p1, triangle.p2);
        }

        public static Relation PointCircle(Vector2 point, Vector2 center, float radius)
        {
            float sqradius = radius * radius;
            float sqrdist = (point - center).sqrMagnitude;
            if (sqradius > sqrdist)
            {
                return Relation.Internal;
            }
            else if (sqradius < sqrdist)
            {
                return Relation.External;
            }
            else
            {
                return Relation.Coincidence;
            }
        }

        public static Relation PointCircle(Vector2 point, Vector2 circlep0, Vector2 circlep1, Vector2 circlep2)
        {
            // | cp0.x   cp0.y   cp0.x^2 + cp0.y^2   1 |
            // | cp1.x   cp1.y   cp1.x^2 + cp1.y^2   1 |
            // | cp2.x   cp2.y   cp2.x^2 + cp2.y^2   1 |
            // |  pt.x    pt.y     pt.x^2 + pt.y^2   1 |

            // simplified:

            // | cp0.x - pt.x   cp0.y - pt.y   cp0.x^2 + cp0.y^2 - (pt.x^2 + pt.y^2) |
            // | cp1.x - pt.x   cp1.y - pt.y   cp1.x^2 + cp1.y^2 - (pt.x^2 + pt.y^2) |
            // | cp2.x - pt.x   cp2.y - pt.y   cp2.x^2 + cp2.y^2 - (pt.x^2 + pt.y^2) |

            float ptx2y2 = point.x * point.x + point.y * point.y;
            float a00 = circlep0.x - point.x;
            float a01 = circlep0.y - point.y;
            float a02 = circlep0.x * circlep0.x + circlep0.y * circlep0.y - ptx2y2;
            float a10 = circlep1.x - point.x;
            float a11 = circlep1.y - point.y;
            float a12 = circlep1.x * circlep1.x + circlep1.y * circlep1.y - ptx2y2;
            float a20 = circlep2.x - point.x;
            float a21 = circlep2.y - point.y;
            float a22 = circlep2.x * circlep2.x + circlep2.y * circlep2.y - ptx2y2;

            // | d00  d01  d02 |
            // | d10  d11  d12 |
            // | d20  d21  d22 |

            float d = a00 * a11 * a22 + a01 * a12 * a20 + a02 * a10 * a21
                - a00 * a12 * a21 - a01 * a10 * a22 - a02 * a11 * a20;

            if (d > 0)
            {
                return Relation.External;
            }
            else if( d < 0)
            {
                return Relation.Internal;
            }
            else
            {
                return Relation.Coincidence;
            }
        }
    }
}

