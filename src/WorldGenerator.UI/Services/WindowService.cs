using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.UI.Services
{
    public class WindowService
    {
        public void Show<T>(T viewModel)
        {
            var window = new GraphWindow
            {
                DataContext = viewModel
            };
            window.Show();
        }
    }
}
