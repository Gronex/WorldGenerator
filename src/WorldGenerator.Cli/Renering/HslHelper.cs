using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Cli.Renering
{
    public static class HslHelper
    {
        public static Color GetColorFromHSL(double hue, double saturation, double lightness)
        {
            hue %= 360;
            var c = (1 - Math.Abs(2 * lightness - 1)) * saturation;

            var h = (int)(hue / 60);
            var x = c * (1 - Math.Abs((h % 2) - 1));

            var m = lightness - (c / 2);

            (double r, double g, double b) color = 
                h switch
                {
                    1 => (c + m, x + m, 0),
                    2 => (x + m, c + m, 0),
                    3 => (0, c + m, x + m),
                    4 => (0, x + m, c + m),
                    5 => (x + m, 0, c + m),
                    6 => (c + m, 0, x + m),
                    _ => (0, 0, 0),
                };
            return Color.FromArgb((int)color.r * 255, (int)color.g * 255, (int)color.b * 255);
        }
    }
}
