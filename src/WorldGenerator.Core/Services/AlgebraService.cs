using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Core
{
    public class AlgebraService
    {
        public (float Slope, float Offset) GetLinearFunction(Vector2 vector1, Vector2 vector2)
        {
            var slope = (vector2.Y - vector1.Y) / (vector2.X - vector1.X);

            var offset = vector1.Y - (vector1.X * slope);
            return (slope, offset);
        }

        public Vector2 GetCrossPoint(Vector2 point1, Vector2 point2, Vector2 limit, bool upper)
        {
            var (slope, b) = GetLinearFunction(point1, point2);

            var newPoint = point2;

            if ((upper && newPoint.X > limit.X) || (!upper && newPoint.X < limit.X))
            {
                newPoint = new Vector2(limit.X, slope * limit.X + b);
            }

            if ((upper && newPoint.Y > limit.Y) || (!upper && newPoint.Y < limit.Y))
            {
                newPoint = new Vector2((limit.Y - b) / slope, limit.Y);
            }
            return newPoint;
        }
    }
}
