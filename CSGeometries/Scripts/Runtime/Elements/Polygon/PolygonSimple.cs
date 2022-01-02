using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public class PolygonSimple : Polygon
    {
        public override float Area()
        {
            throw new System.NotImplementedException();
        }

        public override bool Clockwise()
        {
            throw new System.NotImplementedException();
        }

        public override bool Validate()
        {
            if (verts.Count <= 3)
            {
                Debug.LogError("not enough verts");
                return false;
            }

            // todo 方向验证

            // todo 检查自相交
            return true;
        }
    }
}
