using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WorldGenerator.Core;

namespace WorldGenerator.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Core.WorldGenerator _worldGenerator;

        public MainWindow()
        {
            InitializeComponent();
            _worldGenerator = new Core.WorldGenerator(new Core.Random());
        }

        private void GenerateClicked(object sender, RoutedEventArgs e)
        {
            var canvas = this.FindName("canvas") as Canvas;

            if(canvas == null)
            {
                return;
            }

            var points = _worldGenerator.GeneratePoints((int)canvas.Width, (int)canvas.Height)
                .Take(1000)
                .ToArray();

            foreach(var (X, Y) in points)
            {
                var ellipse = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = new SolidColorBrush(Color.FromRgb(1,1,1))
                };
                canvas.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, X - 2.5);
                Canvas.SetTop(ellipse, Y - 2.5);
            }
        }
    }
}
