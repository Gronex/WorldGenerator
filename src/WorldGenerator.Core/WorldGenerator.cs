using System;
using System.Collections.Generic;

namespace WorldGenerator.Core
{
    public class WorldGenerator
    {
        private readonly IRandom _random;

        public WorldGenerator(IRandom random)
        {
            _random = random;
        }

        public IEnumerable<(int X, int Y)> GeneratePoints(int xMax, int yMax)
        {
            while (true)
            {
                yield return (_random.Next(0, xMax), _random.Next(0, yMax));
            }
        }
    }
}
