using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using MIConvexHull;

namespace WorldGenerator.Core
{
    public class WorldGenerator
    {
        private readonly IRandom _random;
        private readonly AlgebraService _algebraService;

        public WorldGenerator(IRandom random, AlgebraService algebraService)
        {
            _random = random;
            _algebraService = algebraService;
        }

        public IEnumerable<Vector2> GeneratePoints()
        {
            while (true)
            {
                yield return new Vector2((float)_random.Next(), (float)_random.Next());
            }
        }

        public World InitializeWorld(IEnumerable<Vector2> points, Vector2 lowerWorldLimit, Vector2 upperWorldLimit)
        {
            var corners = new[]{
                new DefaultVertex() { Position = new double[] { lowerWorldLimit.X, lowerWorldLimit.Y } },
                new DefaultVertex() { Position = new double[] { upperWorldLimit.X, lowerWorldLimit.Y } },
                new DefaultVertex() { Position = new double[] { upperWorldLimit.X, upperWorldLimit.Y } },
                new DefaultVertex() { Position = new double[] { lowerWorldLimit.X, upperWorldLimit.Y } }
            };

            var verteces = points
                .Select((point) => point.ToVertex())
                .Append(corners[0])
                .Append(corners[1])
                .Append(corners[2])
                .Append(corners[3])
                .ToArray();
            var mesh = VoronoiMesh.Create<IVertex, VoronoiCell>(verteces);

            if(mesh == null)
            {
                throw new ApplicationException("Unable to generate Voronoi");
            }

            var cells = new List<Cell>();
            foreach(var vertex in verteces.Except(corners))
            {
                var cellPoints = new List<Vector2>();
                var edges = mesh.Edges
                    .Where(x => x.Source.Vertices.Any(z => z == vertex) && x.Target.Vertices.Any(z => z == vertex))
                    .ToArray();

                if (!edges.Any())
                {
                    continue;
                }

                var current = edges.First().Source.Circumcenter;

                while (!cellPoints.Contains(current))
                {
                    cellPoints.Add(current);
                    var cell = edges.FirstOrDefault(x => 
                        (x.Source.Circumcenter == current && !cellPoints.Contains(x.Target.Circumcenter))
                        || (x.Target.Circumcenter == current && !cellPoints.Contains(x.Source.Circumcenter)));

                    if(cell == null)
                    {
                        break;
                    }

                    var point = cell.Source.Circumcenter == current
                        ? cell.Target.Circumcenter
                        : cell.Source.Circumcenter;
                    current = point;
                }

                cellPoints = ScopePoints(cellPoints, lowerWorldLimit, upperWorldLimit).ToList();

                cells.Add(new Cell(cellPoints, new Vector2((float)vertex.Position[0], (float)vertex.Position[1])));
            }

            var world = new World(upperWorldLimit, lowerWorldLimit)
            {
                Cells = cells
            };
            return world;
        }

        public void GrowIsland(World world, int islandSeed)
        {
            var seedCell = world.Cells[islandSeed];

            var height = _random.Next();

            seedCell.Height = height;
        }

        public World RelaxCells(World world)
        {
            return InitializeWorld(world.Cells.Select(x => x.GetCentroid()), world.WorldStart, world.WorldLimit);
        }

        private IEnumerable<Vector2> ScopePoints(IEnumerable<Vector2> points, Vector2 lowerWorldLimit, Vector2 upperWorldLimit)
        {
            var resultPoints = new List<Vector2>();
            var pointArray = points.ToArray();
            for (var i = 0; i < pointArray.Length; i++)
            {
                var point = pointArray[i];

                if (point.X > upperWorldLimit.X || point.Y > upperWorldLimit.Y)
                {
                    resultPoints.Add(_algebraService.GetCrossPoint(pointArray.GetWrapedValue(i - 1), point, upperWorldLimit, true));
                    resultPoints.Add(_algebraService.GetCrossPoint(pointArray.GetWrapedValue(i + 1), point, upperWorldLimit, true));
                }
                else if (point.X < lowerWorldLimit.X || point.Y < lowerWorldLimit.Y)
                {
                    resultPoints.Add(_algebraService.GetCrossPoint(pointArray.GetWrapedValue(i - 1), point, lowerWorldLimit, false));
                    resultPoints.Add(_algebraService.GetCrossPoint(pointArray.GetWrapedValue(i + 1), point, lowerWorldLimit, false));
                }
                else
                {
                    resultPoints.Add(point);
                }
            }

            return resultPoints;
        }
    }
}
