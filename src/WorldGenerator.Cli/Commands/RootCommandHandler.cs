using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

using WorldGenerator.Cli.Arguments;
using WorldGenerator.Core;

namespace WorldGenerator.Cli
{
    public class RootCommandHandler : BaseCommandHandler<GeneratorArguments>
    {
        private readonly IConsole _console;
        private readonly Core.WorldGenerator _worldGenerator;
        private readonly IDrawingFactory _drawingFactory;
        private readonly IRandom _random;

        public RootCommandHandler(IConsole console, Core.WorldGenerator worldGenerator, IDrawingFactory drawingFactory, IRandom random)
        {
            _console = console;
            _worldGenerator = worldGenerator;
            _drawingFactory = drawingFactory;
            _random = random;
        }

        public override Task<int> ExecuteCommand(GeneratorArguments args)
        {
            if (args.Seed.HasValue)
            {
                _random.SetSeed(args.Seed.Value);
            }
            else
            {
                args.Seed = _random.Seed;
            }

            _console.Out.WriteLine(args.ToString());

            _console.Out.WriteLine($"Generating {args.Points} points");

            var worldLimit = new Vector2(args.Width, args.Height);

            var points = _worldGenerator.GeneratePoints()
                .Select(p => new Vector2(p.X * worldLimit.X, p.Y * worldLimit.Y))
                .Take(args.Points)
                .ToArray();

            var drawing = _drawingFactory.GetNewRenderer();

            var world = _worldGenerator.InitializeWorld(points, Vector2.Zero, worldLimit);

            _console.Out.WriteLine($"Starting Relaxation process {args.Relax} iterations");
            for (var i = 0; i < args.Relax; i++)
            {
                _console.Out.WriteLine($"Relaxing {i + 1} of {args.Relax}");
                points = world.Cells.Select(x => x.GetCentroid()).ToArray();
                world = _worldGenerator.RelaxCells(world);
            }

            world.Transform(Matrix3x2.CreateTranslation(worldLimit / 2));

            _console.Out.WriteLine("Starting draw");

            DrawVoronoi(drawing, world);

            drawing.Draw();
            var path = Path.GetFullPath("./Temp.svg");
            _console.Out.WriteLine($"Resulting file: {path}");
            return Task.FromResult(0);
        }

        private void DrawVoronoi(
            IRenderer drawing,
            World world)
        {
            foreach(var cell in world.Cells)
            {
                drawing.AddPolygon(cell.Points);
                drawing.AddPoint(cell.RegionBase);
            }
        }
    }
}
