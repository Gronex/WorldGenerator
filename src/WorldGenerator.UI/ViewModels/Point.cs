using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.UI.ViewModels
{
    public class Point
    {
        public Point(double x, double y, int xScale, int yScale)
        {
            X = x;
            Y = y;
            XScale = xScale;
            YScale = yScale;
        }

        public double XScaled => X * XScale;

        public double YScaled => Y * YScale;

        public double X { get; set; }

        public double Y { get; set; }

        public int XScale { get; set; }

        public int YScale { get; set; }
    }
}
