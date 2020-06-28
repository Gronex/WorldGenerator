using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using MIConvexHull;

namespace WorldGenerator.Core
{
    public class VoronoiCell : TriangulationCell<IVertex, VoronoiCell>
    {
        private readonly Lazy<Vector2> _circumcenter;

        public VoronoiCell()
        {
            _circumcenter = new Lazy<Vector2>(GetCircumcenter);
        }

        public Vector2 Circumcenter => _circumcenter.Value;


        private Vector2 GetCircumcenter()
        {
            // From MathWorld: http://mathworld.wolfram.com/Circumcircle.html

            var points = Vertices;

            var m = new double[3, 3];

            // x, y, 1
            for (int i = 0; i < 3; i++)
            {
                m[i, 0] = points[i].Position[0];
                m[i, 1] = points[i].Position[1];
                m[i, 2] = 1;
            }
            var a = Det(m);

            // size, y, 1
            for (int i = 0; i < 3; i++)
            {
                m[i, 0] = LengthSquared(points[i].Position);
            }
            var dx = -Det(m);

            // size, x, 1
            for (int i = 0; i < 3; i++)
            {
                m[i, 1] = points[i].Position[0];
            }
            var dy = Det(m);

            // size, x, y
            for (int i = 0; i < 3; i++)
            {
                m[i, 2] = points[i].Position[1];
            }
            var c = -Det(m);

            var s = -1.0 / (2.0 * a);
            var r = Math.Abs(s) * Math.Sqrt(dx * dx + dy * dy - 4 * a * c);
            return new Vector2((float)(s * dx), (float)(s * dy));
        }

        private double LengthSquared(double[] v)
        {
            double norm = 0;
            for (int i = 0; i < v.Length; i++)
            {
                var t = v[i];
                norm += t * t;
            }
            return norm;
        }

        private double Det(double[,] m)
        {
            return m[0, 0] * ((m[1, 1] * m[2, 2]) - (m[2, 1] * m[1, 2])) - m[0, 1] * (m[1, 0] * m[2, 2] - m[2, 0] * m[1, 2]) + m[0, 2] * (m[1, 0] * m[2, 1] - m[2, 0] * m[1, 1]);
        }
    }
}
