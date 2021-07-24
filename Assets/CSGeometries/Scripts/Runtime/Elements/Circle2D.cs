using UnityEngine;

namespace AillieoUtils.Geometries
{
    public class Circle2D
    {
        public Vector2 center;
        public float radius;

        public static Circle2D CreateWithThreePoints(Vector2 p0, Vector2 p1, Vector2 p2)
        {
            float r02 = p0.x * p0.x + p0.y * p0.y;
            float r12 = p1.x * p1.x + p1.y * p1.y;
            float r22 = p2.x * p2.x + p2.y * p2.y;


            //     | p0.x  p0.y  1 |
            // a = | p1.x  p1.y  1 |
            //     | p2.x  p2.y  1 |

            float a = Misc.Determination3x3(p0.x, p0.y, 1, p1.x, p1.y, 1, p2.x, p2.y, 1);

            //       | r02  p0.y  1 |
            // b = - | r12  p1.y  1 |
            //       | r22  p2.y  1 |

            float b = - Misc.Determination3x3(r02, p0.y, 1, r12, p1.y, 1, r22, p2.y, 1);

            //     | r02  p0.x  1 |
            // c = | r12  p1.x  1 |
            //     | r22  p2.x  1 |

            float c = Misc.Determination3x3(r02, p0.x, 1, r12, p1.x, 1, r22, p2.x, 1);

            //       | r02  p0.x  p0.y |
            // d = - | r12  p1.x  p1.y |
            //       | r22  p2.x  p2.y |

            float d = - Misc.Determination3x3(r02, p0.x, p0.y, r12, p1.x, p1.y, r22, p2.x, p2.y);

            Vector2 center = new Vector2(
                - b / (2 * a),
                - c / (2 * a));

            float radius = Mathf.Sqrt((b * b + c * c - 4 * a * d) / (4 * a * a));

            return new Circle2D() {
                center = center,
                radius = radius,
            };
        }
    }
}
