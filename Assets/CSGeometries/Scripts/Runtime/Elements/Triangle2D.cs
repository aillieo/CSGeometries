using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public class Triangle2D
    {
        public Vector2 p0;
        public Vector2 p1;
        public Vector2 p2;

        public Triangle2D() { }

        public Triangle2D(Vector2 p0, Vector2 p1, Vector2 p2)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
        }

        public IEnumerable<Vector2> Points
        {
            get
            {
                yield return p0;
                yield return p1;
                yield return p2;
            }
        }

        public IEnumerable<Vector2> PointsReverse
        {
            get
            {
                yield return p2;
                yield return p1;
                yield return p0;
            }
        }

        public Vector2 this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return p0;
                    case 1:
                        return p1;
                    case 2:
                        return p2;
                }
                throw new ArgumentOutOfRangeException();
            }

            set
            {
                switch (index)
                {
                    case 0:
                        p0 = value;
                        break;
                    case 1:
                        p1 = value;
                        break;
                    case 2:
                        p2 = value;
                        break;
                }
                throw new ArgumentOutOfRangeException();
            }
        }

        public bool Clockwise()
        {
            return Misc.Clockwise(ref p0, ref p1, ref p2) < 0;
        }
    }
}
