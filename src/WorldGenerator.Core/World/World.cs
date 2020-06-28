using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Core
{
    public class World
    {
        public IReadOnlyList<Cell> Cells { get; set; }

        public Vector2 WorldLimit { get; private set; }
        public Vector2 WorldStart { get; private set; }

        public World(Vector2 worldLimit, Vector2? worldStart = null)
        {
            Cells = new List<Cell>();
            WorldLimit = worldLimit;
            WorldStart = worldStart ?? Vector2.Zero;
        }

        public void Transform(Matrix3x2 matrix3x2)
        {
            foreach(var cell in Cells)
            {
                cell.Transform(matrix3x2);
            }

            WorldLimit = Vector2.Transform(WorldLimit, matrix3x2);
            WorldStart = Vector2.Transform(WorldStart, matrix3x2);
        }
    }
}
