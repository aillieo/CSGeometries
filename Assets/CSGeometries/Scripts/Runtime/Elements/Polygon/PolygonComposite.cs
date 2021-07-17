using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public class PolygonComposite : Polygon
    {
        public readonly List<Polygon> holes = new List<Polygon>();

        public override bool Validate()
        {
            if (holes.Any(h => !Validate()))
            {
                return false;
            }

            // todo 检查各个 hole 是否相交

            // 每个hole 也可能是一个 Composite

            return true;
        }
    }
}
