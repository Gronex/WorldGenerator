using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Core
{
    public class Cell
    {
        public Vector2[] Points { get; }

        public Vector2 RegionBase { get; private set; }

        public Cell(ICollection<Vector2> points, Vector2 regionBase)
        {
            Points = points.ToArray();
            RegionBase = regionBase;
        }

        public Vector2 GetCentroid()
        {
            double x = 0;
            double y = 0;
            for (var i = 0; i < Points.Length; i++)
            {
                var p1 = Points.GetWrapedValue(i);
                var p2 = Points.GetWrapedValue(i + 1);
                
                var partial = (p1.X * p2.Y - p2.X * p1.Y);
                x += (p1.X + p2.X) * partial;
                y += (p1.Y + p2.Y) * partial;
            }
            return new Vector2((float)(x / (6 * Area)), (float)(y / (6 * Area)));
        }

        public double Area
        {
            get
            {
                double acc = 0;
                for (var i = 0; i < Points.Length; i++)
                {
                    var p1 = Points[i];
                    var p2 = Points.GetWrapedValue(i + 1);
                    
                    acc += p1.X * p2.Y - p2.X * p1.Y;
                }
                return acc / 2;
            }
        }

        public void Transform(Matrix3x2 matrix)
        {
            for (var i = 0; i < Points.Length; i++)
            {
                Points[i] = Vector2.Transform(Points[i], matrix);
            }

            RegionBase = Vector2.Transform(RegionBase, matrix);
        }
    }
}
