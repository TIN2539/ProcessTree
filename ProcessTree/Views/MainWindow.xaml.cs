using ProcessTree.ViewModels;
using System.Windows;

namespace ProcessTree
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}