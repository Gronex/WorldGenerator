using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Core
{
    public class Random : IRandom
    {
        private System.Random _random;

        public int Seed { get; private set; }

        public Random() : this(new System.Random())
        {}

        private Random(System.Random random)
        {
            Seed = random.Next();
            _random = new System.Random(Seed);
        }

        public IRandom SetSeed(int seed)
        {
            Seed = seed;
            _random = new System.Random(seed);
            return this;
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
