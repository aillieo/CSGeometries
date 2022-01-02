using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace AillieoUtils.Geometries
{
    public static class BresenhamsLine
    {
        public static IEnumerable<Vector2Int> Intersect(Vector2Int inPointGlobal, Vector2Int outPointGlobal)
        {
            int inX = inPointGlobal.x;
            int inY = inPointGlobal.y;
            int outX = outPointGlobal.x;
            int outY = outPointGlobal.y;

            //Debug.LogError($"inX={inX} inY={inY} outX={outX} outY={outY}");

            float deltaX = outX - inX;
            float deltaY = outY - inY;

            bool isHorizontal = Mathf.Abs(deltaX) >= Mathf.Abs(deltaY);

            int stepX = deltaX > 0 ? 1 : -1;
            int stepY = deltaY > 0 ? 1 : -1;

            float deltaErr = isHorizontal ? Mathf.Abs(deltaY / deltaX) : Mathf.Abs(deltaX / deltaY);

            int x = inX;
            int y = inY;
            float error = 0f;

            yield return new Vector2Int(inX, inY);

            if (isHorizontal)
            {
                /*
                   整体是横向的
                        ／
                      ／
                    ／
                  ／
                */

                for (;
                    stepX > 0 ? (x <= outX) : (x >= outX);
                    x += stepX)
                {
                    yield return new Vector2Int(x, y);

                    error += deltaErr;
                    if (error >= 0.5f)
                    {
                        y += stepY;
                        error -= 1.0f;
                    }
                }
            }
            else
            {
                /*
                   整体是纵向的
                        /
                       /
                      /
                     /
                */

                for (;
                    stepY > 0 ? (y <= outY) : (y >= outY);
                    y += stepY)
                {
                    //Debug.LogError($"交点 ({x},{y})");
                    yield return new Vector2Int(x, y);

                    error += deltaErr;
                    if (error >= 0.5f)
                    {
                        x += stepX;
                        error -= 1.0f;
                    }
                }
            }

            yield return new Vector2Int(outX, outY);
        }
    }
}
