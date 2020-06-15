using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldGenerator.Core
{
    public class WorldGenerator
    {
        private readonly IRandom _random;

        public WorldGenerator(IRandom random)
        {
            _random = random;
        }

        public IEnumerable<(double X, double Y)> GeneratePoints()
        {
            while (true)
            {
                yield return (_random.Next(), _random.Next());
            }
        }

        public void BuildVoronoi(IEnumerable<(int X, int Y)> points)
        {
            var verteces = points.Select((point) => new double[] { point.X, point.Y });
            var mesh = MIConvexHull.Triangulation.CreateVoronoi(verteces.ToList());

            Console.Out.WriteLine(mesh?.ToString());
        }
    }
}
