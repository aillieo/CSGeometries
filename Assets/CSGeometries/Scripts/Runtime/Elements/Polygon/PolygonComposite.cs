using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AillieoUtils.Geometries
{
    public class PolygonComposite : Polygon
    {
        public readonly List<Polygon> holes = new List<Polygon>();

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
            if (holes.Any(h => !h.Validate()))
            {
                return false;
            }

            // todo 检查各个 hole 是否相交

            // 每个hole 也可能是一个 Composite

            return true;
        }
    }
}
