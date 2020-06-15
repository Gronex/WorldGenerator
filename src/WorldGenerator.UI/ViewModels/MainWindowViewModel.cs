using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using WorldGenerator.Core;
using WorldGenerator.UI.Services;

namespace WorldGenerator.UI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly Core.WorldGenerator _worldGenerator;
        private readonly WindowService _windowService;
        private GraphViewModel? _graphViewModel;

        public MainWindowViewModel()
        {
            _worldGenerator = new Core.WorldGenerator(new Core.Random());
            _windowService = new WindowService();
            GeneratePointsCommand = new DelegateCommand(GeneratePoints);
        }

        private int _pointCount = 1000;
        public int PointCount
        {
            get
            {
                return _pointCount;
            }
            set
            {
                if(value > 0)
                {
                    _pointCount = value;
                    OnPropertyChanged(nameof(PointCount));
                }
            }
        }

        public ICommand GeneratePointsCommand { get; }

        public void GeneratePoints()
        {
            var points = _worldGenerator.GeneratePoints()
                .Take(PointCount);

            _graphViewModel = new GraphViewModel(points, 500, 500);

            _windowService.Show(_graphViewModel);
        }
    }
}
