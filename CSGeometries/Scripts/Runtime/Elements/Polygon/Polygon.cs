using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public abstract class Polygon
    {
        public readonly List<Vector2> verts = new List<Vector2>();

        public abstract bool Validate();
    }
}
