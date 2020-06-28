using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Core
{
    public interface IRandom
    {
        int Seed { get; }

        IRandom SetSeed(int seed);

        int Next(int min, int max);

        double Next();
    }
}
