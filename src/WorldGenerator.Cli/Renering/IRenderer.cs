using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Cli
{
    public interface IRenderer
    {
        void AddPoint(Vector2 point, string? containerId = null);

        void AddLine(Vector2 start, Vector2 end);

        string AddPolygon(Color color, params Vector2[] points);

        void Draw();
    }
}
