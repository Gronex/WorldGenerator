using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Core
{
    public interface IRandom
    {
        IRandom NewRandom(int seed);

        int Next(int min, int max);

        double Next();
    }
}
