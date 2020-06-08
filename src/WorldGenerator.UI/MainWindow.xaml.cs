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

namespace WorldGenerator.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateClicked(object sender, RoutedEventArgs e)
        {
            var canvas = this.FindName("canvas") as Canvas;

            canvas?.Children.Add(new Polygon
            {
                Points = new PointCollection
                {
                    { new Point(10, 10) },
                    { new Point(20, 10) },
                    { new Point(20, 20) },
                    { new Point(10, 20) },
                    { new Point(10, 15) },
                },
                Fill = new SolidColorBrush(Color.FromRgb(1, 1, 1))
            });
        }
    }
}
