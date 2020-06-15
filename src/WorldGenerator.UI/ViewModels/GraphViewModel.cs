using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.UI.ViewModels
{
    public class GraphViewModel : BaseViewModel
    {
        public GraphViewModel(IEnumerable<(double X, double Y)> points, int width, int height)
        {
            Points = new ObservableCollection<Point>(points.Select((point) => new Point(point.X, point.Y, width, height)));
            Width = width;
            Height = height;
        }

        private int _width;
        public int Width
        {
            get => _width;
            set
            {
                if(value > 0)
                {
                    _width = value;
                    OnPropertyChanged(nameof(Width));
                }
            }
        }

        private int _height;
        public int Height
        {
            get => _height;
            set
            {
                if (value > 0)
                {
                    _height = value;
                    OnPropertyChanged(nameof(Height));
                }
            }
        }

        public ObservableCollection<Point> Points { get; }
    }
}
