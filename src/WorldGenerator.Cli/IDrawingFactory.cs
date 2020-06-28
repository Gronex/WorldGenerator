using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Cli
{
    public interface IDrawingFactory
    {
        IRenderer GetNewRenderer();
    }
}
