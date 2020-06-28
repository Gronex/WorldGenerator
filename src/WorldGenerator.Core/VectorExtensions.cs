using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using MIConvexHull;

namespace WorldGenerator.Core
{
    public static class VectorExtensions
    {
        public static IVertex ToVertex(this Vector2 vector)
        {
            return new DefaultVertex
            {
                Position = new double[] { vector.X, vector.Y }
            };
        }
    }
}
