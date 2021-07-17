using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public class PolygonSimple : Polygon
    {
        public override bool Validate()
        {
            if (verts.Count <= 3)
            {
                return false;
            }

            // todo 检查自相交
            return true;
        }
    }
}
