using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Cli
{
    public class SvgDrawingFactory : IDrawingFactory
    {
        public IRenderer GetNewRenderer()
        {
            return new SvgRenderer();
        }
    }
}
