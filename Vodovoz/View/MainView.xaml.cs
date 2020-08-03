using System.Windows;
using Vodovoz.ViewModel;

namespace Vodovoz
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}