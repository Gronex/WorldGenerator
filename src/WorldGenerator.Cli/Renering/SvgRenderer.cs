using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Svg;

namespace WorldGenerator.Cli
{
    public class SvgRenderer : IRenderer
    {

        private readonly SvgDocument _svg;

        public SvgRenderer()
        {
            _svg = new SvgDocument();
        }

        public void AddLine(Vector2 start, Vector2 end)
        {
            _svg.Children.Add(new SvgLine()
            {
                StartX = start.X,
                StartY = start.Y,
                EndX = end.X,
                EndY = end.Y,
                Stroke = new SvgColourServer(Color.Black),
                StrokeWidth = 1,
            });
        }

        public void AddPoint(Vector2 point, string? containerId)
        {
            var circle = new SvgCircle()
            {
                CenterX = new SvgUnit(point.X),
                CenterY = new SvgUnit(point.Y),
                Fill = new SvgColourServer(Color.Red),
                Radius = new SvgUnit(1)
            };

            SvgElement container = _svg;
            if(!string.IsNullOrWhiteSpace(containerId))
            {
                container = _svg.GetElementById(containerId);

            }

            container.Children.Add(circle);
        }

        public string AddPolygon(Color color, params Vector2[] points)
        {
            var polyPoints = new SvgPointCollection();

            polyPoints.AddRange(
                points.SelectMany(p => new[] { new SvgUnit(p.X), new SvgUnit(p.Y) }));

            var id = Guid.NewGuid().ToString();
            _svg.Children.Add(new SvgPolygon
            {
                Points = polyPoints,
                Fill = new SvgColourServer(color),
                ID = id
            });

            return id;
        }

        public void Draw()
        {
            using var writer = new XmlTextWriter("./Tmp.svg", Encoding.UTF8);
            _svg.Write(writer);
        }
    }
}
