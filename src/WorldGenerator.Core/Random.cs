using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Core
{
    public class Random : IRandom
    {
        private readonly System.Random _random;

        public Random() : this(new System.Random())
        {}

        private Random(System.Random random)
        {
            _random = random;
        }

        public IRandom NewRandom(int seed)
        {
            return new Random(new System.Random(seed));
        }

        public int Next(int min, int max)
        {
            return _random.Next(min, max);
        }

        public double Next()
        {
            return _random.NextDouble();
        }
    }
}
